using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Client
{
    public static class ChatMessageFactory
    {
        public static ChatMessage ChatMessageToSend(string message)
        {
            var messageBuilder = new ChatMessage.ChatMessageBulder();
            return messageBuilder
                .AddMessage(message)
                .AddMessageStatus(MessageStatus.Sended)
                .ToChatMessage();
        }

        public static ChatMessage ChatMessageFromReceived(JsonBotResponse response)
        {

            var messageBuilder = new ChatMessage.ChatMessageBulder()
                .AddMessage(response.respond)
                .AddMessageStatus(MessageStatus.Received);

            if (response.path != null)
                messageBuilder.AddImageUri(new Uri(response.path));

            return messageBuilder.ToChatMessage();
        }
    }
}
