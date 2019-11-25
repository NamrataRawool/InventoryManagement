using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Utils
{
    public interface IMediaSaver
    {
        bool SaveImage(byte[] image, string destPath);
    }
}
