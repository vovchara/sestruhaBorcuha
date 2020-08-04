using System;
using Monosyne.Graphics;
using Monosyne.Platform;
using Monosyne.Platform.Windows;
using Monosyne.Platform.Windows.DirectX;

namespace Scene.Windows
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WindowsPlatformOptions initOptions = new WindowsPlatformOptions
            {
                DisableFullScreen = true,
                DisplayMode = new DisplayMode(1280, 720, ScreenOrientation.All),
                AssetsDirectory = "Assets",
                UseCapabilities = GraphicsDeviceCapabilities.MapBufferRange | GraphicsDeviceCapabilities.Instancing,
                ShaderSourceType = ShaderSourceType.Compilation
            };

            Run(new WindowsApplication(initOptions));
        }

        private static void Run(AbstractWindowsApplication app)
        {
            app.Run(p => new TestGame(p));
            app.Dispose();
        }
    }
}
