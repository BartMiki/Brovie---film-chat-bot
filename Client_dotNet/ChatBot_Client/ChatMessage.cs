using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace ChatBot_Client
{
    public enum MessageStatus { Sended, Received}

    public class ChatMessage
    {
        public string MessageText { get; private set; }
        public MessageStatus Status { get; private set; }
        public bool HasImage { get; private set; }
        public Uri ImageUri { get; private set; }
        public ImageSource Image { get; private set; }

        public ICommand OpenImageInFullScaleCommand 
            => new CommandHandler(OpenImageInFullScale, HasImage);

        public void OpenImageInFullScale()
        {
            var window = new ImageViewWindow();
            window.Image.Source = Image;
            window.Show();
        }

        public void DownloadImage()
        {
            if (ImageUri != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = ImageUri;
                bitmap.EndInit();

                Image = bitmap;
            }
        }

        private class JsonToSend
        {
            public string respond { get; set; }
        }

        public string GetJsonToSend()
        {
            return JsonConvert.SerializeObject(new JsonToSend() { respond = MessageText });
        }

        private ChatMessage(){ HasImage = false; }

        public class ChatMessageBulder
        {
            private readonly ChatMessage _chatMessage;

            public ChatMessageBulder()
            {
                _chatMessage = new ChatMessage();
            }

            public ChatMessageBulder AddMessage(string message)
            {
                _chatMessage.MessageText = message;
                return this;
            }

            public ChatMessageBulder AddMessageStatus(MessageStatus status)
            {
                _chatMessage.Status = status;
                return this;
            }

            public ChatMessageBulder AddImageUri(Uri uri)
            {
                _chatMessage.ImageUri = uri;
                _chatMessage.HasImage = true;
                return this;
            }

            public ChatMessage ToChatMessage()
            {
                return _chatMessage;
            }
        }         
    }
}
