using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatBot_Client
{
    public class ChatMessage
    {
        public string MessageText { get; set; }
        public string Sended { get; set; }
        public Uri ImageUri { get; set; }
        public ImageSource ImageData { get; set; }

        private ICommand _openImageInFullScaleCommand;

        public ICommand OpenImageInFullScaleCommand
        {
            get => new CommandHandler(OpenImageInFullScale,ImageData, ImageData != null);
        }

        public void OpenImageInFullScale(ImageSource image)
        {
            var window = new ImageViewWindow();
            window.Image.Source = ImageData;
            window.Show();
        }

        public void DownloadImageData()
        {
            if (ImageUri != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = ImageUri;
                bitmap.EndInit();

                ImageData = bitmap;
            }
        }
    }
}
