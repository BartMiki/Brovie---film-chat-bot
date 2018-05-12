using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatBot_Client
{
    public class ConnectionTester
    {
        private readonly int _connectionNumber;
        private readonly int _timeRange;
        private readonly ObservableConcurrentCollection<string> _output;
        private static readonly string[] messageSubjects = new[] {"western", "comedy", "horror", "thriller", "action"};
        private static readonly Random random = new Random();

        public ConnectionTester(int connectionNumber, int timeRange, ObservableConcurrentCollection<string> output)
        {
            _connectionNumber = connectionNumber;
            _timeRange = timeRange;
            _output = output;
        }

        public void Test()
        {
            ThreadLocal<string> sender = new ThreadLocal<string>(
                () => $"{messageSubjects[random.Next(messageSubjects.Length)]} film");

            Action action = () =>
            {

                Thread.Sleep(random.Next(_timeRange));

                using (ChatBotClient client = new ChatBotClient("brovie-film-chatbot.herokuapp.com", "do"))
                {
                    ChatMessage message = ChatMessageFactory.ChatMessageToSend(sender.Value);
                    ChatMessage received = client.PostMessage(message);
                    _output.AddFromEnumerable(new[] {$"{sender.Value} -> {received.MessageText}"});
                }
            };

            Action[] threads = new Action[_connectionNumber];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = action;

            Parallel.Invoke(threads);
        }
    }
}
