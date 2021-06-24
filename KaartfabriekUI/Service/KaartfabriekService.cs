using System;
using System.IO;
using System.Windows.Forms;
using Surfer;
using SurferTools;

namespace KaartfabriekUI.Service
{
    /// <summary>
    /// Service for the GUI
    /// </summary>
    public class KaartfabriekService
    {

        /// <summary>
        /// Action to perform after clicking the button
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="veldDataLocation"></param>
        /// <param name="monsterDataLocation"></param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colK40"></param>
        /// <returns></returns>
        public bool OpenDataForBlanking(string workingFolder, string veldDataLocation, string monsterDataLocation,
            int colX, int colY, int colK40)
        {
            // TODO: Make EPSG-code flexible:
            var surferService = new SurferService(SurferConstants.Epsg28992, workingFolder);

            // Add Velddata:
            var mapFrameVelddata = surferService.AddPostMap(veldDataLocation, "Velddata", colX, colY);
            surferService.SetColoringVelddataPostmap(mapFrameVelddata.Overlays.Item(1) as IPostLayer2, colK40);

            // Add Monsterdata:
            var mapFrameMonsterdata = surferService.AddPostMap(monsterDataLocation, "Monsterdata");
            surferService.SetLabelMonsterdataPostmap(mapFrameMonsterdata.Overlays.Item(1) as IPostLayer2, 3);

            var velddataLimits = new Limits(mapFrameVelddata.xMin,
                                            mapFrameVelddata.xMax,
                                            mapFrameVelddata.yMin,
                                            mapFrameVelddata.yMax);

            // Get AAN-data:
            var aanShapefileLocation = ProcessTools.GetAanData(Limits.RoundUp(velddataLimits, 1), workingFolder);
            // Add AAN-data:
            var mapFrameAan = surferService.AddShapefile(aanShapefileLocation);

            // Increase limits:
            var luchtfotoLimits = Limits.RoundUp(new Limits(mapFrameAan.xMin, mapFrameAan.xMax, mapFrameAan.yMin, mapFrameAan.yMax), 50);
            // Get luchtfoto:
            var luchtfotoLocation = ProcessTools.GetLuchtfotoImage(luchtfotoLimits, workingFolder);
            // Add luchtfoto:
            var mapFrameLuchtfoto = surferService.AddGeoreferencedImage(luchtfotoLocation);

            // Merge map frames:
            var mergedMapFrame = surferService.MergeMapFrames(mapFrameVelddata, mapFrameMonsterdata,
                mapFrameAan, mapFrameLuchtfoto);

            surferService.MakeMapFrameFit(mergedMapFrame);

            // Save result:
            surferService.SavePlotDocument(Path.Combine(workingFolder, "DataForBlanking.srf"));

            // Toon Surfer:
            return surferService.ShowHideSurfer(true);
        }

        /// <summary>
        /// Create the nuclide grids and add them to the active map
        /// </summary>
        /// <param name="workingFolder"></param>
        /// <param name="veldDataLocation"></param>
        /// <param name="blankFileLocation"></param>
        /// <param name="colX"></param>
        /// <param name="colY"></param>
        /// <param name="colAlt"></param>
        /// <param name="colK40"></param>
        /// <param name="colU238"></param>
        /// <param name="colTh232"></param>
        /// <param name="colCs137"></param>
        /// <param name="colTc"></param>
        public void CreateNuclideGrids(string workingFolder, string veldDataLocation, string blankFileLocation,
            int colX, int colY, int colAlt, int colK40, int colU238, int colTh232, int colCs137, int colTc)
        {
            // TODO: Check inputs

            // TODO: Make EPSG-code flexible:
            var surferService = new SurferService(SurferConstants.Epsg28992, workingFolder);

            var nuclideGridsFolder = Path.Combine(workingFolder, "nuclide grids");
            if (!Directory.Exists(nuclideGridsFolder)) Directory.CreateDirectory(nuclideGridsFolder);

            var bufferedBlankFileLocation = string.Empty;
            
            try
            {
                // Create new plot document:
                surferService.CreateNewActivePlotDocument();

                CreateNuclideGrid("Alt.grd", "Altitude", colAlt);
                CreateNuclideGrid("K40.grd", "K-40", colK40);
                CreateNuclideGrid("U238.grd", "U-238", colU238);
                CreateNuclideGrid("Th232.grd", "Th-232", colTh232);
                CreateNuclideGrid("Cs137.grd", "Cs-137", colCs137);
                CreateNuclideGrid("TC.grd", "TC", colTc);


                // Show maps nicely on plot (resize, align horizontally
                surferService.AlignNuclideGrids();
                surferService.SavePlotDocument(Path.Combine(nuclideGridsFolder, "nuclideGrids.srf"));
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Er ging iets mis bij het maken van de nuclide grids. Error: " + e.Message);
            }
            finally
            {
                surferService.ShowHideSurfer(true);
            }

            void CreateNuclideGrid(string fileName, string layerName, int colZ)
            {
                // Add blank file to get the limits:
                var mapBlankFile = surferService.AddShapefile(blankFileLocation);
                if (!File.Exists(bufferedBlankFileLocation))
                {
                    // Buffer blank file:
                    bufferedBlankFileLocation = surferService.BufferPolygon(mapBlankFile, 10);
                    if (!File.Exists(bufferedBlankFileLocation))
                    {
                        MessageBox.Show(@"Het maken van een buffer om de blank file is niet goed gegaan.");
                        return;
                    }
                }

                var mapBufferedBlankFile = surferService.AddShapefile(bufferedBlankFileLocation);

                // Temp file:
                var tmpGridLocation = Path.Combine(workingFolder, "tmpGrid.grd");
                // Final file:
                var outGridLocation = Path.Combine(nuclideGridsFolder, fileName);

                // Grid velddata:
                surferService.InverseDistanceGridding(veldDataLocation, tmpGridLocation, colX, colY, colZ,
                    Limits.RoundUp(
                        new Limits(mapBufferedBlankFile.xMin, mapBufferedBlankFile.xMax, mapBufferedBlankFile.yMin, mapBufferedBlankFile.yMax), 50));
                // Blank using buffered file:
                if (!surferService.GridAssignNoData(tmpGridLocation, bufferedBlankFileLocation, outGridLocation))
                    return;

                // Clean-up tmp file:
                File.Delete(tmpGridLocation);
                // Delete buffered blank file map frame:
                mapBufferedBlankFile.Delete();
                // Add contour layer:
                var mapContour = surferService.AddContour(outGridLocation, layerName, true);
                var mergedMapFrame = surferService.MergeMapFrames(mapBlankFile, mapContour);

                // Group:
                surferService.DeselectAll();
                // Get the contour layer:
                var numOverlays = mergedMapFrame.Overlays.Count;
                for (var i = 1; i <= numOverlays; i++)
                {
                    if (mergedMapFrame.Overlays.Item(i) is not IContourLayer contourLayer) continue;

                    // Select the color scale:
                    contourLayer.ColorScale.Selected = true;
                    // Move:
                    contourLayer.SetZOrder(SrfZOrder.srfZOToBack);
                    break;
                }

                mergedMapFrame.Selected = true;
                surferService.GroupSelection(layerName);
            }
        }
    }
}
