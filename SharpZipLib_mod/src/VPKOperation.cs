using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib
{
    
    /// <summary>
    /// Utitilities for simplifying VPK related operations.
    /// </summary>
    public static class VPKOperation
    {

        /// <summary>
        /// Check if the entry is VPK entry.
        /// </summary>
        /// <param name="ze"></param>
        /// <returns></returns>
        public static bool IsVPK(ZipEntry ze)
        {
            return (new ZipExtraData(ze.ExtraData)).Find(VPKTagData.ID);
        }

        /// <summary>
        /// Unpack the information from the ZipEntry.
        /// </summary>
        /// <param name="ze"></param>
        public static void UnpackInfo(ZipEntry ze)
        {
            ZipExtraData ed = new ZipExtraData(ze.ExtraData);
            VPKTagData vtag;
            if (ed.Find(VPKTagData.ID))
            {
                vtag = (VPKTagData)ed.GetData(VPKTagData.ID);
                ze.Size = vtag.Size;
                ze.CompressedSize = vtag.CompressedSize;
                ze.Crc = vtag.CRC;
            }
        }

        /// <summary>
        /// Pack the information.
        /// </summary>
        /// <param name="ze"></param>
        public static void PackInfo(ZipEntry ze)
        {
            ZipExtraData ed = new ZipExtraData(ze.ExtraData);
            VPKTagData vtag = new VPKTagData();
            vtag.Size = ze.Size;
            vtag.CompressedSize = ze.CompressedSize;
            vtag.CRC = ze.Crc;
            ed.AddEntry(vtag);
            ze.ExtraData = ed.GetEntryData();
        }

    }

}