using System;
using System.Runtime.InteropServices;

namespace SurferTools
{
    /// <summary>
    /// Methods to interact with COM
    /// </summary>
    public class ComTools
    {
        [DllImport("oleaut32.dll", PreserveSig = false)]
        private static extern void GetActiveObject(
            ref Guid rclsid,
            IntPtr pvReserved,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppunk
        );

        [DllImport("ole32.dll")]
        private static extern int CLSIDFromProgID(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszProgID,
            out Guid pclsid
        );

        /// <summary>
        /// Get the active COM obejct by prog ID
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static object GetActiveObject(string progId)
        {
            try
            {
                CLSIDFromProgID(progId, out var clsid);
                GetActiveObject(ref clsid, IntPtr.Zero, out var obj);

                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Swallow: throw;
            }

            return null;
        }
    }
}
