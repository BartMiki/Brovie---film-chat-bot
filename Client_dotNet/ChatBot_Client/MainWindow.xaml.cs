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
        private static readonly ChatBotClient Client = 
            new ChatBotClient("brovie-film-chatbot.herokuapp.com","do");

        public MainWindow()
        {
            DataContext = _messages;
            InitializeComponent();
        }

        ~MainWindow()
        {
            Client.Dispose();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Text != string.Empty)
            {
                AddToMessages(ChatMessageFactory.ChatMessageToSend(MessageBox.Text));
            }
        }

        private void Menu_ConnectionTester_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ConnectionTesterWindow();
            window.Show();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && MessageBox.Text != string.Empty)
            {
                AddToMessages(ChatMessageFactory.ChatMessageToSend(MessageBox.Text));
            }
        }


        private void AddToMessages(ChatMessage chatMessage)
        {
            _messages.Add(chatMessage);
            MessageBox.Text = string.Empty;

            var response = Client.PostMessage(chatMessage);
            _messages.Add(response);

            int lastMessageIndex = MessagesListBox.Items.Count - 1;
            MessagesListBox.ScrollIntoView(MessagesListBox.Items[lastMessageIndex]);
        }
    }
}
