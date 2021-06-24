using System;
using System.IO;
using Surfer;

namespace SurferTools
{
    /// <summary>
    /// Service to interact with the Surfer application using COM
    /// </summary>
    public class SurferService
    {
        private readonly string _epsgCode;
        private IPlotDocument3 _activePlotDocument;
        private Application _surferApp;

        private const string OutGridOptions = "UseDefaults=1, ForgetOptions=1, SaveRefInfoAsInternal=1, SaveRefInfoAsGSIREF=1";


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="epsgCode">The epsg code for all layers</param>
        /// <param name="workingFolder">The working folder</param>
        public SurferService(string epsgCode, string workingFolder)
        {
            _epsgCode = epsgCode;
            GetSurferObject();
            _surferApp.DefaultFilePath = workingFolder;
        }

        private void GetSurferObject()
        {
            var obj = ComTools.GetActiveObject("Surfer.Application");
            if (obj is null)
            {
                // Surfer is not loaded, create new instance
                _surferApp = new Application { PageUnits = SrfPageUnits.srfUnitsCentimeter };
                _activePlotDocument = AddPlotDocument();
            }
            else
            {
                _surferApp = obj as Application;
                if (_surferApp != null)
                {
                    _activePlotDocument = _surferApp.ActiveDocument ?? AddPlotDocument();
                }
            }
        }

        /// <summary>
        /// Show or hide the Surfer application
        /// </summary>
        /// <param name="visible">If its value is set to 'true', the surfer application window is visible and if its value is set to 'false', the application window is hidden.</param>
        /// <returns>The application window visibility</returns>
        public bool ShowHideSurfer(bool visible) => _surferApp.Visible = visible;

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
        /// Save the plot document to disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>True on success, false otherwise</returns>
        public bool SavePlotDocument(string fileName)
        {
            return _activePlotDocument.SaveAs(fileName, "", SrfSaveFormat.srfSaveFormatSrf15);
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
        /// <returns>True on success, false otherwise</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public bool InverseDistanceGridding(string csvFileLocation, string newGridFilename, int colX, int colY, int colZ, Limits limits)
        {
            // D:\dev\TopX\Loonstra\Svn\Surfer\trunk\clsSurfer.cls r1159

            // Fixed variables:
            const int searchMinData = 8;
            const int searchMaxData = 64;

            // For now constants, should be flexible:
            const int searchRadius = 30; // in meters
            const int searchNumSectors = 1;
            const int idPower = 2;
            const int idSmoothing = 20;
            const float gridSpacing = 3.5f; // in meters

            if (!File.Exists(csvFileLocation))
                throw new FileNotFoundException("Griddata file not found", csvFileLocation);

            _surferApp.ScreenUpdating = false;
            try
            {
                return _surferApp.GridData6(DataFile: csvFileLocation, OutGrid: newGridFilename,
                    SearchEnable: true, SearchNumSectors: searchNumSectors, SearchRad1: searchRadius,
                    SearchRad2: searchRadius,
                    xCol: colX, yCol: colY, zCol: colZ,
                    Algorithm: SrfGridAlgorithm.srfInverseDistance, IDPower: idPower, IDSmoothing: idSmoothing,
                    SearchMinData: searchMinData, SearchDataPerSect: searchMaxData / searchNumSectors,
                    SearchMaxEmpty: Math.Max(1, searchNumSectors - 1), SearchMaxData: searchMaxData,
                    xSize: gridSpacing, ySize: gridSpacing,
                    xMin: limits.Xmin, yMin: limits.Ymin, xMax: limits.Xmax, yMax: limits.Ymax,
                    OutGridOptions: OutGridOptions,
                    ShowReport: false);
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
                    OutGrid: outputFileLocation, OutGridOptions: OutGridOptions);
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
        public IMapFrame AddContour(string gridFileLocation, string layerName, bool showColorScale)
        {
            _surferApp.ScreenUpdating = false;
            try
            {
                var mapFrame = _activePlotDocument.Shapes.AddContourMap(gridFileLocation);

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IContourLayer contourLayer)
                    throw new Exception("Cannot get contourLayer");

                contourLayer.CoordinateSystem = _epsgCode;
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
        public IMapFrame AddPostMap(string csvFileLocation, string layerName, int xCol = 1, int yCol = 2)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                var mapFrame = _activePlotDocument.Shapes.AddPostMap2(csvFileLocation, xCol, yCol);

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IPostLayer2 postMapLayer)
                    throw new Exception("Cannot get postMapLayer");

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
                postMapLayer.CoordinateSystem = _epsgCode;

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
        /// Add shapefile as base layer
        /// </summary>
        /// <param name="sfLocation">The location of the shapefile</param>
        /// <returns>The new map frame</returns>
        public IMapFrame AddShapefile(string sfLocation)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                var mapFrame = _activePlotDocument.Shapes.AddBaseMap(sfLocation);
                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IBaseLayer baseMapLayer)
                    throw new Exception("Cannot get BaseMapLayer");

                // Line properties:
                baseMapLayer.Line.ForeColor = srfColor.srfColorPurple;
                baseMapLayer.Line.Width = 0.02;

                baseMapLayer.CoordinateSystem = _epsgCode;

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
        public IMapFrame AddGeoreferencedImage(string imageLocation)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                var mapFrame = _activePlotDocument.Shapes.AddBaseMap(imageLocation);
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
        public IMapFrame MergeMapFrames(params IMapFrame[] mapFrames)
        {
            _surferApp.ScreenUpdating = false;
            try
            {
                DeselectAll();
                foreach (var mapFrame in mapFrames)
                {
                    mapFrame.Selected = true;
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
        public void MakeMapFrameFit(IMapFrame mapFrame)
        {
            // Make it fit on the page:
            var pageSetup = _activePlotDocument.PageSetup;
            var ratioHeight = (pageSetup.Height - pageSetup.TopMargin - pageSetup.BottomMargin) / mapFrame.Height;
            var ratioWidth = (pageSetup.Width - pageSetup.LeftMargin - pageSetup.RightMargin) / mapFrame.Width;
            var ratio = Math.Min(ratioHeight, ratioWidth);
            mapFrame.Height *= ratio;
            mapFrame.Width *= ratio;
            mapFrame.Top = pageSetup.Height - pageSetup.TopMargin;
            mapFrame.Left = pageSetup.LeftMargin;
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
                // Load the color file:
                postMapLayer.SymbolColorMap.LoadFile(Path.Combine(GetSurferSamplesLocation(), "Rainbow.clr"));
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
        /// <param name="labColumn"></param>
        public void SetLabelMonsterdataPostmap(IPostLayer2 postMapLayer, int labColumn)
        {
            postMapLayer.LabCol = labColumn;
        }

        private static void LogException(Exception exception, string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine(exception.Message);
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
            return Path.Combine(_surferApp.Path, "Samples");
        }

        /// <summary>
        /// Buffer a polygon
        /// </summary>
        /// <param name="mapFrame"></param>
        /// <param name="bufferDistance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string BufferPolygon(IMapFrame mapFrame, int bufferDistance)
        {
            if (mapFrame.Overlays.Item(1) is not IBaseLayer baseMapLayer)
                throw new Exception("Cannot get baseMapLayer");

            Console.WriteLine(baseMapLayer.Shapes.Count);
            Console.WriteLine(baseMapLayer.Shapes.Item(1).GetType());

            return "TODO";

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
    }
}
