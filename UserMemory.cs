using System;
using System.Collections.Generic;

namespace CybersecurityAwarenessBot
{
    internal class UserMemory
    {
        private string userName;
        private string favoriteTopic;
        private List<string> conversationHistory;

        public UserMemory()
        {
            conversationHistory = new List<string>();
        }

        public void StoreName(string name)
        {
            userName = name;
        }

        public void StoreFavoriteTopic(string topic)
        {
            favoriteTopic = topic;
        }

        public void AddToHistory(string interaction)
        {
            conversationHistory.Add(interaction);
            // Keep only last 10 interactions to manage memory
            if (conversationHistory.Count > 10)
            {
                conversationHistory.RemoveAt(0);
            }
        }

        public string GetName()
        {
            return userName;
        }

        public string GetFavoriteTopic()
        {
            return favoriteTopic;
        }

        public List<string> GetHistory()
        {
            return new List<string>(conversationHistory);
        }
    }
}
