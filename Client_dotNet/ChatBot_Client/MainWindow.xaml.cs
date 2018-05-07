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
        readonly ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "";

        public MainWindow()
        {
            DataContext = _messages;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Text != string.Empty)
            {
                AddToMessages(new Message() { MessageText = MessageBox.Text, Sended = "False" });
            }
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && MessageBox.Text != string.Empty)
            {
                AddToMessages(new Message() { MessageText = MessageBox.Text, Sended = "True" });
            }
        }

        private void AddToMessages(Message message)
        {
            _messages.Add(message);
            MessageBox.Text = string.Empty;

            string response = PostMessage(new JsonMessage() {respond = message.MessageText});
            _messages.Add(new Message(){MessageText = response,Sended = "Received"});

            int lastMessageIndex = MessagesListBox.Items.Count - 1;
            MessagesListBox.ScrollIntoView(MessagesListBox.Items[lastMessageIndex]);
        }

        private string PostMessage(JsonMessage jsonMessage)
        {
            string message = JsonConvert.SerializeObject(jsonMessage);
            var response = client.PostAsync(
                apiUrl,
                new StringContent(message, Encoding.UTF8, "application/json")
                );

            return response
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;
        }
    }
}
