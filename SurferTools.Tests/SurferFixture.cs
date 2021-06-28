using System;
using System.IO;

namespace SurferTools.Tests
{
    public class SurferFixture: IDisposable
    {
        internal SurferService SurferService = new(SurferConstants.GetProjectionName("EPSG:28992"), Path.GetTempPath());
        internal bool CloseSurferOnTestFinish = true;

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
