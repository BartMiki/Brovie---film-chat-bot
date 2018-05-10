using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChatBot_Client
{
    public class Message
    {
        public string MessageText { get; set; }
        public string Sended { get; set; }
        public Uri ImageUri { get; set; }
        public ImageSource ImageData { get; set; }

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
