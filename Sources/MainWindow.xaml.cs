using System;
using System.Configuration;
using System.Windows;
using System.Windows.Interop;

namespace VisualSnowWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Make entire window and everything in it "transparent" to the Mouse
            var windowHwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(windowHwnd);

            int opacity = 40;
            
            int tmp;
            string opacity_s = ConfigurationManager.AppSettings["opacity"];
            bool ok = int.TryParse(opacity_s, out tmp);

            if (!ok)
            {
                Console.WriteLine("Error parsing opacity: invalid number [{0}], using default opacity [{1}]", opacity_s, opacity);
            } else if(tmp < 0)
            {
                Console.WriteLine("Error parsing opacity: negative value [{0}], using default opacity [{1}]", opacity_s, opacity);
            } else if(tmp > 100)
            {
                Console.WriteLine("Error parsing opacity: bigger than 100 [{0}], using default opacity [{1}]", opacity_s, opacity);
            }
            else
            {
                opacity = tmp;
            }

            VideoElement.Opacity = (float)opacity / 100.0f;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Loop
            VideoElement.Position = TimeSpan.Zero;
        }

    }
}
