using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ChatControllerModels
{
    public class AllPossibleMessages
    {
        public AllPossibleMessages(UserMessageResponse userMessage, GroupMessageResponse groupMessage)
        {
            UserMessage = userMessage;
            GroupMessage = groupMessage;
        }

        public UserMessageResponse UserMessage { get; }
        public GroupMessageResponse GroupMessage { get; }
    }
}
