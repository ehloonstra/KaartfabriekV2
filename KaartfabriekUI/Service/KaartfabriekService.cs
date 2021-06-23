﻿using System;
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

            // Change axis:
            mergedMapFrame.Axes.Item("Right Axis").MajorTickType = SrfTickType.srfTickNone;
            mergedMapFrame.Axes.Item("Top Axis").MajorTickType = SrfTickType.srfTickNone;

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
        public void CreateNuclideGrids(string workingFolder, string veldDataLocation, string blankFileLocation,
            int colX, int colY, int colAlt, int colK40, int colU238, int colTh232, int colCs137)
        {
            // TODO: Check inputs

            // TODO: Make EPSG-code flexible:
            var surferService = new SurferService(SurferConstants.Epsg28992, workingFolder);

            try
            {
                // Create new plot document:
                surferService.CreateNewActivePlotDocument();

                var mapAlt = CreateNuclideGrid("Alt.grd", "Altitude", colAlt);
                var mapK40 = CreateNuclideGrid("K40.grd", "K-40", colK40);
                var mapU238 = CreateNuclideGrid("U238.grd", "U-238", colU238);
                var mapTh232 = CreateNuclideGrid("Th232.grd", "Th-232", colTh232);
                var mapCs137 = CreateNuclideGrid("Cs137.grd", "Cs-137", colCs137);

                // TODO: Add label with nuclide name

                // TODO: Show maps nicely on plot (resize, align horizontally
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Er ging iets mis bij het maken van de nuclide grids. Error: " + e.Message);
            }
            finally
            {
                surferService.ShowHideSurfer(true);
            }

            IMapFrame CreateNuclideGrid(string fileName, string layerName, int colZ)
            {
                // Add blank file to get the limits:
                var mapBlankFile = surferService.AddShapefile(blankFileLocation);
                // Buffer blank file:
                // TODO: Werkt niet, vraag gesteld aan GS: var bufferedBlankFileLocation = surferService.BufferPolygon(mapBlankFile, 10);

                // Temp file:
                var tmpGridLocation = Path.Combine(workingFolder, "tmpGrid.grd");
                // Final file:
                var nuclideGridsFolder = Path.Combine(workingFolder, "nuclide grids");
                if (!Directory.Exists(nuclideGridsFolder)) Directory.CreateDirectory(nuclideGridsFolder);
                var outGridLocation = Path.Combine(nuclideGridsFolder, fileName);

                // Grid velddata:
                surferService.InverseDistanceGridding(veldDataLocation, tmpGridLocation, colX, colY, colZ,
                    Limits.RoundUp(
                        new Limits(mapBlankFile.xMin, mapBlankFile.xMax, mapBlankFile.yMin, mapBlankFile.yMax), 50));
                // Blank file:
                // TODO: Blank using buffered file:
                if (!surferService.GridAssignNoData(tmpGridLocation, blankFileLocation, outGridLocation))
                    return null;

                // Clean-up tmp file:
                File.Delete(tmpGridLocation);
                // Add contour layer:
                var mapContour = surferService.AddContour(outGridLocation, layerName);
                return surferService.MergeMapFrames(mapContour, mapBlankFile);
            }
        }


    }
}