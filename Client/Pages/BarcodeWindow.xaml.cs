using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Xml.Linq;
using Microsoft.Win32;
using System.Drawing.Imaging;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для BarcodeWindow.xaml
    /// </summary>
    public partial class BarcodeWindow : Window
    {
        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap bitmap;
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public BarcodeWindow()
        {
            InitializeComponent();
        }

        private void QRGenerate_Click(object sender, RoutedEventArgs e)
        {
            encoder.QRCodeScale = 8;
            bitmap = encoder.Encode(QrTxt.Text);
            ImageQR.Source = ConvertImage.ToBitmapImage(bitmap);

        }

        private void QRSave_Click(object sender, RoutedEventArgs e)
        {
            saveFileDialog.Filter = "PNG|*.png";
            saveFileDialog.FileName = QrTxt.Text;
            if(saveFileDialog.ShowDialog() == true)
            {
                if(bitmap != null)
                {
                    bitmap.Save(string.Concat(saveFileDialog.FileName, ".png"), ImageFormat.Png);
                }
            }
        }
    }
}
