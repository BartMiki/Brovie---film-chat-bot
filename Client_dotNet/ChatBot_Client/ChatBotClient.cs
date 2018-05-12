using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatBot_Client
{
    public class ChatBotClient : HttpClient
    {
        public Uri Uri { get; }

        public ChatBotClient(string host, string path)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = host;
            uriBuilder.Port = 80;
            uriBuilder.Path = path;
            uriBuilder.Scheme = "http";

            Uri = uriBuilder.Uri;
        }

        public ChatMessage PostMessage(ChatMessage toSend)
        {
            try
            {
                return InnerPostMessage(toSend);
            }
            catch (Exception)
            {
                return ErrorMessage("Ups something went wrong.");
            }
        }

        private ChatMessage ErrorMessage(string message)
        {
            return new ChatMessage.ChatMessageBulder()
                .AddMessage(message)
                .AddMessageStatus(MessageStatus.Error)
                .ToChatMessage();
        }

        private ChatMessage InnerPostMessage(ChatMessage toSend)
        {
            var botResponse = PostAsync(
                Uri, new StringContent(toSend.GetJsonToSend(), Encoding.UTF8)
            ).Result;

            byte[] bytes = botResponse.Content.ReadAsByteArrayAsync().Result;
            string json = Encoding.UTF8.GetString(bytes);
            json = json
                .Replace("\"{", "{")
                .Replace("}\"", "}")
                .Replace("\\\"", "'");

            var response = JsonConvert.DeserializeObject<JsonBotResponse>(json);

            ChatMessage result = ChatMessageFactory.ChatMessageFromReceived(response);
            result.DownloadImage();

            return result;
        }
    }
}
