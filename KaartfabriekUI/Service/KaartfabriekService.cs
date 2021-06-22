using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surfer;
using SurferTools;

namespace KaartfabriekUI.Service
{
    public class KaartfabriekService
    {
        public KaartfabriekService()
        {
            
        }


        public bool OpenDataForBlanking(string workingFolder, string veldDataLocation, string monsterDataLocation)
        {
            // TODO: Make EPSG-code flexible:
            var surferService = new SurferService(SurferConstants.Epsg28992);

            // Add Velddata:
            var mapFrameVelddata = surferService.AddPostMap(veldDataLocation, "Velddata");
            surferService.SetColoringVelddataPostmap(mapFrameVelddata.Overlays.Item(1) as IPostLayer2, 16);

            // Add Monsterdata:
            var mapFrameMonsterdata = surferService.AddPostMap( monsterDataLocation, "Monsterdata");
            surferService.SetLabelMonsterdataPostmap(mapFrameMonsterdata.Overlays.Item(1) as IPostLayer2, 3);

            var velddataLimits = new Limits(mapFrameVelddata.xMin,
                                            mapFrameVelddata.xMax,
                                            mapFrameVelddata.yMin,
                                            mapFrameVelddata.yMax);

            // Get AAN-data:
            var aanShapefileLocation = ProcessTools.GetAanData(Limits.RoundUp(velddataLimits,1) , workingFolder);
            // Add AAN-data:
            var mapFrameAan = surferService.AddShapefile( aanShapefileLocation);

            // Increase limits:
            var luchtfotoLimits = Limits.RoundUp(new Limits(mapFrameAan.xMin, mapFrameAan.xMax, mapFrameAan.yMin, mapFrameAan.yMax), 50);
            // Get luchtfoto:
            var luchtfotoLocation = ProcessTools.GetLuchtfotoImage(luchtfotoLimits, workingFolder);
            // Add luchtfoto:
            var mapFrameLuchtfoto = surferService.AddGeoreferencedImage(luchtfotoLocation);
            
            // Merge map frames:
            var mergedMapFrame = surferService.MergeMapFrames( mapFrameVelddata, mapFrameMonsterdata,
                mapFrameAan, mapFrameLuchtfoto);

            // Change axis:
            mergedMapFrame.Axes.Item("Right Axis").MajorTickType = SrfTickType.srfTickNone;
            mergedMapFrame.Axes.Item("Top Axis").MajorTickType = SrfTickType.srfTickNone;

            surferService.MakeMapFrameFit(mergedMapFrame);

            // Save result:
            surferService.SavePlotDocument(Path.Combine(workingFolder, "DataForBlanking.srf"));

            // Toon Surfer:
            return surferService.ShowHideSurfer(true);
        }
    }
}
