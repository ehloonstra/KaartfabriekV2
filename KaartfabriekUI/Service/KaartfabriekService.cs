using System;
using System.Collections.Generic;
using System.Drawing;
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
            var aanShapefileLocation = new ProcessTools().GetAanData(Limits.RoundUp(velddataLimits, 1), workingFolder);
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
            var luchtfotoLocation = new ProcessTools().GetLuchtfotoImage(luchtfotoLimits, workingFolder);
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
            surferService.SaveAsPlotDocument(Path.Combine(workingFolder, "DataForBlanking.srf"));

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
        /// <param name="bufferDistance"></param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colAlt"></param>
        /// <param name="colK40"></param>
        /// <param name="colU238"></param>
        /// <param name="colTh232"></param>
        /// <param name="colCs137"></param>
        /// <param name="colTc"></param>
        public void CreateNuclideGrids(string workingFolder, string veldDataLocation, string blankFileLocation, double bufferDistance,
            int colX, int colY, int colAlt, int colK40, int colU238, int colTh232, int colCs137, int colTc)
        {
            // TODO: Check inputs

            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode), workingFolder,
                _addProgress, false);

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

                _projectFile.NuclideGridLocations ??= new NuclideGridLocations();

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
                surferService.SaveAsPlotDocument(Path.Combine(nuclideGridsFolder, "nuclideGrids.srf"));
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
                var limits = Convert.ToInt32(_projectFile.GridSettings.Limits);
                surferService.InverseDistanceGridding(veldDataLocation, tmpGridLocation, colX, colY, colZ,
                    Limits.RoundUp(
                        new Limits(mapBufferedBlankFile.xMin, mapBufferedBlankFile.xMax, mapBufferedBlankFile.yMin,
                            mapBufferedBlankFile.yMax), limits), _projectFile.GridSettings);
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
        /// Calculate the new grids and add them as contour file to the template plot document
        /// </summary>
        /// <param name="selectedFormulas"></param>
        /// <param name="colorRow"></param>
        public void CreateSoilMaps(List<FormulaData> selectedFormulas, Action<int, Color> colorRow)
        {
            _addProgress("Het maken van de bodembestanden is gestart.");
            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode),
                _projectFile.WorkingFolder, _addProgress, false);

            try
            {
                surferService.ScreenUpdating = false;

                // Check if template exists:
                var templateLocation = Path.Combine(_projectFile.WorkingFolder,
                    SurferConstants.BodemkaartenResultaatSurferFolder, SurferConstants.TemplateName);
                if (!File.Exists(templateLocation))
                    throw new FileNotFoundException($"Kan {SurferConstants.TemplateName} niet vinden.", templateLocation);

                foreach (var formula in selectedFormulas)
                {
                    try
                    {
                        colorRow(formula.RowIndex, Color.Blue);
                        if (!surferService.CalcGrid(formula))
                        {
                            _addProgress($"Er ging iets niet goed bij het berekenen van {formula.Output}.");
                            continue;
                        }

                        _addProgress($"{formula.Output} is berekend.");
                        // Open template:
                        surferService.OpenSrf(templateLocation);
                        surferService.SaveAsPlotDocument(Path.Combine(_projectFile.WorkingFolder,
                            SurferConstants.BodemkaartenResultaatSurferFolder, $"{formula.Output}.srf"));
                        // Change grid
                        var statistics = surferService.ChangeGridSource(SurferConstants.TemplateMapName,
                            Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenGridsFolder,
                                $"{formula.Output}.grd"),
                            Path.Combine(@"D:\dev\TopX\Loonstra\TSC Tools\Kaartfabriek\Steven", formula.LevelFile));

                        // Change text:
                        if (statistics is not null)
                        {
                            var mean = Math.Round(statistics.Mean, 1, MidpointRounding.AwayFromZero);
                            surferService.ChangeText("Gemiddelde", $"Gemiddelde: {mean}");
                        }

                        surferService.SetSoilMapData(formula.Output);

                        surferService.SavePlotDocument();
                        colorRow(formula.RowIndex, Color.DarkGreen);
                    }
                    catch (Exception e)
                    {
                        _addProgress($"Er ging wat fout bij het maken van {formula.Output}. Error: {e.Message}");
                        colorRow(formula.RowIndex, Color.DarkRed);
                        // Swallow: throw;
                    }

                }
            }
            finally
            {
                surferService.ShowHideSurfer(true);
            }
        }

        /// <summary>
        /// Create the template, which will be used for all soil maps
        /// </summary>
        /// <param name="surferTemplateLocation">The location of the base template</param>
        public void CreateTemplate(string surferTemplateLocation)
        {
            _addProgress("Het maken van de template is gestart.");
            var surferService = new SurferService(SurferConstants.GetProjectionName(_projectFile.EpsgCode),
                _projectFile.WorkingFolder, _addProgress, false);

            try
            {
                // Open template:
                surferService.OpenSrf(surferTemplateLocation);

                var folder = Helpers.CheckFolder(Path.Combine(_projectFile.WorkingFolder,
                    SurferConstants.BodemkaartenResultaatSurferFolder));

                // Copy base template to project:
                surferService.SaveAsPlotDocument(Path.Combine(folder, SurferConstants.TemplateName));

                // Change Perceelnr:
                surferService.ChangeText("Perceelnr", _projectFile.ParcelData.Number);
                // Change Perceelgegevens:
                var pData = _projectFile.ParcelData;
                var value = string.Format("Naam: {1}{0}{0}Perceel: {2}{0}{0}Omvang: {3} ha{0}{0}Projectie: x",
                    Environment.NewLine, pData.Customer, pData.Name, pData.Size);
                surferService.ChangeText("Perceelgegevens", value);


                // Change grid
                surferService.ChangeGridSource(SurferConstants.TemplateMapName, _projectFile.NuclideGridLocations.Tc);

                // Add blank file
                surferService.AddBlankFile(SurferConstants.TemplateMapName, _projectFile.FieldBorderLocation);

                // Add sample data:
                surferService.AddSamplePoints(SurferConstants.TemplateMapName, _projectFile.SampleDataFileLocationProjected);

                // Size frame to look good
                surferService.SizeMapFrameForTemplate(SurferConstants.TemplateMapName);

                // Save changes:
                surferService.SavePlotDocument();
            }
            catch (Exception e)
            {
                _addProgress("Error in CreateTemplate. Message: " + e.Message);
            }
            finally
            {
                // Show surfer:
                surferService.ShowHideSurfer(true);
            }
        }
    }
}
