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
            IPlotDocument plot = _surferApp.Documents.Add(SrfDocTypes.srfDocPlot);

            // Set page orientation:
            plot.PageSetup.Orientation = SrfPaperOrientation.srfLandscape;

            plot.Activate();

            return plot;
        }


        //public IPlotDocument OpenTemplatePlotDocument(string templatePath)
        //{
        //    if (!File.Exists(templatePath))
        //        throw new FileNotFoundException("Cannot find template plot document", templatePath);

        //    _surferApp.Documents.Open()

        //}
    }
}
