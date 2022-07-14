using System;
namespace DiaryApp
{
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

        public Task task;
       

        public Topic(string title, string description, double estimatedTimeToMaster, double timeSpent,
           string source, DateTime startLearningDate, bool inProgress, DateTime completitionDate)
        {

            
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletitionDate = completitionDate;



        }

        public Topic()
        {

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
                "\n Estimated time to master: " + EstimatedTimeToMaster.ToString() + " days" +
                "\n Time spent: " + TimeSpent.ToString() + " days" +
                "\n Source: " + Source.ToString() +
                "\n Start learning date:  " + StartLearningDate.ToString("dd/MM/yyyy") +
                "\n still in progress: " + InProgress.ToString() +
                "\n Completition date: " + CompletitionDate.ToString("dd/MM/yyyy");
        }
    }


}










         



















