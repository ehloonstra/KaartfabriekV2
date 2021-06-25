using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Shared;
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

            if (mapFrame is not IMapFrame3 mapFrame3)
                throw new Exception("Cannot get mapFrame3");
            _fixture.SurferService.SetColoringVelddataPostmap(mapFrame3.Overlays.Item(1) as IPostLayer2, 16);
            // Trying to force a refresh

        }

        [Fact]
        public void BufferBlankFile()
        {
            var surferApp = new Surfer.Application { PageUnits = SrfPageUnits.srfUnitsCentimeter };
            // Add PlotDocument:
            if (surferApp.Documents.Add() is not IPlotDocument3 plot)
                throw new Exception("Could not add plot document");

            // Set page orientation:
            plot.PageSetup.Orientation = SrfPaperOrientation.srfLandscape;
            plot.Activate();

            // Add blank file:
            var mapFrame = plot.Shapes.AddBaseMap(@"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\Aan.bln");

            if (mapFrame.Overlays.Item(1) is not IVectorBaseLayer2 baseMapLayer)
                throw new Exception("Cannot get baseMapLayer");

            if (baseMapLayer.Shapes is not IShapes7 shapes)
                throw new Exception("Cannot get shapes");

            plot.Selection.DeselectAll();

            shapes.StartEditing();

            if (baseMapLayer.Shapes.Item(1) is not IPolygon2 polygon)
                throw new Exception("Cannot get polygon");

            polygon.Selected = true;

            if (plot.Selection is not ISelection3 plotSelection)
                throw new Exception("Cannot get plot selection");

            plotSelection.Buffer(NumberBuffers: 1, BufferDistance: 20);

            if (baseMapLayer.Shapes.Item(baseMapLayer.Shapes.Count) is not IPolygon2 bufferedPolygon)
                throw new Exception("Cannot get buffered polygon");

            bufferedPolygon.Name = "Buffer Polygon";
            bufferedPolygon.Line.ForeColorRGBA.Color = srfColor.srfColorForestGreen;
            bufferedPolygon.Fill.ForeColorRGBA.Color = srfColor.srfColorForestGreen;
            bufferedPolygon.Fill.Pattern = "25 Percent";
            bufferedPolygon.SetZOrder(SrfZOrder.srfZOToBack);

            shapes.StopEditing();

            surferApp.Visible = true;
        }

        [Fact]
        public void GridMath()
        {
            var surferApp = new Surfer.Application { PageUnits = SrfPageUnits.srfUnitsCentimeter };
            // Add PlotDocument:
            if (surferApp.Documents.Add() is not IPlotDocument3 plot)
                throw new Exception("Could not add plot document");

            // Set page orientation:
            plot.PageSetup.Orientation = SrfPaperOrientation.srfLandscape;
            plot.Activate();

            var gridMathInput = new List<IGridMathInput>
            {
                surferApp.NewGridMathInput(
                    @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\Cs137.grd",
                    "A"),
                surferApp.NewGridMathInput(
                    @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\K40.grd",
                    "B")
            };

            var outGrid =
                @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\MathTest.grd";
            surferApp.GridMath3("2*A+B", gridMathInput.ToArray(), outGrid);

            plot.Shapes.AddContourMap(outGrid);

            surferApp.Visible = true;
        }

        [Fact]
        public void ListAllSurferMethods()
        {
            // ListAllMethods(typeof(IPlotDocument3));
            //ListAllMethods(typeof(ISelection3));
            //ListAllMethods(typeof(IShapes7));
            // ListAllMethods(typeof(IMarkerFormat));
            //ListAllMethods(typeof(IColorMap));
            //ListAllMethods(typeof(IColorMap2));
            ListAllMethods(typeof(IPostLayer2));
            //ListAllMethods(typeof(IContourLayer));
            //ListAllMethods(typeof(IMapFrame3));
        }

        [Fact]
        public void TestProjectFile()
        {
            // Create project
            var project = new Project
            {
                WorkingFolder = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp",
                SampleDataFileLocation =
                    @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\01 monsterdata.csv",
                SampleDataFileLocationProjected =
                @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\01 monsterdata.csv",
                FieldDataFileLocation = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\01 velddata.csv",
                FieldDataFileLocationProjected = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\01 velddata-RD.csv",
                FieldBorderLocation = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\Aan.bln",
                FieldBorderLocationBuffered = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\01 Lange Kamp\Aan.buffered.bln",
                ColumnIndexes = new ColumnIndexes { X = 1, Y = 2, Alt = 3, K40 = 16, Cs137 = 17, Th232 = 18, U238 = 19, Tc = 20 },
                NuclideGridLocations = new NuclideGridLocations
                {
                    AltLocation = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\Alt.grd",
                    K40Location = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\K40.grd",
                    Cs137Location = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\Cs137.grd",
                    Th232Location = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\Th232.grd",
                    U238Location = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\U238.grd",
                    TcLocation = @"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\TC.grd"
                }
            };

            // Check values
            project.WorkingFolder.ShouldBe(@"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp");
            project.ColumnIndexes.K40.ShouldBe(16);
            project.NuclideGridLocations.AltLocation.ShouldBe(@"D:\dev\TopX\Loonstra\Testdata\Van Leeuwen Boomkamp\2021 017 Van Leeuwen Boomkamp\2 Data\02 Lange Stuk\nuclide grids\Alt.grd");
            project.GridSettings.GridSpacing.ShouldBe(3.5f);

            // Write project file:
            var fileName = Path.Combine(Path.GetTempPath(), "project.json");
            project.Save(fileName);
            _output.WriteLine(fileName);

            // Read project file:
            var newProject = Project.Load(fileName);

            // Check values
            newProject.WorkingFolder.ShouldBe(project.WorkingFolder);
            newProject.ColumnIndexes.K40.ShouldBe(project.ColumnIndexes.K40);
            newProject.NuclideGridLocations.AltLocation.ShouldBe(project.NuclideGridLocations.AltLocation);
            newProject.GridSettings.GridSpacing.ShouldBe(project.GridSettings.GridSpacing);
        }

        private void ListAllMethods(Type myType)
        {
            _output.WriteLine("Listing the methods of " + myType.Name);
            // Get the public methods.
            var myArrayMethodInfo = myType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _output.WriteLine("\nThe number of public methods is {0}.", myArrayMethodInfo.Length);
            // Display all the methods.
            DisplayMethodInfo(myArrayMethodInfo);
            // Get the nonpublic methods.
            var myArrayMethodInfo1 = myType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _output.WriteLine("\nThe number of protected methods is {0}.", myArrayMethodInfo1.Length);
            // Display information for all methods.
            DisplayMethodInfo(myArrayMethodInfo1);
        }

        private void DisplayMethodInfo(IEnumerable<MethodInfo> myArrayMethodInfo)
        {
            // Display information for all methods.
            foreach (var myMethodInfo in myArrayMethodInfo)
            {
                _output.WriteLine("\nThe name of the method is {0}. It returns {1}", myMethodInfo.Name, myMethodInfo.ReturnType);
            }
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
