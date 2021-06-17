using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace SurferTools.Tests
{
    public class SurferTests : IClassFixture<SurferFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly SurferFixture _fixture;

        public SurferTests(ITestOutputHelper output, SurferFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            _output.WriteLine("In ctor SurferTests");
        }

        [Fact]
        public async Task ShowHideSurfer()
        {
            // Show Surfer
            var retVal = _fixture.SurferService.ShowHideSurfer(true);
            retVal.ShouldBeTrue("Surfer is not visible");
            _fixture.SurferService.IsVisible.ShouldBeTrue("Surfer is not visible");

            // Wait 5 seconds:
            await Task.Delay(5_000);

            // Hide Surfer
            retVal = _fixture.SurferService.ShowHideSurfer(false);
            retVal.ShouldBeFalse("Surfer is still visible");
            _fixture.SurferService.IsVisible.ShouldBeFalse("Surfer is still visible");

            _output.WriteLine("ShowHideSurfer was successfull");
        }

        [Fact]
        public void AddPlotDocument()
        {
            _fixture.CloseSurferOnTestFinish = false;
            var currentNumDocuments = _fixture.SurferService.GetNumDocuments();
            var retVal = _fixture.SurferService.AddPlotDocument();
            retVal.ShouldNotBeNull("New plot document is null");
            _output.WriteLine(retVal.FullName);

            var newNumDocuments = _fixture.SurferService.GetNumDocuments();
            newNumDocuments.ShouldBe(currentNumDocuments + 1, "Number of documents is unexpected");
            
            _output.WriteLine("AddPlotDocument was successfull");
        }

    }
}
