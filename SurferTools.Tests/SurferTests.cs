using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

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

            var retVal = _fixture.SurferService.InverseDistanceGridding(_csvFileLocation, newGridFilename, 16);
            retVal.ShouldBeTrue("Gridding was not successful");
            File.Exists(newGridFilename).ShouldBeTrue("New grid file doesn't exists");

            // Need a plot document:
            GetPlotDocument();

            // Add the grid as a contour map:
            var mapFrame = _fixture.PlotDocument.Shapes.AddContourMap(newGridFilename);
        }

        [Fact]
        public void AddPostMap()
        {
            _fixture.CloseSurferOnTestFinish = false;
            // Need a plot document:
            GetPlotDocument();

            _fixture.SurferService.AddPostMap(_fixture.PlotDocument, _csvFileLocation, 16);
        }


        [Fact]
        public void FullRun()
        {
            // Init Surfer service:
            var surferService = new SurferService();
            // Get plot document:
            var plotDocument = surferService.AddPlotDocument();
            // Add post map:
            var mapFrame = surferService.AddPostMap(plotDocument, _csvFileLocation, 16);
            // Get limits:
            var dataLimits = new Limits(mapFrame.xMin, mapFrame.xMax, mapFrame.yMin, mapFrame.yMax);


        }

        private void GetPlotDocument()
        {
            if (_fixture.PlotDocument is null)
                AddPlotDocument();

            if (_fixture.PlotDocument is null)
                throw new ShouldAssertException("Can't get the plot document");
        }

        private void AddPlotDocument()
        {
            var currentNumDocuments = _fixture.SurferService.GetNumDocuments();
            _fixture.PlotDocument = _fixture.SurferService.AddPlotDocument();
            _fixture.PlotDocument.ShouldNotBeNull("New plot document is null");
            _output.WriteLine(_fixture.PlotDocument.FullName);

            var newNumDocuments = _fixture.SurferService.GetNumDocuments();
            newNumDocuments.ShouldBe(currentNumDocuments + 1, "Number of documents is unexpected");

            _output.WriteLine("AddPlotDocument was successfull");
        }
    }
}
