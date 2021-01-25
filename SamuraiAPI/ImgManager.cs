using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI
{
    public class ImgManager
    {
        public void OpenImg(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image i = Image.FromStream(ms);
            i.Save(@"1.jpeg",ImageFormat.Jpeg);

            var filePath = @"1.jpeg";
            ProcessStartInfo Info = new ProcessStartInfo()
            {
                FileName = "mspaint.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = filePath
            };
            Process.Start(Info);

            
        }
    }
}
