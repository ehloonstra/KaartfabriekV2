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

        /// <summary>
        /// Constructor
        /// </summary>
        public SurferService(string epsgCode)
        {
            _epsgCode = epsgCode;
            GetSurferObject();
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
        /// <param name="columnIndexZ">The column index of the Z-value</param>
        /// <returns>True o success, false otherwise</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public bool InverseDistanceGridding(string csvFileLocation, string newGridFilename, int columnIndexZ)
        {
            // D:\dev\TopX\Loonstra\Svn\Surfer\trunk\clsSurfer.cls r1159

            // Fixed variables:
            const int searchMinData = 8;
            const int searchMaxData = 64;

            // For now constants, should be flexible:
            const int searchRadius = 30; // in meters
            const int searchNumSectors = 1;
            const int xCol = 1;
            const int yCol = 2;
            const int idPower = 2;
            const int idSmoothing = 20;
            const float gridSpacing = 3.5f; // in meters

            if (!File.Exists(csvFileLocation))
                throw new FileNotFoundException("Griddata file not found", csvFileLocation);

            // TODO: Add limits:

            _surferApp.ScreenUpdating = false;
            try
            {
                return _surferApp.GridData6(DataFile: csvFileLocation, OutGrid: newGridFilename,
                    SearchEnable: true, SearchNumSectors: searchNumSectors, SearchRad1: searchRadius,
                    SearchRad2: searchRadius,
                    Algorithm: SrfGridAlgorithm.srfInverseDistance,
                    xCol: xCol, yCol: yCol, zCol: columnIndexZ, IDPower: idPower, IDSmoothing: idSmoothing,
                    SearchMinData: searchMinData, SearchDataPerSect: searchMaxData / searchNumSectors,
                    SearchMaxEmpty: Math.Max(1, searchNumSectors - 1), SearchMaxData: searchMaxData,
                    xSize: gridSpacing, ySize: gridSpacing,
                    OutGridOptions: "UseDefaults=1, ForgetOptions=1, SaveRefInfoAsGSIREF=1",
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
                    throw new Exception("Cannot get baseMapLayer");

                // Line properties:
                baseMapLayer.Line.ForeColor = srfColor.srfColorPurple;
                baseMapLayer.Line.Width = 0.01;

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
                _activePlotDocument.Selection.DeselectAll();
                foreach (var mapFrame in mapFrames)
                {
                    mapFrame.Selected = true;
                }

                var newMapFrame = _activePlotDocument.Selection.OverlayMaps();
                _activePlotDocument.Selection.DeselectAll();

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
                // Load the color file:
                postMapLayer.SymbolColorMap.LoadFile(Path.Combine(GetSurferSamplesLocation(), "Rainbow.clr"));
                postMapLayer.SymbolColorMethod = SrfSymbolColorMethod.srfSymbolColorMethodGradient;
                postMapLayer.SymbolColorCol = zColumn; // Returns/sets the symbol color column.
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


        //public IPlotDocument OpenTemplatePlotDocument(string templatePath)
        //{
        //    if (!File.Exists(templatePath))
        //        throw new FileNotFoundException("Cannot find template plot document", templatePath);

        //    _surferApp.Documents.Open()

        //}


        /// <summary>
        /// Get the samples folder of Surfer for the color maps
        /// </summary>
        /// <returns></returns>
        public string GetSurferSamplesLocation()
        {
            return Path.Combine(_surferApp.Path, "Samples");
        }
    }
}
