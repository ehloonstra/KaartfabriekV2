using System;

namespace SurferTools.Tests
{
    public class SurferFixture: IDisposable
    {
        public SurferService SurferService = new();
        public bool CloseSurferOnTestFinish = true;

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
