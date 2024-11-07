using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IntelOrca.Biohazard.BioRand;
using QRCoder;

namespace biorand.app.Views.Seed
{
    /// <summary>
    /// Interaction logic for QR.xaml
    /// </summary>
    public partial class QR : UserControl
    {
        public QR()
        {
            InitializeComponent();
            UpdateImage();
        }

        //seed (RandoConfig)
        public static readonly DependencyProperty SeedProperty = DependencyProperty.Register("Seed", typeof(RandoConfig), typeof(QR), new PropertyMetadata(Seed_Changed));
        public RandoConfig Seed { get => (RandoConfig)GetValue(SeedProperty); set => SetValue(SeedProperty, value); }

        private void UpdateImage()
        {
            var config = Seed;
            if (config == null)
            {
                config = new RandoConfig();
            }

            var seed = config.ToString();
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(seed, QRCodeGenerator.ECCLevel.M);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(3);
            qrImage.Source = ConvertBitmap(qrCodeImage);
            qrImage.Stretch = Stretch.None;
            RenderOptions.SetBitmapScalingMode(qrImage, BitmapScalingMode.NearestNeighbor);
        }

        private BitmapSource ConvertBitmap(System.Drawing.Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }

        private static void Seed_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as QR).UpdateImage();
        }
    }
}
