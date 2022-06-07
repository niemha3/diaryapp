using System;
using System.Collections.Generic;
namespace DiaryApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Tämä on master branch


            List<Topic> topics = new List<Topic>();


            Console.WriteLine("Input topic id: ");
            int id = Topic.ReadInputInt();


            Console.Write("Input title: ");
            string title = Topic.ReadInputString();


            Console.Write("Input description: ");
            string description = Topic.ReadInputString();


            Console.Write("Input estimated time in hours to master the topic: ");
            double estimatedTimeToMaster = Topic.ReadDoubleInput();

            Console.Write("Input time that you actually spent for the topic: ");
            double timeSpent = Topic.ReadDoubleInput();

            Console.Write("What was your source website or book name: ");
            string source = Topic.ReadInputString();

            Console.Write("Input your starting date (e.g 11/06/1993): ");
            DateTime startLearningDate = Topic.ReadDateTime();

            Console.Write("Is it still in progress?");
            bool inProgress = true;
            string result = Topic.ReadInputString();
            if (result == "no")
            {
                inProgress = false;

            }

            else
            {
                inProgress = true;
            }

            Console.Write("Please input topics completition date (e.g 11/06/1993): ");
            DateTime completitionDate = Topic.ReadDateTime();

            Topic topic = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, source, startLearningDate, inProgress, completitionDate);



            topics.Add(topic);


            Console.WriteLine(topic.ToString());
        }
    }

    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletitionDate { get; set; }

        public Topic(int id, string title, string description, double estimatedTimeToMaster, double timeSpent,
           string source, DateTime startLearningDate, bool inProgress, DateTime completitionDate)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletitionDate = completitionDate;



        }
        public static string ReadInputString()
        {
            string input = Console.ReadLine();
            return input;
        }

        public static int ReadInputInt()
        {
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }

        public static double ReadDoubleInput()
        {
            double input = Convert.ToDouble(Console.ReadLine());
            return input;
        }

        public static DateTime ReadDateTime()
        {
            string inputDate = Console.ReadLine();
            DateTime completitionDate = DateTime.Parse(inputDate);
            return completitionDate;
        }

        public override string ToString()
        {
            return "Topic Id:" + Id.ToString() +
                "\nTopic title: " + Title.ToString() +
                "\n topic description: " + Description.ToString() +
                "\n Estimated time to master: " + EstimatedTimeToMaster.ToString() + " hours" +
                "\n Time spent: " + TimeSpent.ToString() + " hours" +
                "\n Source: " + Source.ToString() +
                "\n Start learning date:  " + StartLearningDate.ToString("dd/MM/yyyy") +
                "\n still in progress: " + InProgress.ToString() +
                "\n Completition date: " + CompletitionDate.ToString("dd/MM/yyyy");
        }
    }



}
