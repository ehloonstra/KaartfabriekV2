using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Shouldly;
using Surfer;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SurferTools.Tests
{
    public class SurferTests : IClassFixture<SurferFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly SurferFixture _fixture;
        private readonly string _csvFileLocation;

        public SurferTests(ITestOutputHelper output, SurferFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            _csvFileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"TestData\01 velddata-RD.csv");
            _output.WriteLine("In ctor SurferTests");
        }

        [Fact]
        public async Task ShowHideSurfer()
        {
            // Show Surfer
            var retVal = _fixture.SurferService.ShowHideSurfer(true);
            retVal.ShouldBeTrue("Surfer is not visible");
            _fixture.SurferService.IsVisible.ShouldBeTrue("Surfer is not visible");

            // Wait 5 seconds:
            await Task.Delay(5_000);

            // Hide Surfer
            retVal = _fixture.SurferService.ShowHideSurfer(false);
            retVal.ShouldBeFalse("Surfer is still visible");
            _fixture.SurferService.IsVisible.ShouldBeFalse("Surfer is still visible");

            _output.WriteLine("ShowHideSurfer was successfull");
        }

        [Fact]
        public void InverseDistanceGridding()
        {
            _fixture.CloseSurferOnTestFinish = false;

            // Create the grid:
            var newGridFilename = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(_csvFileLocation) + ".grd");
            if (File.Exists(newGridFilename)) File.Delete(newGridFilename);

            // TODO: add limits:
            var retVal = _fixture.SurferService.InverseDistanceGridding(_csvFileLocation, newGridFilename, 1, 2, 16, null);
            retVal.ShouldBeTrue("Gridding was not successful");
            File.Exists(newGridFilename).ShouldBeTrue("New grid file doesn't exists");

            // TODO: Add the grid as a contour map:
            //var mapFrame = _fixture.PlotDocument.Shapes.AddContourMap(newGridFilename);
            //mapFrame.ShouldNotBeNull("Map frame is null");
        }

        [Fact]
        public void AddPostMap()
        {
            _fixture.CloseSurferOnTestFinish = false;

            var mapFrame = _fixture.SurferService.AddPostMap(_csvFileLocation, "Velddata");
            mapFrame.ShouldNotBeNull("Map frame is null");
            _fixture.SurferService.SetColoringVelddataPostmap(mapFrame.Overlays.Item(1) as IPostLayer2, 16);
        }

        [Fact]
        public void BufferBlankFile()
        {
            var surferApp = new Surfer.Application { PageUnits = SrfPageUnits.srfUnitsCentimeter };
            // Add PlotDocument:
            if (!(surferApp.Documents.Add() is IPlotDocument3 plot))
                throw new Exception("Could not add plot document");

            // Set page orientation:
            plot.PageSetup.Orientation = SrfPaperOrientation.srfLandscape;
            plot.Activate();

            // Add blank file:
            var mapFrame = plot.Shapes.AddBaseMap(@"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\Aan.bln");

            if (mapFrame.Overlays.Item(1) is not IVectorBaseLayer2 baseMapLayer)
                throw new Exception("Cannot get baseMapLayer");

            if (baseMapLayer.Shapes.Item(1) is not IPolygon2 polygon)
                throw new Exception("Cannot get polygon");

            plot.Selection.DeselectAll();
            // polygon.Select();  <=== Throws 0x80020009 DISP_E_EXCEPTION
            // polygon.Selected = true;  <=== Throws 0x80020009 DISP_E_EXCEPTION
            // plot.Selection.Buffer <== Doesn't exists

            plot.Selection.Combine(); // <== Does exists
            plot.Selection.StackMaps(); // <== Does exists

            surferApp.Visible = true;
        }

        [Fact]
        public void RoundUpTo1()
        {
            var limits = new Limits(162834.4, 163312.9, 467386, 468325);
            var newLimits = Limits.RoundUp(limits, 1);
            _output.WriteLine(newLimits.ToString());
            newLimits.Xmin.ShouldBe(162834);
            newLimits.Ymin.ShouldBe(467386);
            newLimits.Xmax.ShouldBe(163313);
            newLimits.Ymax.ShouldBe(468326);
        }

        [Fact]
        public void RoundUpTo50()
        {
            var limits = new Limits(162834.4, 163312.9, 467386, 468325);
            var newLimits = Limits.RoundUp(limits, 50);
            _output.WriteLine(newLimits.ToString());
            newLimits.Xmin.ShouldBe(162800);
            newLimits.Ymin.ShouldBe(467350);
            newLimits.Xmax.ShouldBe(163350);
            newLimits.Ymax.ShouldBe(468350);
        }

        [Fact]
        public void RoundUpTo100()
        {
            var limits = new Limits(162834, 163312, 467386, 468325);
            var newLimits = Limits.RoundUp(limits, 100);
            _output.WriteLine(newLimits.ToString());
            newLimits.Xmin.ShouldBe(162800);
            newLimits.Ymin.ShouldBe(467300);
            newLimits.Xmax.ShouldBe(163400);
            newLimits.Ymax.ShouldBe(468400);
        }

        [Fact]
        public void GetSurferSamplesLocation()
        {
            var result = _fixture.SurferService.GetSurferSamplesLocation();
            result.ShouldBe(@"D:\Program Files\Golden Software\Surfer\Samples");
        }
    }
}
