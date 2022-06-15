using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiaryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Topic> topicKokoelma = new Dictionary<int, Topic>();
       
            string json = File.ReadAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsontopics.json");

            List<Topic> topicLista = new List<Topic>();
            topicLista = JsonSerializer.Deserialize<List<Topic>>(json);

       

            // luuppaa jokaisen list itemin id ja itse topic ja lisää listaan
            foreach (var item in topicLista)
            {
                int id = item.Id;
                topicKokoelma.Add(id, item);
            }

            int taskId = 0;
            bool menu = true;

            while (menu)
            {
               int userInput = ShowMenu();
            
                switch (userInput)
                {
                    case 1:
                        AddNewTopic(topicKokoelma);
                        break;

                    case 2:
                        ShowTopics(topicKokoelma);
                       
                        break;

                    case 3:
                        searchTopics(topicKokoelma);

                        break;

                    case 4:
                        updateTopic(topicKokoelma);

                        break;

                    case 5:
                        deleteTopic(topicKokoelma);
                        break;

                    case 6:
                        //AddNewTask ei vielä konkreettisesti lisää taskia niinkuin pitäisi
                        // On vielä vaiheessa
                        taskId++;
                        AddNewTask(taskId);
                        break;

                    case 7:
                        menu = false;
                        break;
                    default:
                        break;
                }
            }

        }

        static int ShowMenu()
        {
            Console.WriteLine("1. Add new topic ");
            Console.WriteLine("2. Show all topics");
            Console.WriteLine("3. Search topics");
            Console.WriteLine("4. Edit topics");
            Console.WriteLine("5. Delete topic");
            Console.WriteLine("6. Add new tasks ");
            Console.WriteLine("7. to quit");
            Console.Write("Input number to choose what to do:");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine("----------------");
            return input;
        }

        static void AddNewTopic( Dictionary<int, Topic> topicKokoelma)
        {
            Console.WriteLine("Input id for the topic: ");
            int id = Topic.ReadInputInt();

            Console.Write("Input topic's title: ");
            string title = Topic.ReadInputString();

            Console.Write("Input description for topic: ");
            string description = Topic.ReadInputString();

            Console.Write("Estimated time in days to learn the topic: ");
            double estimatedTimeToMaster = Topic.ReadDoubleInput();

            Console.Write("What was your source website or book name: ");
            string source = Topic.ReadInputString();

            Console.Write("Input your starting date (e.g 11/06/1993): ");
            DateTime startLearningDate = Topic.ReadDateTime();

            Console.Write("Please input topics completition date (e.g 11/06/1993): ");
            DateTime completitionDate = Topic.ReadDateTime();
            bool inProgress;
            if (completitionDate < DateTime.Today)
            {
                inProgress = false;
            }
            else
            {
                inProgress = true;
            }

            double timeSpent;
            if(inProgress)
            {
                timeSpent = (startLearningDate - DateTime.Today).Days;
            }

            else
            {
                timeSpent = (completitionDate - startLearningDate).Days;
            }

            Topic topic = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, source, startLearningDate, inProgress, completitionDate);
            topicKokoelma.Add(id, topic);

            //Kirjoitus Json-tiedostoon
            WriteToJson(topicKokoelma);
        }

        static void WriteToJson(Dictionary<int, Topic> topicKokoelma)
        {
            
            string jsonFile = JsonSerializer.Serialize(topicKokoelma.Values);
            File.WriteAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsontopics.json", jsonFile);
        }

        static void ShowTopics(Dictionary<int, Topic> topicKokoelma)
        {
            foreach (var item in topicKokoelma.Values)
            {
                Console.WriteLine(item);
                Console.WriteLine("----------------");
            }

        }
        static void AddNewTask(int taskId)
        {
            List<string> taskNotes = new List<string>();
            Console.Clear();
            Console.WriteLine("input task title: ");
            string taskTitle = Topic.ReadInputString();

            Console.Write("Input task description: ");
            string taskDescription = Topic.ReadInputString();

            Console.WriteLine("input notes: ");
            string note = Topic.ReadInputString();
            taskNotes.Add(note);

            Console.WriteLine("When is the task due(e.g 01/01/2000): ");
            DateTime taskDeadline = Topic.ReadDateTime();

            Console.WriteLine("What is the priority of the task? (very urgent, urgent or not urgent)");
            string urgency = Console.ReadLine();


            bool taskDone;
            if (taskDeadline < DateTime.Today)
            {
                taskDone = true;
            }
            else
            {
                taskDone = false;
            }
            Task task = new Task(taskId, taskTitle, taskDescription, taskNotes, taskDeadline, taskDone);
            
        }

        static void searchTopics(Dictionary<int, Topic> topicKokoelma)
        {
            Console.Write("Please input id for the topic you're searching: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (topicKokoelma.ContainsKey(id))
            {
                Console.WriteLine("-----------------");
                Console.WriteLine(topicKokoelma[id].ToString());
            }
            else
            {
                Console.WriteLine("No matches");
            }
        }

        static void updateTopic(Dictionary <int, Topic> topicKokoelma)
        {
            ShowTopics(topicKokoelma);
            Console.WriteLine("---------------");
            Console.WriteLine("Please input id for the topic you would like to edit: ");
            int id = int.Parse(Console.ReadLine());
            while (topicKokoelma.ContainsKey(id) == true)
            {
                
                Console.WriteLine("1) Edit Id");
                Console.WriteLine("2) Edit title");
                Console.WriteLine("3) Edit Description");
                Console.WriteLine("4) Edit Estimated time to master");
                Console.WriteLine("5) Edit source");
                Console.WriteLine("6) Edit start learning date: ");
                Console.WriteLine("7) Edit completition date");
                Console.WriteLine("8) Back to main menu");
                Console.WriteLine("What do you want to edit?");
                int input = Convert.ToInt32(Console.ReadLine());

                Topic haettu = null;
                topicKokoelma.TryGetValue(id, out haettu);
                switch (input)
                {
                    case 1:
                        Console.Write("Enter new Id: ");
                        int newId = Topic.ReadInputInt();
                        haettu.Id = newId;
                        break;

                    case 2:
                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();
                        haettu.Title = newTitle;
                        break;

                    case 3:
                        Console.Write("Enter new description: ");
                        string newDescription = Console.ReadLine();
                        haettu.Description = newDescription;
                        break;

                    case 4:
                        Console.Write("New estimated time to master: ");
                        int newEstimatedTime = Topic.ReadInputInt();
                        haettu.EstimatedTimeToMaster = newEstimatedTime;
                        break;

                    case 5:
                        Console.Write("Enter new source: ");
                        string newSource = Console.ReadLine();
                        haettu.Source = newSource;
                        break;

                    case 6:
                        Console.WriteLine("Enter new start date for learning(e.g 01/01/2000):");
                        DateTime newStartDate = Topic.ReadDateTime();
                        haettu.StartLearningDate = newStartDate;
                        break;

                    case 7:
                        Console.WriteLine("Enter new finish date(01/01/2000): ");
                        DateTime newCompletitionDate = Topic.ReadDateTime();
                        haettu.CompletitionDate = newCompletitionDate;
                        break;
                    case 8:

                       
                        break;

                    default:
                        break;
                }

            }
            
        }

        static void deleteTopic(Dictionary<int, Topic> topicKokoelma)
        {
            foreach (var item in topicKokoelma)
            {
                Console.WriteLine(item.Value);
                Console.WriteLine("-------------------------------");
            }
            Console.Write("Please input id for the topic you would like to delete: ");
            int id = int.Parse(Console.ReadLine());
            if (topicKokoelma.ContainsKey(id))
            {
                Console.WriteLine(topicKokoelma[id].ToString());
                Console.WriteLine("Are you sure you want to delete this topic(yes/no): ");
                string input = Console.ReadLine();
                if (input.Equals("yes")) {
                    topicKokoelma.Remove(id);
                    WriteToJson(topicKokoelma);
                }
                else
                { Console.WriteLine("Delete cancelled");
                  Console.WriteLine("-------------------------------");
                }
            }

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


    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public DateTime Deadline { get; set; }
        public Enum Priority { get; set; }
        public bool Done { get; set; }

        public Task (int taskId, string taskTitle, string taskDescription, List<string> taskNotes, DateTime taskDeadline, bool taskDone)
        {
            TaskId = taskId;
            Title = taskTitle;
            Description = taskDescription;
            Notes = taskNotes;
            Deadline = taskDeadline;
            //Priority = taskPriority;
            Done = taskDone;
        }
    }


}










         



















