using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace SurferTools.Tests
{
    public class SurferTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly SurferService _surferService = new();
        private bool _closeSurferOnTestFinish = true;

        public SurferTests(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("In ctor SurferTests");
        }

        public void Dispose()
        {
            if (_closeSurferOnTestFinish)
            {
                // Clean up:
                _surferService?.QuitSurfer();
                _output.WriteLine("Surfer is closed in Dispose()");
            }
            else
            {
                _surferService.ShowHideSurfer(true);
            }

            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task ShowHideSurfer()
        {
            // Show Surfer
            var retVal = _surferService.ShowHideSurfer(true);
            retVal.ShouldBeTrue("Surfer is not visible");
            _surferService.IsVisible.ShouldBeTrue("Surfer is not visible");

            // Wait 5 seconds:
            await Task.Delay(5_000);

            // Hide Surfer
            retVal = _surferService.ShowHideSurfer(false);
            retVal.ShouldBeFalse("Surfer is still visible");
            _surferService.IsVisible.ShouldBeFalse("Surfer is still visible");

            _output.WriteLine("ShowHideSurfer was successfull");
        }

        [Fact]
        public void AddPlotDocument()
        {
            _closeSurferOnTestFinish = false;
            var currentNumDocuments = _surferService.GetNumDocuments();
            var retVal = _surferService.AddPlotDocument();
            retVal.ShouldNotBeNull("New plot document is null");
            _output.WriteLine(retVal.FullName);

            var newNumDocuments = _surferService.GetNumDocuments();
            newNumDocuments.ShouldBe(currentNumDocuments + 1, "Number of documents is unexpected");
            
            _output.WriteLine("AddPlotDocument was successfull");
        }

    }
}
