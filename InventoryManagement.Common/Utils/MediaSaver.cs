
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using System.IO;

namespace InventoryManagement.Common.Utils
{
    public class MediaSaver : IMediaSaver
    {
        public MediaSaver()
        {
        }
        public bool SaveImage(byte[] image, string relativedestPath)
        {
            try
            {
                var destPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", relativedestPath);
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                File.WriteAllBytes(destPath, image);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
           ;


        }

    }
}

