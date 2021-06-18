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
        private readonly Application _surferApp = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public SurferService()
        {
            _surferApp.PageUnits = SrfPageUnits.srfUnitsCentimeter;
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
        public IPlotDocument AddPlotDocument()
        {
            try
            {
                IPlotDocument plot = _surferApp.Documents.Add(SrfDocTypes.srfDocPlot);

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
        /// <param name="plotDocument"></param>
        /// <param name="csvFileLocation"></param>
        /// <param name="zCol"></param>
        /// <param name="xCol"></param>
        /// <param name="yCol"></param>
        /// <param name="limitIncrease">How much should the map from limits be increased</param>
        /// <returns>Returns a MapFrame object.</returns>
        public IMapFrame AddPostMap(IPlotDocument plotDocument, string csvFileLocation, int zCol, int xCol = 1, int yCol = 2, int limitIncrease = 20)
        {
            _surferApp.ScreenUpdating = false;

            try
            {
                var mapFrame = plotDocument.Shapes.AddPostMap2(csvFileLocation, xCol, yCol, zCol);

                // Increase limits:
                mapFrame.SetLimits(Math.Floor(mapFrame.xMin) - limitIncrease, Math.Floor(mapFrame.xMax) + limitIncrease,
                    Math.Floor(mapFrame.yMin) - limitIncrease, Math.Floor(mapFrame.yMax) + limitIncrease);

                // Make it fit on the page:
                var ratioHeight = (plotDocument.PageSetup.Height - plotDocument.PageSetup.TopMargin - plotDocument.PageSetup.BottomMargin) / mapFrame.Height;
                var ratioWidth = (plotDocument.PageSetup.Width - plotDocument.PageSetup.LeftMargin - plotDocument.PageSetup.RightMargin) / mapFrame.Width;
                var ratio = Math.Min(ratioHeight, ratioWidth);
                mapFrame.Height *= ratio;
                mapFrame.Width *= ratio;
                mapFrame.Top = plotDocument.PageSetup.Height - plotDocument.PageSetup.TopMargin;

                // Get the added layer:
                if (mapFrame.Overlays.Item(1) is not IPostLayer2 postMapLayer)
                    throw new Exception("Cannot get postMapLayer");

                // Change its name
                postMapLayer.Name = "K-40";

                // Set symbols:
                postMapLayer.Symbol.Index = 12; //	Returns/sets the glyph index.
                postMapLayer.Symbol.Color = srfColor.srfColorRed;
                postMapLayer.Symbol.Size = 0.15; // Returns/sets the symbol height in page units.
                postMapLayer.SymCol = 0;  // Returns/sets the column containing the symbol type (0 if none).

                // Set rainbow coloring:
                postMapLayer.SymbolColorMethod = SrfSymbolColorMethod.srfSymbolColorMethodGradient;
                // TODO: LoadPreset doesn't exists:  postMapLayer.SymbolColorMap.LoadPreset("Rainbow")
                // TODO: Results in hiding the data: postMapLayer.SymbolColorCol = zCol; // Returns/sets the symbol color column.

                // No labels
                postMapLayer.LabCol = 0; // Returns/sets the column containing the labels (0 if none).

                // Set the coordinate system:
                postMapLayer.CoordinateSystem = SurferConstants.Epsg28992;

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

    }
}
