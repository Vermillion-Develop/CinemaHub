using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CinemaHub.Helpers
{
    public static class ImgDehash
    {
        public static Bitmap? LoadFromBytes(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return new Bitmap(AssetLoader.Open(new Uri("avares://CinemaHub/Assets/Images/dfImg.jfif")));

            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    return new Bitmap(ms);
                }
            }
            catch
            {
               return new Bitmap(AssetLoader.Open(new Uri("avares://CinemaHub/Assets/Images/dfImg.jfif")));
            }
        }
    }
}
