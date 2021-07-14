using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
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
            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode), workingFolder,
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

            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode), workingFolder,
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
        /// <param name="levelFilesFolder"></param>
        /// <param name="colorRow"></param>
        public void CreateSoilMaps(List<FormulaData> selectedFormulas, string levelFilesFolder,
            Action<int, Color> colorRow)
        {
            _addProgress("Het maken van de bodembestanden is gestart.");
            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode),
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
                        if (!surferService.CalcGrid(formula, _projectFile.ParcelData.Name, _projectFile.Gwt))
                        {
                            _addProgress($"Er ging iets niet goed bij het berekenen van {formula.Output}.");
                            continue;
                        }

                        _addProgress($"{formula.Output} is berekend.");
                        // Open template:
                        surferService.OpenSrf(templateLocation);
                        surferService.SaveAsPlotDocument(Path.Combine(_projectFile.WorkingFolder,
                            SurferConstants.BodemkaartenResultaatSurferFolder, $"{_projectFile.ParcelData.Name} {formula.Output}.srf"));
                        // Change grid
                        var statistics = surferService.ChangeGridSource(SurferConstants.TemplateMapName,
                            Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenGridsFolder,
                                $"{_projectFile.ParcelData.Name} {formula.Output}.grd"),
                            Path.Combine(levelFilesFolder, formula.LevelFile));

                        // Change text:
                        if (statistics is not null)
                        {
                            var mean = Math.Round(statistics.Mean, 1, MidpointRounding.AwayFromZero);
                            surferService.ChangeText("Gemiddelde", $"Gemiddelde: {mean}");
                        }

                        surferService.SetSoilMapData(formula.Output);

                        // For Monsterdata additional steps are required
                        if (formula.Formula == FormulaConstants.Monsterpunten)
                        {
                            var postLayer = surferService.AddPostLayer(SurferConstants.TemplateMapName,
                                _projectFile.FieldDataFileLocationProjected, "Velddata");
                            postLayer.Symbol.Index = 0; //	Returns/sets the glyph index.
                            postLayer.Symbol.Size = 0.05;
                            postLayer.Symbol.Color = srfColor.srfColorBlack40;

                            // Move to just above the contour layer:
                            postLayer.SetZOrder(SrfZOrder.srfZOToBack);
                            postLayer.SetZOrder(SrfZOrder.srfZOForward); // Above background image
                            postLayer.SetZOrder(SrfZOrder.srfZOForward); // Above contour

                            // Make sure monster data layer is visible:
                            surferService.SetLayerVisibility(SurferConstants.TemplateMapName, "Monster data", true);
                        }

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
            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode),
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
                surferService.ChangeText("Perceelnr", _projectFile.ProjectNr);
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

        /// <summary>
        /// Convert XML project file from older version to JSON project file for this version.
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        /// <exception cref="XmlException"></exception>
        public bool ConvertOldProjectFile(string xmlFile)
        {
            var doc = XDocument.Load(xmlFile);

            var nawNode = doc.Root?.Element("NAW");
            if (nawNode is null) throw new XmlException("Cannot find NAW node");
            _projectFile.ParcelData.Customer = nawNode.Element("Naam")?.Value;
            _projectFile.ParcelData.Name = nawNode.Element("Perceelnaam")?.Value;

            var projectgegevensNode = doc.Root?.Element("Projectgegevens");
            if (projectgegevensNode is null) throw new XmlException("Cannot find Projectgegevens node");
            _projectFile.ProjectNr = projectgegevensNode.Element("Projectnr")?.Value;
            _projectFile.ParcelData.Number = projectgegevensNode.Element("Perceelnr")?.Value;
            _projectFile.ParcelData.Size = projectgegevensNode.Element("Oppervlakte")?.Value.Replace("ha", "").Trim();
            _projectFile.Gwt = projectgegevensNode.Element("Grondwatertrap")?.Value;

            var gridsettingsNode = doc.Root?.Element("Gridsettings");
            if (gridsettingsNode is null) throw new XmlException("Cannot find Gridsettings node");
            _projectFile.GridSettings.SearchNumSectors = gridsettingsNode.Element("SearchNumberSectors")?.Value;
            _projectFile.GridSettings.SearchRadius = gridsettingsNode.Element("SearchRadius1")?.Element("RD")?.Value;
            _projectFile.GridSettings.IdPower = gridsettingsNode.Element("InverseDistancePower")?.Value;
            _projectFile.GridSettings.IdSmoothing = gridsettingsNode.Element("InverseDistanceSmoothing")?.Value;
            _projectFile.GridSettings.GridSpacing = gridsettingsNode.Element("GridSpacingX")?.Value;
            _projectFile.GridSettings.Limits = gridsettingsNode.Element("Limits")?.Element("RD")?.Element("xMin")?.Value;

            var formulesNode = doc.Root?.Element("Formules")?.Element("Formules");
            if (formulesNode is null) throw new XmlException("Cannot find Formules node");

            var formules = _projectFile.FormulaData;
            foreach (var element in formulesNode.Descendants("Formule"))
            {
                var output = GetCorrectGridName(element.Element("GridC")?.Value);
                if (SkipFormula(output)) continue;

                var gridA = GetCorrectGridName(element.Element("GridA")?.Value);
                var gridB = GetCorrectGridName(element.Element("GridB")?.Value);
                var gridC = GetCorrectGridName(element.Element("GridD")?.Value);
                var gridD = GetCorrectGridName(element.Element("GridE")?.Value);
                var min = element.Element("Min")?.Value;
                var max = element.Element("Max")?.Value;
                var lvlFile = element.Element("LvlFile")?.Value;
                if (output.Trim().ToLower() == "veldcapaciteit") lvlFile = "Veldcapaciteit 15-35 2.5.lvl";

                var function = ConvertFormulaFunction(element.Element("Function")?.Value, output, gridA, gridB, gridC, gridD);

                if (string.IsNullOrEmpty(function)) continue;

                _projectFile.FormulaData.Add(new FormulaData(output, function, gridA, gridB, gridC, gridD, min, max,
                    lvlFile));
            }

            return true;
        }

        private bool SkipFormula(string output)
        {
            if (output.Trim().ToLower() == "uitspoelingsgevoeligheid") return true;
            if (output.Trim().ToLower() == "pratylenchus penetrans") return true;
            if (output.Trim().ToLower() == "trichodorus") return true;
            if (output.Trim().ToLower() == "chitwoodi") return true;
            if (output.Trim().ToLower() == "schurft") return true;
            if (output.Trim().ToLower() == "stuifgevoeligheid") return true;
            if (output.Trim().ToLower().StartsWith("bodemclassificatie")) return true;

            return false;
        }

        private string ConvertFormulaFunction(string formula, string output, string gridA, string gridB, string gridC, string gridD)
        {
            if (output.Trim().ToLower() == "monsterpunten") return FormulaConstants.Monsterpunten;
            if (output.Trim().ToLower() == "ligging") return FormulaConstants.Alt;
            if (output.Trim().ToLower() == "bulkdichtheid") return FormulaConstants.Bulkdichtheid;
            if (output.Trim().ToLower() == "waterretentie") return FormulaConstants.Waterretentie;
            if (output.Trim().ToLower() == "veldcapaciteit") return FormulaConstants.Veldcapaciteit;
            if (output.Trim().ToLower() == "waterdoorlatendheid") return FormulaConstants.Waterdoorlatendheid;
            if (output.Trim().ToLower() == "slemp") return FormulaConstants.Slemp;

            // First strip 'C='
            var retVal = formula.Replace("C=", "").Trim();
            // Now replace 'a' variable:
            retVal = retVal.Replace("a", $"{gridA}");
            // If exist, replace 'b' variable:
            if (!string.IsNullOrEmpty(gridB))
                retVal = retVal.Replace("b", $"{gridB}");
            // 'c' variable is output
            // If exist, replace 'd' variable:
            if (!string.IsNullOrEmpty(gridC))
                retVal = retVal.Replace("d", $"{gridC}");
            // If exist, replace 'c' variable:
            if (!string.IsNullOrEmpty(gridD))
                retVal = retVal.Replace("e", $"{gridD}");

            return retVal.Trim();
        }

        private string GetCorrectGridName(string gridName)
        {
            if (string.IsNullOrEmpty(gridName)) return string.Empty;

            // Alt;Cs137;K40;TC;Th232;U238;CaCO3;K-getal;Ligging; Lutum; M0; M50; Mg; Mn; Monsterpunten;
            // OS; P-Al; pH; PW; Stikstof; Zandfractie; Bulkdichtheid; Slemp; Veldcapaciteit; Waterdoorlatendheid; Waterretentie

            if (gridName.Trim().ToLower() == "os") return FormulaConstants.Os;
            if (gridName.Trim().ToLower() == "cs") return FormulaConstants.Cs137;
            if (gridName.Trim().ToLower() == "k") return FormulaConstants.K40;
            if (gridName.Trim().ToLower() == "u") return FormulaConstants.U238;
            if (gridName.Trim().ToLower() == "th") return FormulaConstants.Th232;
            if (gridName.Trim().ToLower() == "tc") return FormulaConstants.Tc;

            return gridName;
        }

        /// <summary>
        /// Export all soilmaps to EMF
        /// </summary>
        public void ExportEmf()
        {
            var resultFolder = Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenResultaatEmfFolder);
            if (!Directory.Exists(resultFolder)) Directory.CreateDirectory(resultFolder);

            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode),
                _projectFile.WorkingFolder, _addProgress, false);

            // Get all soilmaps from formula section of project file:
            foreach (var formulaData in _projectFile.FormulaData)
            {
                var fileName = Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenResultaatSurferFolder,
                    $"{_projectFile.ParcelData.Name} {formulaData.Output}.srf");

                if (File.Exists(fileName))
                {

                    var result = surferService.ExportAsEmf(fileName, resultFolder);
                    _addProgress(result
                        ? $"{formulaData.Output} is geëxporteerd."
                        : $"Er ging wat fout bij het exporteren van {formulaData.Output}");
                }
                else
                {
                    _addProgress($"{formulaData.Output}.srf bestaat niet en wordt niet geëxporteerd.");
                }

            }
        }

        /// <summary>
        /// Export all grids using in the formulas as 1 csv file.
        /// </summary>
        public void ExportCsv()
        {
            var resultFolder = Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenResultaatCsvFolder);
            if (!Directory.Exists(resultFolder)) Directory.CreateDirectory(resultFolder);

            var datFilesDict = new Dictionary<string, string>();

            var surferService = new SurferService(SurferConstants.GetCoordinateSystemName(_projectFile.EpsgCode),
                _projectFile.WorkingFolder, _addProgress, false);

            // Get all nuclide grids:
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.Alt, resultFolder, out var newFileName))
                datFilesDict.Add(FormulaConstants.Alt, newFileName);
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.K40, resultFolder, out newFileName))
                datFilesDict.Add(FormulaConstants.K40, newFileName);
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.Cs137, resultFolder, out newFileName))
                datFilesDict.Add(FormulaConstants.Cs137, newFileName);
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.Th232, resultFolder, out newFileName))
                datFilesDict.Add(FormulaConstants.Th232, newFileName);           
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.U238, resultFolder, out newFileName))
                datFilesDict.Add(FormulaConstants.U238, newFileName);           
            if (ExportNuclideGrid(surferService, _projectFile.NuclideGridLocations.Tc, resultFolder, out newFileName))
                datFilesDict.Add(FormulaConstants.Tc, newFileName);

            // Get all soilmaps from formula section of project file:
            foreach (var formulaData in _projectFile.FormulaData)
            {
                var fileName = Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenGridsFolder,
                    $"{_projectFile.ParcelData.Name} {formulaData.Output}.grd");

                if (ExportNuclideGrid(surferService, fileName, resultFolder, out newFileName))
                    datFilesDict.Add(formulaData.Output, newFileName);

                //var fileName = Path.Combine(_projectFile.WorkingFolder, SurferConstants.BodemkaartenGridsFolder,
                //    $"{_projectFile.ParcelData.Name} {formulaData.Output}.grd");

                //if (File.Exists(fileName))
                //{
                //    var newFileName = Path.Combine(resultFolder, Path.ChangeExtension(Path.GetFileName(fileName), ".dat"));

                //    var result = surferService.ExportAsDatFile(fileName, newFileName);
                //    _addProgress(result
                //        ? $"{formulaData.Output} is geëxporteerd."
                //        : $"Er ging wat fout bij het exporteren van {formulaData.Output}");
                //    if (result) datFilesDict.Add(formulaData.Output, newFileName);
                //}
                //else
                //{
                //    _addProgress($"{formulaData.Output}.grd bestaat niet en wordt niet geëxporteerd.");
                //}
            }

            // Now process each dat file and create 1 csv file:
            var csvFileName = Path.Combine(resultFolder,
                $"{_projectFile.ParcelData.Customer} {_projectFile.ParcelData.Name}.csv");
            surferService.DeleteFile(csvFileName);
            MergeDatFilesIntoCsv(datFilesDict, csvFileName);
        }

        private bool ExportNuclideGrid(SurferService surferService, string fileName, string resultFolder, out string newFileName )
        {
            var baseFileName = Path.GetFileName(fileName);
            newFileName = string.Empty;

            if (File.Exists(fileName))
            {
                newFileName = Path.Combine(resultFolder, Path.ChangeExtension(baseFileName, ".dat"));

                var result = surferService.ExportAsDatFile(fileName, newFileName);
                _addProgress(result
                    ? $"{baseFileName} is geëxporteerd."
                    : $"Er ging wat fout bij het exporteren van {baseFileName}");
                return result;
            }

            _addProgress($"{baseFileName}.grd bestaat niet en wordt niet geëxporteerd.");
            return false;
        }

        private void MergeDatFilesIntoCsv(Dictionary<string, string> datFilesDict, string csvFileName)
        {
            var lines = new List<StringBuilder>();

            // Open all dat files and extract last column:
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = " ",
                WhiteSpaceChars = Array.Empty<char>()
            };

            // Read all files:
            var fileNum = 0;
            foreach (var (key, value) in datFilesDict)
            {
                using var reader = new StreamReader(value);
                using var csv = new CsvReader(reader, config);
                var lineNumber = 0;
                var rows = csv.GetRecords<DatStructure>();
                foreach (var row in rows)
                {
                    if (fileNum == 0)
                    {
                        // First file
                        if (lineNumber == 0)
                        {
                            // Header
                            lines.Add(new StringBuilder());
                            lines[0].Append($"X,Y,{key}");
                            lineNumber++;
                        }

                        lines.Add(new StringBuilder());
                        lines[lineNumber]
                            .Append($"{row.X.ToString("0.##", CultureInfo.InvariantCulture)},{row.Y.ToString("0.##", CultureInfo.InvariantCulture)},{row.Z.ToString("0.####", CultureInfo.InvariantCulture)}");
                    }
                    else
                    {
                        // Next file
                        if (lineNumber == 0)
                        {
                            // Header
                            lines[0].Append($",{key}");
                            lineNumber++;
                        }

                        lines[lineNumber].Append($",{row.Z.ToString("0.####", CultureInfo.InvariantCulture)}");
                    }

                    lineNumber++;
                }

                fileNum++;
            }

            // Convert the list of string builders to 1 string builder:
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line.ToString());
            }
            // Write file:
            File.WriteAllText(csvFileName, sb.ToString());
            if (!File.Exists(csvFileName))
                _addProgress("Kon het CSV-bestand niet maken: " + csvFileName);
        }
    }
}
