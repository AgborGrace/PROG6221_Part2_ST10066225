using System;
using System.Collections.Generic;
using ChatBotCyberSecurity;

namespace CybersecurityAwarenessBot
{
    internal class Program
    {
        private static UserMemory userMemory = new UserMemory();
        private static SentimentAnalyzer sentimentAnalyzer = new SentimentAnalyzer();
        private static ConversationFlow conversationFlow = new ConversationFlow();

        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.Play();

            ImageDisplay imageDisplay = new ImageDisplay();
            imageDisplay.Show();

            Utils.TypingEffect("Welcome to the Cybersecurity Awareness Bot!");

            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine();
            userMemory.StoreName(userName);

            // Ask for favorite cybersecurity topic for personalization
            Console.Write("What cybersecurity topic interests you most? (password, phishing, privacy, etc.): ");
            string favoriteTopic = Console.ReadLine();
            userMemory.StoreFavoriteTopic(favoriteTopic);

            Console.Clear();
            imageDisplay.Show();
            Utils.TypingEffect($"Hello {userName}, I'm your friendly cybersecurity assistant.");

            if (!string.IsNullOrWhiteSpace(favoriteTopic))
            {
                Utils.TypingEffect($"I see you're interested in {favoriteTopic}. That's a great area to focus on!");
            }

            while (true)
            {
                MenuDisplay.ShowMenu(userName);

                Console.Write("\nYou: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                // Check for exit commands
                if (input.ToLower().Contains("goodbye") || input.ToLower().Contains("bye") || input.ToLower().Contains("exit"))
                {
                    Utils.TypingEffect("Bot: Goodbye and stay safe online!");
                    break;
                }

                // Detect sentiment and adjust response
                string sentiment = sentimentAnalyzer.AnalyzeSentiment(input);

                // Get response based on input and context
                string response = ChatBotData.GetResponse(input, userMemory, sentiment);

                // Handle conversation flow
                string flowResponse = conversationFlow.HandleFollowUp(input, response);
                if (!string.IsNullOrWhiteSpace(flowResponse))
                {
                    response = flowResponse;
                }

                Utils.TypingEffect("Bot: " + response);

                // Store this interaction for potential follow-ups
                conversationFlow.SetLastTopic(ChatBotData.GetLastMatchedKeyword());

                Console.WriteLine("\nWould you like to ask another question? (yes/no)");
                string again = Console.ReadLine().ToLower();
                if (again != "yes" && again != "y") break;

                Console.Clear();
            }

            Console.WriteLine("Thank you for using the Cybersecurity Awareness Bot!");
        }
    }
}