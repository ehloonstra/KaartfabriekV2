using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using Shared;
using Surfer;
using SurferTools;

namespace KaartfabriekUI.Service
{
    /// <summary>
    /// Service for the GUI
    /// </summary>
    public class KaartfabriekService
    {

        private readonly ProjectFile _projectFile;
        private readonly Action<string> _addProgress;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="projectFile"></param>
        /// <param name="addProgress">To log the progress in the form</param>
        public KaartfabriekService(ProjectFile projectFile, Action<string> addProgress)
        {
            _projectFile = projectFile;
            _addProgress = addProgress;
        }

        /// <summary>
        /// Action to perform after clicking the button
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="veldDataLocation"></param>
        /// <param name="monsterDataLocation"></param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colK40"></param>
        /// <returns></returns>
        public bool OpenDataForBlanking(string workingFolder, string veldDataLocation, string monsterDataLocation,
            int colX, int colY, int colK40)
        {
            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode), workingFolder,
                _addProgress);

            // Add Velddata:
            var mapFrameVelddata = surferService.AddPostMap(veldDataLocation, "Velddata", colX, colY);
            surferService.SetColoringVelddataPostmap(mapFrameVelddata.Overlays.Item(1) as IPostLayer2, colK40);

            // Add Monsterdata:
            var mapFrameMonsterdata = surferService.AddPostMap(monsterDataLocation, "Monsterdata");
            surferService.SetLabelMonsterdataPostmap(mapFrameMonsterdata.Overlays.Item(1) as IPostLayer2, 3);

            var velddataLimits = new Limits(mapFrameVelddata.xMin,
                mapFrameVelddata.xMax,
                mapFrameVelddata.yMin,
                mapFrameVelddata.yMax);

            // Get AAN-data:
            var aanShapefileLocation = ProcessTools.GetAanData(Limits.RoundUp(velddataLimits, 1), workingFolder);
            if (!File.Exists(aanShapefileLocation))
            {
                throw new Exception("Kon de AAN-data niet ophalen.");
            }

            _addProgress("De AAN-data is opgehaald.");
            // Add AAN-data:
            var mapFrameAan = surferService.AddShapefile(aanShapefileLocation);

            // Increase limits:
            var luchtfotoLimits =
                Limits.RoundUp(new Limits(mapFrameAan.xMin, mapFrameAan.xMax, mapFrameAan.yMin, mapFrameAan.yMax), 50);
            // Get luchtfoto:
            var luchtfotoLocation = ProcessTools.GetLuchtfotoImage(luchtfotoLimits, workingFolder);
            if (!File.Exists(aanShapefileLocation))
            {
                throw new Exception("Kon de Luchtfoto-data niet ophalen.");
            }

            _addProgress("De luchtfoto is opgehaald.");
            // Add luchtfoto:
            var mapFrameLuchtfoto = surferService.AddGeoreferencedImage(luchtfotoLocation);

            // Merge map frames:
            var mergedMapFrame = surferService.MergeMapFrames(mapFrameVelddata, mapFrameMonsterdata,
                mapFrameAan, mapFrameLuchtfoto);

            surferService.MakeMapFrameFit(mergedMapFrame);

            // Save result:
            surferService.SavePlotDocument(Path.Combine(workingFolder, "DataForBlanking.srf"));

            _addProgress("Het plot document met de AAN en Luchtfoto data wordt getoond.");

            // Toon Surfer:
            return surferService.ShowHideSurfer(true);
        }

        /// <summary>
        /// Create the nuclide grids and add them to the active map
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="veldDataLocation"></param>
        /// <param name="blankFileLocation"></param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colAlt"></param>
        /// <param name="colK40"></param>
        /// <param name="colU238"></param>
        /// <param name="colTh232"></param>
        /// <param name="colCs137"></param>
        /// <param name="colTc"></param>
        public void CreateNuclideGrids(string workingFolder, string veldDataLocation, string blankFileLocation,
            int colX, int colY, int colAlt, int colK40, int colU238, int colTh232, int colCs137, int colTc)
        {
            // TODO: Check inputs

            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode), workingFolder,
                _addProgress);

            var nuclideGridsFolder = Path.Combine(workingFolder, SurferConstants.NuclideGridsFolder);
            if (!Directory.Exists(nuclideGridsFolder)) Directory.CreateDirectory(nuclideGridsFolder);

            var bufferedBlankFileLocation = string.Empty;
            IMapFrame mapBufferedBlankFile;

            try
            {
                // Create new plot document:
                surferService.CreateNewActivePlotDocument();
                _addProgress("Nieuw plot document aangemaakt.");
                // Add blank file to get the limits:
                var mapBlankFileTmp = surferService.AddShapefile(blankFileLocation);
                _addProgress("Perceelsgrens toegevoegd.");
                if (!File.Exists(bufferedBlankFileLocation))
                {
                    // Buffer blank file:
                    const int bufferDistance = 10;
                    bufferedBlankFileLocation = surferService.BufferPolygon(mapBlankFileTmp, bufferDistance);
                    if (!File.Exists(bufferedBlankFileLocation))
                    {
                        const string msg = @"Het maken van een buffer om de perceelsgrens is niet goed gegaan.";
                        _addProgress(msg);
                        MessageBox.Show(msg);
                        return;
                    }
                    else
                    {
                        _addProgress($"De perceelsgrens heeft een buffer van {bufferDistance} meter gekregen.");
                    }
                }

                _projectFile.FieldBorderLocationBuffered = bufferedBlankFileLocation;

                mapBufferedBlankFile = surferService.AddShapefile(bufferedBlankFileLocation);

                var altGrid = CreateNuclideGrid("Alt.grd", "Altitude", colAlt);
                var k40Grid = CreateNuclideGrid("K40.grd", "K-40", colK40);
                var u238Grid = CreateNuclideGrid("U238.grd", "U-238", colU238);
                var th232Grid = CreateNuclideGrid("Th232.grd", "Th-232", colTh232);
                var cs137Grid = CreateNuclideGrid("Cs137.grd", "Cs-137", colCs137);
                var tcGrid = CreateNuclideGrid("TC.grd", "TC", colTc);

                _projectFile.NuclideGridLocations.Alt = altGrid;
                _projectFile.NuclideGridLocations.K40 = k40Grid;
                _projectFile.NuclideGridLocations.U238 = u238Grid;
                _projectFile.NuclideGridLocations.Th232 = th232Grid;
                _projectFile.NuclideGridLocations.Cs137 = cs137Grid;
                _projectFile.NuclideGridLocations.Tc = tcGrid;
                _projectFile.Save();

                // Delete temp map frames:
                mapBlankFileTmp.Delete();
                mapBufferedBlankFile.Delete();

                // Show maps nicely on plot (resize, align horizontally
                surferService.AlignNuclideGrids();
                surferService.SavePlotDocument(Path.Combine(nuclideGridsFolder, "nuclideGrids.srf"));
                _addProgress("Alle nuclide grids zijn geplaatst op het plot.");
            }
            catch (Exception e)
            {
                var msg = @"Er ging iets mis bij het maken van de nuclide grids. Error: " + e.Message;
                _addProgress(msg);
                MessageBox.Show(msg);
            }
            finally
            {
                surferService.ShowHideSurfer(true);
            }

            string CreateNuclideGrid(string fileName, string layerName, int colZ)
            {
                // Load blank file:
                var mapBlankFile = surferService.AddShapefile(blankFileLocation);

                // Temp file:
                var tmpGridLocation = Path.Combine(workingFolder, "tmpGrid.grd");
                // Final file:
                var outGridLocation = Path.Combine(nuclideGridsFolder, fileName);

                // Grid velddata:
                surferService.InverseDistanceGridding(veldDataLocation, tmpGridLocation, colX, colY, colZ,
                    Limits.RoundUp(
                        new Limits(mapBufferedBlankFile.xMin, mapBufferedBlankFile.xMax, mapBufferedBlankFile.yMin,
                            mapBufferedBlankFile.yMax), 50));
                _addProgress($"Het grid van {layerName} is berekend.");
                // Blank using buffered file:
                if (!surferService.GridAssignNoData(tmpGridLocation, bufferedBlankFileLocation, outGridLocation))
                    return "";

                // Clean-up tmp file:
                File.Delete(tmpGridLocation);
                // Add contour layer:
                var mapContour = surferService.AddContour(outGridLocation, layerName, true);
                var mergedMapFrame = surferService.MergeMapFrames(mapBlankFile, mapContour);

                // Group:
                surferService.DeselectAll();
                // Get the contour layer:
                var numOverlays = mergedMapFrame.Overlays.Count;
                for (var i = 1; i <= numOverlays; i++)
                {
                    if (mergedMapFrame.Overlays.Item(i) is not IContourLayer contourLayer) continue;

                    // Select the color scale:
                    contourLayer.ColorScale.Selected = true;
                    // Move:
                    contourLayer.SetZOrder(SrfZOrder.srfZOToBack);
                    break;
                }

                mergedMapFrame.Selected = true;
                surferService.GroupSelection(layerName);

                _addProgress($"Het grid van {layerName} is toegevoegd aan de plot.");

                return outGridLocation;
            }
        }

        /// <summary>
        /// Get the default formulas from the application settings
        /// </summary>
        /// <returns></returns>
        public List<FormulaData> GetDefaultFormulas()
        {
            // If application settings has no formulas, use these hard-coded ones:
            var retVal = new List<FormulaData>
        {
            new("Lutum", "0.7*Th232-4", "Th232", "", "", "", "", "", "Lutum 0-10.lvl"),
            new("Zandfractie", "-0.8*(Th232 + U238) + 103", "Th232", "U238", "", "", "", "", "Zandfractie 75-100.lvl"),
            new("Leem", "100-Zandfractie", "Zandfractie", "", "", "", "", "", "Leem 0 30 Oud.lvl"),
            new("M0", "-0.5*K40+250", "K40", "", "", "", "", "", "M0 0-130.lvl"),
            new("M50", "-0.36709706918763*K40+235", "K40", "", "", "", "", "", "M50 150 250.lvl"),
            new("OS", "5.23297521874561 -0.0235 * Cs137 -0.012 * K40", "Cs137", "K40", "", "", "", "", "OS 0-5.lvl"),
            new("pH", "1.21876485586487 + 0.0299 * Th232 + 0.4333 * U238", "Th232", "U238", "", "", "5.5", "6.9",
                "Ph 4-7 0.25.lvl"),
            new("K-getal", "191.122454275834 -0.7771 * TC", "TC", "", "", "", "15", "29", "K-getal.lvl"),
            new("PW", "52.3556817192375 -7.7197 * Th232 + 7.3653 * U238", "Th232", "U238", "", "", "40", "75",
                "Pw.lvl"),
            new("Mg", "77.4222684918912 + 1.8301 * Th232 -2.8296 * U238", "Th232", "U238", "", "", "57", "71",
                "Mg 0-125 25.lvl"),
            new("Stikstof", "4067.52188017669 -13.2469 * TC+60", "TC", "", "", "", "1040", "1290",
                "Bgr 0-2000 200.lvl"),
            new("Bulkdichtheid", "Bulkdichtheid", "", "", "", "", "", "", "Bulkdichtheid.lvl"),
            new("Waterretentie", "Waterretentie", "", "", "", "", "", "", "Waterretentie 20-30.lvl"),
            new("Veldcapaciteit", "Veldcapaciteit", "", "", "", "", "", "", "Veldcapaciteit 0.3-0.38 0.1.lvl"),
            new("Waterdoorlatendheid", "Waterdoorlatendheid", "", "", "", "", "", "", "Waterdoorlatendheid 0-50 5.lvl"),
            new("Bodemclassificatie", "Bodemclassificatie Niet-Eolisch", "", "", "", "", "", "",
                "Bodemclassificatie Niet-Eolisch.lvl"),
            new("Slemp", "Slemp", "", "", "", "", "", "", "Slemp.lvl"),
            new("Monsterpunten", "TC", "TC", "", "", "", "", "", "Tc 200 250.lvl"),
            new("Ligging", "Alt", "Alt", "", "", "", "", "", "Ligging 2-7.lvl")
        };

            // TODO: Save to application settings file:

            return retVal;
        }

        /// <summary>
        /// Calculate the new grids and add them as contour file to the template plot document
        /// </summary>
        /// <param name="selectedFormulas"></param>
        public void CreateSoilMaps(List<FormulaData> selectedFormulas)
        {
            _addProgress("Het maken van de bodembestanden is gestart.");
            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode),
                _projectFile.WorkingFolder, _addProgress);

            foreach (var formula in selectedFormulas)
            {
                try
                {
                    if (surferService.CalcGrid(formula))
                        _addProgress($"{formula.Output} is berekend.");
                    else
                        _addProgress($"Er ging iets niet goed bij het berekenen van {formula.Output}.");
                }
                catch (Exception e)
                {
                    _addProgress($"Kan {formula.Output} niet berekenen. Error: {e.Message}");
                    // Swallow: throw;
                }
            }
        }
    }
}
