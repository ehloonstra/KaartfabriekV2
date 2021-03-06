using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Shared;
using Surfer;

namespace SurferTools
{
    /// <summary>
    /// Service to interact with the Surfer application using COM
    /// </summary>
    public class SurferService
    {
        private readonly string _coordinateSystem;
        private readonly string _workingFolder;
        private readonly Action<string> _addProgress;
        private IPlotDocument3 _activePlotDocument;
        private IApplication5 _surferApp;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system for all layers</param>
        /// <param name="workingFolder">The working folder</param>
        /// <param name="addProgress"></param>
        /// <param name="addPlotDocument"></param>
        public SurferService(string coordinateSystem, string workingFolder, Action<string> addProgress, bool addPlotDocument = true)
        {
            _coordinateSystem = coordinateSystem;
            _workingFolder = workingFolder;
            _addProgress = addProgress;

            GetSurferObject(addPlotDocument);
            try
            {
                // Sometimes it fails
                _surferApp.DefaultFilePath = workingFolder;
            }
            catch (Exception e)
            {
                _addProgress("Kan de default file path for surfer niet zetten. Error: " + e.Message);
                // swallow throw;
            }
        }

        /// <summary>
        /// Screen updating
        /// </summary>
        public bool ScreenUpdating
        {
            get => _surferApp.ScreenUpdating;
            set => _surferApp.ScreenUpdating = value;
        }

        private void GetSurferObject(bool addPlotDocument)
        {
            var obj = ComTools.GetActiveObject("Surfer.Application");
            if (obj is null)
            {
                // Surfer is not loaded, create new instance
                _surferApp = new Application { PageUnits = SrfPageUnits.srfUnitsCentimeter };
                if (addPlotDocument) _activePlotDocument = AddPlotDocument();
            }
            else
            {
                _surferApp = obj as IApplication5;
                if (_surferApp == null) return;

                if (addPlotDocument)
                    _activePlotDocument = _surferApp.ActiveDocument ?? AddPlotDocument();
            }
        }

        /// <summary>
        /// Show or hide the Surfer application
        /// </summary>
        /// <param name="visible">If its value is set to 'true', the surfer application window is visible and if its value is set to 'false', the application window is hidden.</param>
        /// <returns>The application window visibility</returns>
        public bool ShowHideSurfer(bool visible)
        {
            if (visible)
            {
                if (_surferApp.ActiveWindow is not null)
                    _surferApp.ActiveWindow.Zoom(SrfZoomTypes.srfZoomPage);
                _surferApp.ScreenUpdating = true;
            }

            _surferApp.Visible = visible;
            return _surferApp.Visible;
        }

        /// <summary>
        /// Get the application window visibility
        /// </summary>
        public bool IsVisible => _surferApp.Visible;

        /// <summary>
        /// Terminates the Surfer application.
        /// </summary>
        public void QuitSurfer() => _surferApp.Quit();

        /// <summary>
        /// Get the number of documents open
        /// </summary>
        /// <returns>The number of documents open</returns>
        public int GetNumDocuments() => _surferApp.Documents.Count;

        /// <summary>
        /// Create a plot document
        /// </summary>
        /// <returns>The created plot document</returns>
        private IPlotDocument3 AddPlotDocument()
        {
            try
            {
                IPlotDocument3 plot = _surferApp.Documents.Add();

                // Set page orientation:
                plot.PageSetup.Orientation = SrfPaperOrientation.srfLandscape;

                plot.Activate();
                return plot;
            }
            catch (Exception e)
            {
                LogException(e, "Error in AddPlotDocument");
                throw;
            }
        }

        /// <summary>
        /// Create new plot document and activate it
        /// </summary>
        public void CreateNewActivePlotDocument()
        {
            var plot = AddPlotDocument();
            _activePlotDocument = plot;
        }

        /// <summary>
        /// Save the active plot document to disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>True on success, false otherwise</returns>
        public bool SaveAsPlotDocument(string fileName)
        {
            return _activePlotDocument.SaveAs(fileName, "", SrfSaveFormat.srfSaveFormatSrf15);
        }

        /// <summary>
        /// Save the active plot document
        /// </summary>
        /// <returns></returns>
        public bool SavePlotDocument()
        {
            return _activePlotDocument.Save();
        }

        /// <summary>
        /// Creates a grid from irregularly spaced XYZ data using the Inverse Distance to a Power gridding method
        /// </summary>
        /// <param name="csvFileLocation">The location of the file with the irregularly spaced XYZ data, coordinates must be in meter (RD or UTM)</param>
        /// <param name="newGridFilename">The location of the newly created file</param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colZ">The column index of the Z-value</param>
        /// <param name="limits">The limits</param>
        /// <param name="gridSettings"></param>
        /// <returns>True on success, false otherwise</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public bool InverseDistanceGridding(string csvFileLocation, string newGridFilename, int colX, int colY,
            int colZ, Limits limits, GridSettings gridSettings)
        {
            // D:\dev\TopX\Loonstra\Svn\Surfer\trunk\clsSurfer.cls r1159

            if (!File.Exists(csvFileLocation))
                throw new FileNotFoundException("Griddata file not found", csvFileLocation);

            var searchMaxData = Convert.ToInt32(gridSettings.SearchMaxData);
            var searchNumSectors = Convert.ToInt32(gridSettings.SearchNumSectors);
            var gridSpacing = Convert.ToDouble(gridSettings.GridSpacing, CultureInfo.InvariantCulture);

            _surferApp.ScreenUpdating = false;
            try
            {
                var retVal = _surferApp.GridData6(DataFile: csvFileLocation, OutGrid: newGridFilename,
                    SearchEnable: true, SearchNumSectors: gridSettings.SearchNumSectors, SearchRad1: gridSettings.SearchRadius,
                    SearchRad2: gridSettings.SearchRadius,
                    xCol: colX, yCol: colY, zCol: colZ,
                    Algorithm: SrfGridAlgorithm.srfInverseDistance, IDPower: gridSettings.IdPower, IDSmoothing: gridSettings.IdSmoothing,
                    SearchMinData: gridSettings.SearchMinData, SearchDataPerSect: searchMaxData / searchNumSectors,
                    SearchMaxEmpty: Math.Max(1, searchNumSectors - 1), SearchMaxData: gridSettings.SearchMaxData,
                    xSize: gridSpacing, ySize: gridSpacing,
                    xMin: limits.Xmin, yMin: limits.Ymin, xMax: limits.Xmax, yMax: limits.Ymax,
                    OutGridOptions: SurferConstants.OutGridOptions,
                    ShowReport: false);

                if (!retVal) return false;
                AddCoordinateSystemToGrid(newGridFilename);

                return true;
            }
            catch (Exception e)
            {
                LogException(e, "Error in GridData6");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }


        /// <summary>
        /// Set the grid values outside the blank file to NoData
        /// </summary>
        /// <param name="gridFileLocation"></param>
        /// <param name="blankFileLocation"></param>
        /// <param name="outputFileLocation"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public bool GridAssignNoData(string gridFileLocation, string blankFileLocation, string outputFileLocation)
        {
            try
            {
                if (!File.Exists(gridFileLocation)) throw new FileNotFoundException("Cannot find input grid");
                if (!File.Exists(blankFileLocation)) throw new FileNotFoundException("Cannot find blank file");

                return _surferApp.GridAssignNoData(InGrid: gridFileLocation, NoDataFile: blankFileLocation,
                    Side: SrfNoDataPolygonSide.srfNoDataPolyMixed,
                    OutGrid: outputFileLocation, OutGridOptions: SurferConstants.OutGridOptions);
            }
            catch (Exception e)
            {
                LogException(e, "Error in GridAssignNoData");
                throw;
            }
        }

        /// <summary>
        /// Add the contour lines of the grid
        /// </summary>
        /// <param name="gridFileLocation"></param>
        /// <param name="layerName"></param>
        /// <param name="showColorScale"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IMapFrame3 AddContour(string gridFileLocation, string layerName, bool showColorScale)
        {
            _surferApp.ScreenUpdating = false;
            try
            {
                if (_activePlotDocument.Shapes.AddContourMap(gridFileLocation) is not IMapFrame3 mapFrame)
                    throw new Exception("Cannot add contourLayer");

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IContourLayer contourLayer)
                    throw new Exception("Cannot get contourLayer");

                contourLayer.CoordinateSystem = _coordinateSystem;
                contourLayer.FillContours = true;
                contourLayer.SmoothContours = SrfConSmoothType.srfConSmoothHigh;
                contourLayer.Name = layerName;

                contourLayer.ShowMajorLabels = false;
                contourLayer.ShowMinorLabels = false;

                contourLayer.ShowColorScale = showColorScale;
                if (showColorScale)
                {
                    contourLayer.ColorScale.Title = layerName;
                    contourLayer.ColorScale.TitlePosition = SrfColorScaleTitlePosition.srfColorScaleTitlePositionBottom;
                    contourLayer.ColorScale.TitleOffsetVertical = 1d;
                    contourLayer.ColorScale.TitleAngle = 0;

                    contourLayer.ColorScale.Top = mapFrame.Top - mapFrame.Height + contourLayer.ColorScale.Height + 1; // Align bottom
                    contourLayer.ColorScale.Left = mapFrame.Left + mapFrame.Width;
                }

                return mapFrame;
            }
            catch (Exception e)
            {
                LogException(e, "Error in AddContour");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }

        /// <summary>
        /// Group the selection into an IComposite
        /// </summary>
        /// <param name="name">The name of the group</param>
        public void GroupSelection(string name)
        {
            var group = _activePlotDocument.Selection.Combine();
            group.Name = name;
            DeselectAll();
        }


        /// <summary>
        /// Add a postmap
        /// </summary>
        /// <param name="csvFileLocation"></param>
        /// <param name="layerName">The name of the layer in Surfer</param>
        /// <param name="xCol"></param>
        /// <param name="yCol"></param>
        /// <returns>Returns a MapFrame object.</returns>
        public IMapFrame3 AddPostMap(string csvFileLocation, string layerName, int xCol = 1, int yCol = 2)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                if (_activePlotDocument.Shapes.AddPostMap2(csvFileLocation, xCol, yCol) is not IMapFrame3 mapFrame)
                    throw new Exception("Cannot add postMapLayer");

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IPostLayer2 postMapLayer)
                    throw new Exception("Cannot get postMapLayer");

                _addProgress($"De PostMap {layerName} is toegevoegd.");

                // Change its name
                postMapLayer.Name = layerName;

                // Set default symbol settings:
                postMapLayer.Symbol.Index = 12; //	Returns/sets the glyph index.
                postMapLayer.Symbol.Color = srfColor.srfColorRed;
                postMapLayer.Symbol.Size = 0.15; // Returns/sets the symbol height in page units.
                postMapLayer.SymCol = 0;  // Returns/sets the column containing the symbol type (0 if none).

                // No labels
                postMapLayer.LabCol = 0; // Returns/sets the column containing the labels (0 if none).

                // Set the coordinate system:
                postMapLayer.CoordinateSystem = _coordinateSystem;

                return mapFrame;
            }
            catch (Exception e)
            {
                LogException(e, "Error in AddPostMap");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }

        /// <summary>
        /// Add PostMap to existing map frame
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="csvFileLocation"></param>
        /// <param name="layerName"></param>
        /// <param name="xCol"></param>
        /// <param name="yCol"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public IPostLayer2 AddPostLayer(string mapName, string csvFileLocation, string layerName, int xCol = 1, int yCol = 2)
        {
            if (!File.Exists(csvFileLocation))
                throw new FileNotFoundException("Cannot find data file", csvFileLocation);

            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            if (shapes.AddPostLayer(mapFrame, csvFileLocation, xCol, yCol) is not IPostLayer2 postLayer)
                throw new Exception("Could not add data as post layer");

            postLayer.Name = layerName;
            postLayer.CoordinateSystem = _coordinateSystem;

            // Set default symbol settings:
            postLayer.Symbol.Index = 12; //	Returns/sets the glyph index.
            postLayer.Symbol.Color = srfColor.srfColorRed;
            postLayer.Symbol.Size = 0.15; // Returns/sets the symbol height in page units.
            postLayer.SymCol = 0;  // Returns/sets the column containing the symbol type (0 if none).

            // No labels
            postLayer.LabCol = 0; // Returns/sets the column containing the labels (0 if none).

            return postLayer;
        }

        /// <summary>
        /// Add shapefile as base layer
        /// </summary>
        /// <param name="sfLocation">The location of the shapefile</param>
        /// <param name="foreColor">The line color</param>
        /// <returns>The new map frame</returns>
        public IMapFrame3 AddShapefile(string sfLocation, srfColor foreColor = srfColor.srfColorPurple)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                if (!File.Exists(sfLocation))
                    throw new FileNotFoundException("Could not find shapefile", sfLocation);

                if (_activePlotDocument.Shapes.AddBaseMap(sfLocation) is not IMapFrame3 mapFrame)
                    throw new Exception("Cannot add BaseMapLayer");

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IBaseLayer baseMapLayer)
                    throw new Exception("Cannot get BaseMapLayer");

                // Line properties:
                baseMapLayer.Line.ForeColor = foreColor;
                baseMapLayer.Line.Width = 0.01;

                baseMapLayer.CoordinateSystem = _coordinateSystem;

                return mapFrame;
            }
            catch (Exception e)
            {
                LogException(e, "Error in AddShapefile");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }


        /// <summary>
        /// Add georeferenced image (i.e. luchtfoto)
        /// </summary>
        /// <param name="imageLocation"></param>
        /// <returns></returns>
        public IMapFrame3 AddGeoreferencedImage(string imageLocation)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                if (_activePlotDocument.Shapes.AddBaseMap(imageLocation) is not IMapFrame3 mapFrame)
                    throw new Exception("Cannot add BaseMapLayer");

                return mapFrame;
            }
            catch (Exception e)
            {
                LogException(e, "Error in AddShapefile");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }

        /// <summary>
        /// Merge all map frames into one map frame
        /// </summary>
        /// <param name="mapFrames">The list of map frames</param>
        /// <returns>The new map frame</returns>
        public IMapFrame MergeMapFrames(params IMapFrame3[] mapFrames)
        {
            _surferApp.ScreenUpdating = false;
            try
            {
                DeselectAll();
                foreach (var mapFrame in mapFrames)
                {
                    mapFrame.Selected = true;
                    mapFrame.CoordinateSystem = _coordinateSystem;
                }

                var newMapFrame = _activePlotDocument.Selection.OverlayMaps();
                DeselectAll();

                // Change axis:
                newMapFrame.Axes.Item("Right Axis").MajorTickType = SrfTickType.srfTickNone;
                newMapFrame.Axes.Item("Top Axis").MajorTickType = SrfTickType.srfTickNone;

                return newMapFrame;
            }
            catch (Exception e)
            {
                LogException(e, "Error in MergeMapFrames");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }

        /// <summary>
        /// Deselect all on active plotdocument
        /// </summary>
        public void DeselectAll()
        {
            _activePlotDocument.Selection.DeselectAll();
        }

        /// <summary>
        /// Make the map frame fit nicely on the page
        /// </summary>
        /// <param name="mapFrame"></param>
        /// <param name="endWidth"></param>
        /// <param name="startHeight"></param>
        public double MakeMapFrameFit(IMapFrame mapFrame, double endWidth = 0d, double startHeight = 0d)
        {
            // Make it fit on the page:
            var pageSetup = _activePlotDocument.PageSetup;

            LabelsAssenWijzigen(false, mapFrame.Axes);

            double ratioWidth;
            if (endWidth == 0d)
            {
                ratioWidth = (pageSetup.Width - pageSetup.LeftMargin - pageSetup.RightMargin) / mapFrame.Width;
            }
            else
            {
                ratioWidth = (endWidth - pageSetup.LeftMargin) / mapFrame.Width;
            }

            mapFrame.Axes.Item("Left Axis").ShowLabels = false;
            double ratioHeight;
            if (startHeight == 0d)
            {
                ratioHeight = (pageSetup.Height - pageSetup.TopMargin - pageSetup.BottomMargin) / mapFrame.Height;
            }
            else
            {
                ratioHeight = (pageSetup.Height - pageSetup.TopMargin - startHeight) / mapFrame.Height;
            }
            var ratio = Math.Min(ratioHeight, ratioWidth);
            ratio *= 0.9; // Make the map slightly smaller
            mapFrame.Height *= ratio;
            mapFrame.Width *= ratio;
            mapFrame.Top = pageSetup.Height - pageSetup.TopMargin;
            mapFrame.Axes.Item("Left Axis").ShowLabels = true;
            mapFrame.Left = pageSetup.LeftMargin;
            mapFrame.Axes.Item("Bottom Axis").ShowLabels = true;
            mapFrame.Axes.Item("Bottom Axis").MajorTickType = SrfTickType.srfTickOut;

            return ratio;
        }

        /// <summary>
        /// Set Post map layer symbology coloring
        /// </summary>
        /// <param name="postMapLayer"></param>
        /// <param name="zColumn"></param>
        public void SetColoringVelddataPostmap(IPostLayer2 postMapLayer, int zColumn)
        {
            if (postMapLayer is null) return;

            _surferApp.ScreenUpdating = false;
            try
            {
                // Set rainbow coloring:
                postMapLayer.SymbolColorCol = zColumn; // Returns/sets the symbol color column.
                postMapLayer.SymbolColorMethod = SrfSymbolColorMethod.srfSymbolColorMethodGradient;

                if (postMapLayer.SymbolColorMap is not IColorMap2 colorMap)
                    throw new Exception("Cannot get colorMap");
                colorMap.LoadPreset("Rainbow");

                // Force reload:
                postMapLayer.DataFile = postMapLayer.DataFile;

                _addProgress($"De kleuren zijn toegepast op de PostMap {postMapLayer.Name}");
            }
            catch (Exception e)
            {
                LogException(e, "Error in SetColoringVelddataPostmap");
                throw;
            }
            finally
            {
                _surferApp.ScreenUpdating = true;
            }
        }

        /// <summary>
        /// Enable the labels for the monsterdata Post map
        /// </summary>
        /// <param name="postMapLayer"></param>
        /// <param name="labelColumn"></param>
        public void SetLabelMonsterdataPostmap(IPostLayer2 postMapLayer, int labelColumn)
        {
            postMapLayer.LabCol = labelColumn;
            postMapLayer.Symbol.Index = 12;
        }

        private void LogException(Exception exception, string msg)
        {
            _addProgress(msg);
            var innerEx = exception.InnerException;
            while (innerEx is not null)
            {
                Console.WriteLine(innerEx.Message);
                innerEx = innerEx.InnerException;
            }
        }

        /// <summary>
        /// Get the samples folder of Surfer for the color maps
        /// </summary>
        /// <returns></returns>
        public string GetSurferSamplesLocation()
        {
            var folder = Path.Combine(_surferApp.Path, "Samples");
            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException("Cannot find the Surfer Samples folder");

            return folder;
        }

        /// <summary>
        /// Get the system.gsj location which contains the EPSG code 
        /// </summary>
        /// <returns></returns>
        public string GetSystemGsjLocation()
        {
            var file = Path.Combine(_surferApp.Path, "system.gsj");
            if (!File.Exists(file))
                throw new FileNotFoundException("Cannot find system.gsj", file);

            return file;
        }

        /// <summary>
        /// Buffer a polygon
        /// </summary>
        /// <param name="mapFrame"></param>
        /// <param name="bufferDistance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string BufferPolygon(IMapFrame mapFrame, double bufferDistance)
        {
            if (mapFrame.Overlays.Item(1) is not IVectorBaseLayer2 baseMapLayer)
                throw new Exception("Cannot get IVectorBaseLayer2");

            if (baseMapLayer.Shapes is not IShapes7 shapes)
                throw new Exception("Cannot get shapes");

            DeselectAll();

            shapes.StartEditing();

            if (baseMapLayer.Shapes.Item(1) is not IPolygon2 polygon)
                throw new Exception("Cannot get polygon");

            polygon.Selected = true;

            if (_activePlotDocument.Selection is not ISelection3 plotSelection)
                throw new Exception("Cannot get plot selection");

            plotSelection.Buffer(1, bufferDistance);

            if (baseMapLayer.Shapes.Item(baseMapLayer.Shapes.Count) is not IPolygon2 bufferedPolygon)
                throw new Exception("Cannot get buffered polygon");

            bufferedPolygon.Name = "Buffer Polygon";
            bufferedPolygon.Line.ForeColorRGBA.Color = srfColor.srfColorForestGreen;
            bufferedPolygon.SetZOrder(SrfZOrder.srfZOToFront);

            // Save buffered polygon:
            DeselectAll();
            bufferedPolygon.Selected = true;
            var newFilename = Path.ChangeExtension(baseMapLayer.FileName, ".buffered.bln");
            _activePlotDocument.Export(newFilename, true);

            bufferedPolygon.Delete();

            shapes.StopEditing();

            return newFilename;
        }

        /// <summary>
        /// Align the 6 maps nicely on the plot
        /// </summary>
        public void AlignNuclideGrids()
        {
            DeselectAll();
            var scale = 1d;
            var pageSetup = _activePlotDocument.PageSetup;
            var previousWidths = 0d;
            const float gap = 0.5f;
            var mapOnRow = 0;

            var numMaps = _activePlotDocument.Shapes.Count;
            for (var i = 1; i <= numMaps; i++)
            {
                if (_activePlotDocument.Shapes.Item(i) is not IComposite group)
                    continue;

                if (i == 1)
                {
                    var ratioHeight = (pageSetup.Height / 2 - pageSetup.TopMargin - pageSetup.BottomMargin) /
                                      group.Height;
                    var ratioWidth = ((pageSetup.Width / 3) - pageSetup.LeftMargin - pageSetup.RightMargin) /
                                     group.Width;
                    scale = Math.Min(ratioHeight, ratioWidth);
                }

                // Scale:
                group.Height *= scale;
                group.Width *= scale;

                if (i <= 3)
                {
                    group.Top = pageSetup.Height - pageSetup.TopMargin;
                }
                else
                {
                    group.Top = pageSetup.Height - pageSetup.TopMargin - group.Height - gap;
                }
                group.Left = pageSetup.LeftMargin + previousWidths + mapOnRow * gap;
                mapOnRow++;

                // Save previous widths:
                previousWidths += group.Width;

                if (i != 3) continue;

                // Reset, start at the beginning:
                previousWidths = 0d;
                mapOnRow = 0;
            }
        }

        /// <summary>
        /// Calculate a new grid based on the formula data
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="fieldName"></param>
        /// <param name="gwt"></param>
        public bool CalcGrid(FormulaData formula, string fieldName, string gwt)
        {
            var outputFolder = Path.Combine(_workingFolder, SurferConstants.BodemkaartenGridsFolder);
            if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

            var outGrid = Path.Combine(outputFolder, $"{fieldName} {formula.Output}.grd");

            //  Check for special formulas like Bodemclassificatie, Waterretentie, etc.
            var specialCalculations = new SpecialCalculations(_workingFolder, fieldName, _surferApp);
            switch (formula.Formula)
            {
                case FormulaConstants.BodemclassificatieEolisch:
                    return false;
                case FormulaConstants.BodemclassificatieNietEolisch:
                    return false;
                case FormulaConstants.Bulkdichtheid:
                    return specialCalculations.CalculateBulkdichtheid(outGrid);
                case FormulaConstants.Slemp:
                    return specialCalculations.CalculateSlemp(outGrid, gwt);
                case FormulaConstants.Veldcapaciteit:
                    return specialCalculations.CalculateVeldcapaciteit(outGrid);
                case FormulaConstants.Waterdoorlatendheid:
                    return specialCalculations.CalculateWaterdoorlatendheid(outGrid);                
                case FormulaConstants.Aanvulpunt:
                    return specialCalculations.CalculateAanvulpunt(outGrid);
                case FormulaConstants.Waterretentie:
                    return specialCalculations.CalculateWaterretentie(outGrid);
                case FormulaConstants.Monsterpunten:
                    return specialCalculations.CalculateMonsterpunten(outGrid, formula.GridA);
            }

            var gridMathInput = new List<IGridMathInput>();

            if (!string.IsNullOrEmpty(formula.GridA))
                gridMathInput.Add(_surferApp.NewGridMathInput(GetFullPath(_workingFolder, fieldName, formula.GridA), formula.GridA));

            if (!string.IsNullOrEmpty(formula.GridB))
                gridMathInput.Add(_surferApp.NewGridMathInput(GetFullPath(_workingFolder, fieldName, formula.GridB), formula.GridB));

            if (!string.IsNullOrEmpty(formula.GridC))
                gridMathInput.Add(_surferApp.NewGridMathInput(GetFullPath(_workingFolder, fieldName, formula.GridC), formula.GridC));

            if (!string.IsNullOrEmpty(formula.GridD))
                gridMathInput.Add(_surferApp.NewGridMathInput(GetFullPath(_workingFolder, fieldName, formula.GridD), formula.GridD));

            _surferApp.GridMath3(formula.Formula, gridMathInput.ToArray(), outGrid);

            LimitGrid(outGrid, formula.Minimum, formula.Maximum);

            // Add coordinate system
            AddCoordinateSystemToGrid(outGrid);

            return File.Exists(outGrid);
        }

        private void AddCoordinateSystemToGrid(string gridLocation)
        {
            if (!File.Exists(gridLocation)) return;

            try
            {
                if (_surferApp.NewGrid() is not IGrid3 grid) return;

                // Set coordinate system:
                grid.LoadFile2(gridLocation);
                grid.CoordinateSystem = _coordinateSystem;
                grid.SaveFile2(gridLocation, Options: SurferConstants.OutGridOptions);
                // TODO: Shouldn't close?
            }
            catch (Exception e)
            {
                throw new Exception("Error in AddCoordinateSystemToGrid", e);
            }
        }

        private void LimitGrid(string inGrid, string mimimumString, string maximumString)
        {
            var limitsSet = false;
            var formula = string.Empty;
            if (!string.IsNullOrEmpty(mimimumString) && string.IsNullOrEmpty(maximumString))
            {
                // Minimum is set, maximum not
                var minimum = mimimumString.Replace(',', '.');
                limitsSet = true;
                formula = $"IF(A<{minimum},{minimum},A)";
            }
            if (string.IsNullOrEmpty(mimimumString) && !string.IsNullOrEmpty(maximumString))
            {
                // Maximum is set, minimum is not
                var maximum = maximumString.Replace(',', '.');
                limitsSet = true;
                formula = $"IF(A>{maximum},{maximum},A)";
            }

            if (!string.IsNullOrEmpty(mimimumString) && !string.IsNullOrEmpty(maximumString))
            {
                // Maximum AND minimum are set
                var maximum = maximumString.Replace(',', '.');
                var minimum = mimimumString.Replace(',', '.');
                limitsSet = true;
                formula = $"IF(A<{minimum},{minimum},IF(A>{maximum},{maximum},A))";
            }

            if (!limitsSet) return;

            _addProgress("Limiting het grid.");
            var tmpOutGrid = Path.ChangeExtension(inGrid, ".limited.grd");
            var gridMathInput = new List<IGridMathInput>(1) { _surferApp.NewGridMathInput(inGrid, "A") };
            _surferApp.GridMath3(formula, gridMathInput.ToArray(), tmpOutGrid);

            if (!File.Exists(tmpOutGrid)) return;

            _addProgress("Limited grid is aangemaakt");
            // Move new file:
            File.Move(tmpOutGrid, inGrid, true);
        }

        /// <summary>
        /// Look for the grid file in several folders
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="fieldName"></param>
        /// <param name="gridName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFullPath(string workingFolder, string fieldName, string gridName)
        {
            // First check in the nuclide grids folder:
            var fullPath = Path.Combine(workingFolder, SurferConstants.NuclideGridsFolder, $"{gridName}.grd");
            if (File.Exists(fullPath)) return fullPath;

            // Next try the results folder:
            fullPath = Path.Combine(workingFolder, SurferConstants.BodemkaartenGridsFolder, $"{fieldName} {gridName}.grd");
            if (File.Exists(fullPath)) return fullPath;

            throw new Exception("Cannot find grid for GridMath with name " + gridName);
        }


        /// <summary>
        /// Open the .srf file
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public void OpenSrf(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                throw new FileNotFoundException("Could not find the .srf file to open.", fileLocation);

            if (_surferApp.Documents.Open2(fileLocation) is not IPlotDocument3 plot)
                throw new Exception("Could not open .srf file");

            plot.Activate();
            _activePlotDocument = plot;
        }

        /// <summary>
        /// Change the text in the active plot document
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void ChangeText(string label, string value)
        {
            if (_activePlotDocument.Shapes.Item(label) is not IText text)
                throw new Exception("Could not find text with name " + label);

            text.Text = value;
            text.Visible = true;
        }

        /// <summary>
        /// Change the grid source
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="newGridLocation"></param>
        /// <param name="lvlFile"></param>
        /// <returns>The grid statistics</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public IGridStatistics ChangeGridSource(string mapName, string newGridLocation, string lvlFile = "")
        {
            if (!File.Exists(newGridLocation))
                throw new FileNotFoundException("Cannot find new grid", newGridLocation);

            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            if (mapFrame.Overlays.Item("Contours") is not IContourLayer contourLayer)
                throw new Exception("Cannot get contourLayer");

            try
            {
                contourLayer.GridFile = newGridLocation;
            }
            catch (Exception e)
            {
                throw new Exception("Error in changing grid source. Possibly horizontal planar", e);
            }

            contourLayer.CoordinateSystem = _coordinateSystem;

            if (mapFrame.CoordinateSystem != _coordinateSystem)
            {
                mapFrame.CoordinateSystem = _coordinateSystem;
                mapFrame.SetLimitsToData();
            }

            if (File.Exists(lvlFile))
            {
                contourLayer.Levels.LoadFile(lvlFile);
                if (Path.GetFileName(newGridLocation).EndsWith($"{FormulaConstants.Slemp}.grd"))
                {
                    contourLayer.ColorScale.LabelFormat.NumDigits = 2;
                    contourLayer.ColorScale.LabelFormat.Type = SrfLabelType.srfLabFixed;
                }
            }
            else if (!string.IsNullOrEmpty(lvlFile))
            {
                _addProgress($"Kon {Path.GetFileName(lvlFile)} niet vinden.");
            }

            return (contourLayer.Grid as IGrid3)?.Statistics();
        }

        /// <summary>
        /// Add the blank file to an existing map frame
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="blankFileLocation"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public void AddBlankFile(string mapName, string blankFileLocation)
        {
            if (!File.Exists(blankFileLocation))
                throw new FileNotFoundException("Cannot find blank file", blankFileLocation);

            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            var baseLayer = shapes.AddBaseLayer(mapFrame, blankFileLocation);
            if (baseLayer is null)
                throw new Exception("Could not get blank file layer");

            // Line properties:
            baseLayer.Line.ForeColor = srfColor.srfColorBlack;
            baseLayer.Line.Width = 0.01;

            baseLayer.CoordinateSystem = _coordinateSystem;
        }

        /// <summary>
        /// Add sample points to existing map frame
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="sampleDataLocation"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public void AddSamplePoints(string mapName, string sampleDataLocation)
        {
            if (!File.Exists(sampleDataLocation))
                throw new FileNotFoundException("Cannot find sample data file", sampleDataLocation);

            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            if (shapes.AddPostLayer(mapFrame, sampleDataLocation) is not IPostLayer2 postLayer)
                throw new Exception("Could not add sample data");

            postLayer.Name = "Monster data";
            postLayer.CoordinateSystem = _coordinateSystem;
            SetLabelMonsterdataPostmap(postLayer, 3);
        }

        /// <summary>
        /// Size the map frame to fit it nicely in the template
        /// </summary>
        /// <param name="mapName"></param>
        /// <exception cref="Exception"></exception>
        public void SizeMapFrameForTemplate(string mapName)
        {
            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            if (shapes.Item("Dimensions") is not IComposite2 groupDimensions)
                throw new Exception("Could not get shape Dimensions");

            if (groupDimensions.Shapes.Item("dimTop") is not IShape dimTop)
                throw new Exception("Could not find dimTop");

            if (groupDimensions.Shapes.Item("dimBottom") is not IShape dimBottom)
                throw new Exception("Could not find dimBottom");

            var ratio = MakeMapFrameFit(mapFrame, dimTop.Left, dimBottom.Top);

            // De labels en ticks van de assen verpoeren de breedte,
            // dus eerst uitzetten:
            LabelsAssenWijzigen(false, mapFrame.Axes);

            // Reset:
            mapFrame.Left = dimBottom.Left;
            mapFrame.Top = dimTop.Top;

            // Center map:
            if (dimTop.Left - dimBottom.Left > mapFrame.Width)
                mapFrame.Left += (dimTop.Left - mapFrame.Left - mapFrame.Width) / 2;
            if (dimTop.Top - dimBottom.Top > mapFrame.Height)
                mapFrame.Top -= (dimTop.Top - dimBottom.Top - mapFrame.Height) / 2;

            const int roundUp = 10;

            // Workaround To Get the real xMapPerPU And yMapPerPU:
            var (realXMapPerPu, realYMapPerPu) = GetRealxMapPerPuAndyMapPerPu(mapFrame);

            // Check if the width needs to be enlarged:
            if (dimTop.Left - dimBottom.Left > mapFrame.Width)
            {
                //var xMin = mapFrame.Axes.Item("Left Axis").Cross1 - mapFrame.Left * mapFrame.yMapPerPU;
                var xMin = mapFrame.Axes.Item("Left Axis").Cross1 
                           - (mapFrame.Left - dimBottom.Left) * realXMapPerPu;
                // xMin = RoundUp(xMin, roundUp); // 162870
                mapFrame.Axes.Item("Left Axis").SetScale(Cross1: xMin);

                // Reset:
                mapFrame.Left = dimBottom.Left;

                var xMax = mapFrame.Axes.Item("Right Axis").Cross1 +
                           ((dimTop.Left - mapFrame.Left - mapFrame.Width) * realXMapPerPu); // 163370 163550
                mapFrame.Axes.Item("Right Axis").SetScale(Cross1: xMax);

                // Enlarge top and bottom axes:
                var firstMajorTick = RoundUp(xMin, roundUp);
                if (firstMajorTick <= xMin) firstMajorTick += roundUp;
                mapFrame.Axes.Item("Top Axis")
                    .SetScale(Minimum: xMin, FirstMajorTick: firstMajorTick, Maximum: xMax, LastMajorTick: xMax);
                mapFrame.Axes.Item("Bottom Axis").SetScale(Minimum: xMin, FirstMajorTick: firstMajorTick, Maximum: xMax,
                    LastMajorTick: xMax);
            }

            // Check if the height needs to be enlarged:
            if (dimTop.Top - dimBottom.Top > mapFrame.Height)
            {
                // 527420
                var yMin = mapFrame.Axes.Item("Bottom Axis").Cross1 -
                           (mapFrame.Top - mapFrame.Height - dimBottom.Top) * realYMapPerPu;
                yMin = RoundUp(yMin, roundUp); // 527340
                mapFrame.Axes.Item("Bottom Axis").SetScale(Cross1: yMin);

                // Reset:
                mapFrame.Top = dimBottom.Top + mapFrame.Height;

                // 527960
                var yMax = mapFrame.Axes.Item("Top Axis").Cross1 +
                           (dimTop.Top - mapFrame.Top) * realYMapPerPu; // 528035 516752
                //yMax /= ratio;
                mapFrame.Axes.Item("Top Axis").SetScale(Cross1: yMax);

                // Enlarge left and right axes:
                var firstMajorTick = RoundUp(yMin, roundUp);
                if (firstMajorTick <= yMin) firstMajorTick += roundUp;
                mapFrame.Axes.Item("Left Axis")
                    .SetScale(Minimum: yMin, FirstMajorTick: firstMajorTick, Maximum: yMax, LastMajorTick: yMax);
                mapFrame.Axes.Item("Right Axis")
                    .SetScale(Minimum: yMin, FirstMajorTick: firstMajorTick, Maximum: yMax, LastMajorTick: yMax);
            }

            // Set limits of Map frame:
            mapFrame.SetLimits(mapFrame.Axes.Item("Bottom Axis").Minimum, mapFrame.Axes.Item("Bottom Axis").Maximum,
                mapFrame.Axes.Item("Left Axis").Minimum, mapFrame.Axes.Item("Left Axis").Maximum);

            // Reset location:
            mapFrame.Left = dimBottom.Left;
            mapFrame.Top = dimTop.Top;
            
            // The map scale is not properly scaled:
            if (shapes.Item("Map Scale") is IScaleBar3 mapScale)
            {
                mapScale.LabelFont.Size = 8;
                mapScale.CycleSpacing =
                    RoundUp((mapFrame.Axes.Item("Right Axis").Cross1 - mapFrame.Axes.Item("Left Axis").Cross1) / 8,
                        roundUp);
                mapScale.Left = mapFrame.Left + 1;
            }

            // Show labels again:
            LabelsAssenWijzigen(true, mapFrame.Axes);

            // Remove Dimensions:
#if !DEBUG
            groupDimensions.Delete();
#endif
        }

        private double RoundUp(double value, int roundUp)
        {
            return (Math.Round(value / roundUp, MidpointRounding.ToZero) * roundUp);
        }

        private (double, double) GetRealxMapPerPuAndyMapPerPu(IMapFrame3 mapFrame)
        {
            // Plot.Shapes needs to be cast to IShape7 to get acces to AddEmptyBaseLayer():
            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            // When the rectangle is added to the plot, page units are used for the parameter coordinates.
            // When the rectangle is added to a map layer, map coordinates are used for the parameters coordinates.

            try
            {
                // This always throws an invalid cast error, but the rectangle is added:
                shapes.AddRectangle(0d, 0d, 1d, 1d);
            }
            catch
            {
                // swallow
            }

            _activePlotDocument.Selection.DeselectAll();
            if (shapes.Item(shapes.Count) is not IPolygon2 polygon)
                throw new Exception("Could not get polygon");

            polygon.Select();
            Thread.Sleep(1000); // Sometimes Copy throws an error: Unable to open clipboard
            _activePlotDocument.Selection.Copy();

            var tempLayer = shapes.AddEmptyBaseLayer(mapFrame);

            if (tempLayer.Shapes is not IShapes7 shapesTempLayer)
                throw new Exception("Could not get shapes of temp layer");

            shapesTempLayer.StartEditing();
            Thread.Sleep(1000); // Sometimes Paste throws an error: Unable to open clipboard
            // Paste in the 1-inch rect into the map converting the page units coordinates to map units:
            shapesTempLayer.Paste();

            var realXMapPerPu = tempLayer.Shapes.Item(1).Width;
            var realYMapPerPu = tempLayer.Shapes.Item(1).Height;

            polygon.Delete(); //done with workaround, don't need this anymore
            tempLayer.Delete(); //done with workaround, don't need this anymore

            return (realXMapPerPu, realYMapPerPu);
        }

        private static void LabelsAssenWijzigen(bool enabled, IAxes axes)
        {
            if (enabled)
            {
                axes.Item("Bottom Axis").ShowLabels = true;
                axes.Item("Bottom Axis").MajorTickType = SrfTickType.srfTickOut;
                axes.Item("Left Axis").ShowLabels = true;
                axes.Item("Left Axis").MajorTickType = SrfTickType.srfTickOut;
            }
            else
            {
                axes.Item("Bottom Axis").ShowLabels = false;
                axes.Item("Bottom Axis").MajorTickType = SrfTickType.srfTickNone;
                axes.Item("Left Axis").ShowLabels = false;
                axes.Item("Left Axis").MajorTickType = SrfTickType.srfTickNone;
            }
        }

        /// <summary>
        /// Set the soil map data
        /// </summary>
        /// <param name="formulaOutput"></param>
        /// <exception cref="Exception"></exception>
        public void SetSoilMapData(string formulaOutput)
        {
            if (_activePlotDocument.Shapes is not IShapes7 shapes)
                throw new Exception("Could not get shapes");

            if (shapes.Item("Teksten") is not IComposite2 groupTeksten)
                throw new Exception("Could not get group Teksten");

            var soilMapName = formulaOutput;

            try
            {
                // Throws an exception when not found:
                if (groupTeksten.Shapes.Item(formulaOutput) is IComposite2 groupOutput)
                {
                    // Make group visible:
                    groupOutput.Visible = true;

                    // Get header from group
                    const string label = "Header";
                    if (groupOutput.Shapes.Item(label) is not IText header)
                        throw new Exception("Could not find text with name " + label);
                    header.Visible = false;
                    // Use the text from the template:
                    soilMapName = header.Text;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Swallow throw;
            }

            // Set type of map: Lutum, Zandfractie, etc:
            const string label2 = "Perceelgegevens";
            if (_activePlotDocument.Shapes.Item(label2) is not IText text)
                throw new Exception("Could not find text with name " + label2);

            text.Text = text.Text.Replace("Projectie: x", $"Projectie: {soilMapName}");
        }

        /// <summary>
        /// Set the visibility of the layer
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="layerName"></param>
        /// <param name="visible"></param>
        /// <exception cref="Exception"></exception>
        public void SetLayerVisibility(string mapName, string layerName, bool visible)
        {
            if (_activePlotDocument.Shapes.Item(mapName) is not IMapFrame3 mapFrame)
                throw new Exception("Could not find map with name " + mapName);

            try
            {
                var layer = mapFrame.Overlays.Item(layerName);
                if (layer is not null) layer.Visible = visible;
            }
            catch (Exception e)
            {
                _addProgress($"Could not find layer with name {layerName}. Error: {e.Message}");
                // swallow throw;
            }
        }

        /// <summary>
        /// Export active document as emf-file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resultFolder"></param>
        /// <returns></returns>
        public bool ExportAsEmf(string fileName, string resultFolder)
        {
            OpenSrf(fileName);

            // http://surferhelp.goldensoftware.com/subsys/subsys_hid_gsiemf2_scriptopt.htm?Highlight=emf
            // ReSharper disable once AssignNullToNotNullAttribute
            var newFileName = Path.Combine(resultFolder, Path.ChangeExtension(Path.GetFileName(fileName), ".emf"));

            var retVal = _activePlotDocument.Export2(newFileName, false, "Defaults=1,AllTextToPolygons=0,MaxBitmapSizeInMB=10", "emf");
            if (retVal)
            {
                _activePlotDocument.Close(SrfSaveTypes.srfSaveChangesNo);
            }

            return retVal;
        }

        /// <summary>
        /// Export Surfer grid file as DAT-file
        /// X, Y, Z space delimetered
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool ExportAsDatFile(string fileName, string newFileName)
        {
            try
            {
                if (_surferApp.NewGrid() is not IGrid3 grid) return false;

                DeleteFile(newFileName);

                grid.LoadFile2(fileName);

                // http://surferhelp.goldensoftware.com/subsys/XYZ_Grid_Export_Automation_Opt.htm
                grid.SaveFile2(newFileName, SrfGridFormat.srfGridFmtXYZ, "Defaults=1, NoBlanks=1");

                return File.Exists(newFileName);
            }
            catch (Exception e)
            {
                throw new Exception("Error in ExportAsDatFile", e);
            }
        }

        /// <summary>
        /// Delete the file if it exists
        /// </summary>
        /// <param name="fileLocation"></param>
        public void DeleteFile(string fileLocation)
        {
            if (File.Exists(fileLocation))
                File.Delete(fileLocation);
        }

        /// <summary>
        /// Append PointSample
        /// </summary>
        /// <param name="gridFileLocation"></param>
        /// <param name="csvLocation"></param>
        /// <param name="zCol"></param>
        /// <param name="columnName"></param>
        public void PointSample(string gridFileLocation, string csvLocation, int zCol, string columnName)
        {
            var wksDocument = _surferApp.PointSample(gridFileLocation, csvLocation, 1, 2, zCol);
            wksDocument.Cells(1, zCol).Value = columnName;
            wksDocument.SaveAs(csvLocation);
            wksDocument.Close();
        }

        /// <summary>
        /// Close the active plot document
        /// </summary>
        public void CloseActivePlot()
        {
            _activePlotDocument.Close(SrfSaveTypes.srfSaveChangesNo);
        }
    }
}
