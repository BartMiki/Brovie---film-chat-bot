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
                return postMessage(toSend);
            }
            catch (Exception)
            {
                throw new Exception("");
            }
        }


        private ChatMessage postMessage(ChatMessage toSend)
        {
            var botResponse = PostAsync(
                Uri, new StringContent(toSend.GetJsonToSend(), Encoding.UTF8)
            ).Result;

            string json = botResponse.Content.ReadAsStringAsync().Result;
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
