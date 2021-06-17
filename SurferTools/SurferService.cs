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

            try
            {
                return _surferApp.GridData6(DataFile: csvFileLocation, OutGrid: newGridFilename,
                    SearchEnable: true, SearchNumSectors: searchNumSectors, SearchRad1: searchRadius, SearchRad2: searchRadius, 
                    Algorithm: SrfGridAlgorithm.srfInverseDistance,
                    xCol: xCol, yCol: yCol, zCol: columnIndexZ, IDPower: idPower, IDSmoothing: idSmoothing,
                    SearchMinData: searchMinData, SearchDataPerSect: searchMaxData / searchNumSectors,
                    SearchMaxEmpty: Math.Max(1, searchNumSectors - 1), SearchMaxData: searchMaxData,
                    xSize: gridSpacing, ySize: gridSpacing, ShowReport: false);
            }
            catch (Exception e)
            {
                LogException(e, "Error in GridData6");
                throw;
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
