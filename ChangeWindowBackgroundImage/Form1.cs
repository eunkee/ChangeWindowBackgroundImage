using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace ChangeWindowBackgroundImage
{
    public partial class Form1 : Form
    {
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Image img = Properties.Resources.windowbackground;
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.png");
            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}










