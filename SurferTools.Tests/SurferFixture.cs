using System;
using Surfer;

namespace SurferTools.Tests
{
    public class SurferFixture: IDisposable
    {
        internal SurferService SurferService = new();
        internal bool CloseSurferOnTestFinish = true;

        internal IPlotDocument PlotDocument = null;

        public SurferFixture()
        {
            
        }

        public void Dispose()
        {
            // ... clean up
            if (CloseSurferOnTestFinish)
            {
                // Clean up:
                SurferService?.QuitSurfer();
                SurferService = null;
            }
            else
            {
                SurferService.ShowHideSurfer(true);
            }            

            GC.SuppressFinalize(this);
        }
    }
}
