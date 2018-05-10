using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ChatBot_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ObservableCollection<ChatMessage> _messages = new ObservableCollection<ChatMessage>();
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "192.168.43.117:5000/do";

        public MainWindow()
        {
            DataContext = _messages;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Text != string.Empty)
            {
                AddToMessages(new ChatMessage() { MessageText = MessageBox.Text, Sended = "True" });
            }
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && MessageBox.Text != string.Empty)
            {
                AddToMessages(new ChatMessage() { MessageText = MessageBox.Text, Sended = "True" });
            }
        }

        private void AddToMessages(ChatMessage chatMessage)
        {
            _messages.Add(chatMessage);
            MessageBox.Text = string.Empty;

            var response = PostMessage(new JsonMessage() {respond = chatMessage.MessageText});
            _messages.Add(response);

            int lastMessageIndex = MessagesListBox.Items.Count - 1;
            MessagesListBox.ScrollIntoView(MessagesListBox.Items[lastMessageIndex]);
        }

        private ChatMessage PostMessage(JsonMessage jsonMessage)
        {
            string message = JsonConvert.SerializeObject(jsonMessage);

            //_messages.Add(new Message(){MessageText = message, Sended = "True"});

            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = "192.168.43.117";
            uriBuilder.Port = 5000;
            uriBuilder.Path = "do";
            uriBuilder.Scheme = "http";

            //var response = client.PostAsync(
            //    uriBuilder.Uri,
            //    new StringContent(message, Encoding.UTF8)
            //    ).Result;

            //var json = @response.Content.ReadAsStringAsync().Result;
            //json = json.Replace("\"", "'");
            var json =
                @"{'respond': 'Los olvidados is a great film!', 'path': 'https://image.tmdb.org/t/p/w500//ufyPovbgRKlKTWrlzDFgULfgfyi.jpg'}";
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(json);

            ChatMessage result = new ChatMessage();
            result.Sended = "Received";
            result.MessageText = jsonResponse.respond;
            result.ImageUri = new Uri(jsonResponse.path, UriKind.Absolute);
            result.DownloadImageData();

            return result;
        }
    }
}
