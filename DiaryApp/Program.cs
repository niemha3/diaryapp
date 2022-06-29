﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using DiaryApp.Models;
using ClassLibraryForDates;
namespace DiaryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
               
           
            Dictionary<int, Topic> topicKokoelma = new Dictionary<int, Topic>();
            List<Topic> topicLista = new List<Topic>();
            

            Dictionary<int, Task> taskKokoelma = new Dictionary<int, Task>();
            List<Task> taskLista = new List<Task>();
       

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
                        ShowTopics();
                        break;

                    case 3:
                        searchTopics(topicKokoelma);
                        break;

                    case 4:
                        UpdateTopic(topicKokoelma);
                        break;

                    case 5:
                        deleteTopic(topicKokoelma);
                        break;

                    case 6:
                        //AddNewTask ei vielä konkreettisesti lisää taskia niinkuin pitäisi
                        // On vielä vaiheessa
                        AddNewTask(topicKokoelma,taskKokoelma);
                        break;
                    case 7:
                        ShowTasks(taskKokoelma);
                        break;
                    case 8:
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
            Console.WriteLine("7. Show tasks");
            Console.WriteLine("8. To quit");

            Console.Write("Input number to choose what to do:");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine("----------------");
            return input;
        }

        static void AddNewTopic( Dictionary<int, Topic> topicKokoelma)
        {

            string title = "";
            while (true)
            {
                Console.Write("Input topic's title: ");
                title = Console.ReadLine();
                if(title.Length > 255)
                {
                   Console.WriteLine("Maxixum length is 255 characters, please try again.");
                   continue;
                }
                    break;
             }
            

            string description = "";
            while(true)
            {
                Console.Write("Input description for topic: ");
                    description = Console.ReadLine();
                    if(description.Length > 255)
                    {
                        Console.WriteLine("Maxixum length is 255 characters, please try again.");
                        continue;
                    }
                    break;

            }

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

            bool dateInFuture = Class1.FuturePast(startLearningDate);
            double timeSpent;
            if(dateInFuture == true)
            {
                timeSpent = 0;
                    
            }

            else if (dateInFuture == false && inProgress == true)
            {
                timeSpent = (DateTime.Today - startLearningDate).Days;
            }

            else
            {
                timeSpent = (completitionDate - startLearningDate).Days;
            }

         

          
            bool studiesOnTime = Class1.studiesOnTimeOrNot(startLearningDate, completitionDate, estimatedTimeToMaster);
          


            Topic topic = new Topic(title, description, estimatedTimeToMaster, timeSpent, source, startLearningDate, inProgress, completitionDate);

            //topicKokoelma.Add(id, topic);

            using(DiaryAppContext yhteys = new DiaryAppContext())
            {
                Models.Topic uusiTopic = new Models.Topic()
                {

                    Title = title,
                    Description = description,
                    TimeToMaster = estimatedTimeToMaster,
                    TimeSpent = timeSpent,
                    Source = source,
                    StartLearningDate = startLearningDate,
                    InProgress = inProgress,
                    CompletitionDate = completitionDate,
                    StudiesOnTime = studiesOnTime,
                    DateInFuture = dateInFuture

                };

                yhteys.Topics.Add(uusiTopic);
                yhteys.SaveChanges();


            }

            //Kirjoitus Json-tiedostoon
            //WriteTopicsToJson(topicKokoelma);
        }

        //static void ReadTopicsFromJson(Dictionary<int, Topic> topicKokoelma, List<Topic> topicLista)
        //{
        //    string jsonTopics = File.ReadAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsontopics.json");
        //    topicLista = JsonSerializer.Deserialize<List<Topic>>(jsonTopics);
        //    foreach (var item in topicLista)
        //    {
        //        int id = item.Id;
        //        topicKokoelma.Add(id, item);
        //    }
        //}
        //static void WriteTopicsToJson(Dictionary<int, Topic> topicKokoelma)
        //{
            
        //    string jsonFile = JsonSerializer.Serialize(topicKokoelma.Values);
        //    File.WriteAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsontopics.json", jsonFile);
        //}
        //static void WriteTasksToJson (Dictionary <int, Task> taskKokoelma)
        //{
        //    string jsonTasks = JsonSerializer.Serialize(taskKokoelma.Values);
        //    File.WriteAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsonTasks.json", jsonTasks);
           
        //}
        //static void ReadTasksFromJson (Dictionary <int, Task> taskKokoelma, List<Task> taskLista)
        //{
        //    string jsonTasks = File.ReadAllText(@"C:\Users\Harri\source\repos\DiaryApp\jsonTasks.json");
        //    taskLista = JsonSerializer.Deserialize<List<Task>> (jsonTasks);
        //    foreach (var task in taskLista)
        //    {
        //        int taskId = task.TaskId;
        //        taskKokoelma.Add(taskId, task);
        //    }
        //}

        
        static void ShowTopics()
        {
           using (DiaryAppContext yhteys = new DiaryAppContext())
            {
                var topics = yhteys.Topics.Select(topikki => topikki);
                foreach (var topic in topics)
                {
                    Console.WriteLine("Id: " + topic.Id);
                    Console.WriteLine("Title: " + topic.Title);
                    Console.WriteLine("Description: " + topic.Description);
                    Console.WriteLine("Time to master: " + topic.TimeToMaster);
                    Console.WriteLine("Time spent: " + topic.TimeSpent);
                    Console.WriteLine("Source: " + topic.Source);
                    Console.WriteLine("Start learning date: " + topic.StartLearningDate);
                    Console.WriteLine("Completition date: " + topic.CompletitionDate);
                    Console.WriteLine("In progress: " + topic.InProgress);
                    Console.WriteLine("Studies on time: " + topic.StudiesOnTime);
                    
                    Console.WriteLine("-------------------");

                }
            }

        }
        static void AddNewTask( Dictionary <int, Topic> topicKokoelma, Dictionary <int, Task> taskKokoelma)
        {
            ShowTopics();
            //List<string> taskNotes = new List<string>();

            Console.WriteLine("Enter topics Id where you want to add the task");
            int taskId = int.Parse(Console.ReadLine());

            Console.WriteLine("input task title: ");
            string taskTitle = Topic.ReadInputString();

            Console.Write("Input task description: ");
            string taskDescription = Topic.ReadInputString();

            Console.WriteLine("input notes: ");
            string taskNotes = Topic.ReadInputString();
            //taskNotes.Add(note);

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

            using(DiaryAppContext yhteys = new DiaryAppContext())
            {
                Models.Task uusiTask = new Models.Task();
                {
                    uusiTask.TopicId = taskId;
                    uusiTask.Title = taskTitle;
                    uusiTask.Description = taskDescription;
                    uusiTask.Notes = taskNotes;
                    uusiTask.Deadline = taskDeadline;
                    uusiTask.Done = taskDone;
                    uusiTask.Urgency = urgency;

                }

                yhteys.Tasks.Add(uusiTask);
                yhteys.SaveChanges();
                

            
            }

            
        }
        static void ShowTasks (Dictionary <int, Task> taskKokoelma)
        {
            foreach (var task in taskKokoelma.Values)
            {
                Console.WriteLine(task);
            }
        }

        static void searchTopics(Dictionary<int, Topic> topicKokoelma)
        {
            using(DiaryAppContext yhteys = new DiaryAppContext())
            {
                Console.WriteLine("Please input id for the topic you're searching: ");
                int id = int.Parse(Console.ReadLine());
                var topicToLookFor = yhteys.Topics.Where(topic => topic.Id == id).FirstOrDefault();
                Console.WriteLine("Id: " + topicToLookFor.Id);
                Console.WriteLine("Title: " + topicToLookFor.Title);
                Console.WriteLine("Description: " + topicToLookFor.Description);
                Console.WriteLine("Time to master: " + topicToLookFor.TimeToMaster);
                Console.WriteLine("Time spent: " + topicToLookFor.TimeSpent);
                Console.WriteLine("Source: " + topicToLookFor.Source);
                Console.WriteLine("Start learning date: " + topicToLookFor.StartLearningDate);
                Console.WriteLine("Completition date: " + topicToLookFor.CompletitionDate);
                Console.WriteLine("In progress: " + topicToLookFor.InProgress);
                Console.WriteLine("-------------------");

            }
            //Console.Write("Please input id for the topic you're searching: ");
            //int id = Convert.ToInt32(Console.ReadLine());
            //if (topicKokoelma.ContainsKey(id))
            //{
            //    Console.WriteLine("-----------------");
            //    Console.WriteLine(topicKokoelma[id].ToString());
            //}
            //else
            //{
            //    Console.WriteLine("No matches");
            //    ShowMenu();

            //}
        }

        static void UpdateTopic(Dictionary <int, Topic> topicKokoelma)
        {
            ShowTopics();
            
            Console.WriteLine("Please input id for the topic you would like to edit: ");
            int id = int.Parse(Console.ReadLine());
            using (DiaryAppContext yhteys = new DiaryAppContext())
            {
                var topicToEdit = yhteys.Topics.Where(topic => topic.Id == id).Single();
                Console.WriteLine("1) Edit title");
                Console.WriteLine("2) Edit Description");
                Console.WriteLine("3) Edit Estimated time to master");
                Console.WriteLine("4) Edit source");
                Console.WriteLine("5) Edit start learning date: ");
                Console.WriteLine("6) Edit completition date");
                Console.WriteLine("7) Back to main menu");
                Console.WriteLine("What do you want to edit?");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();
                         topicToEdit.Title = newTitle;
                         yhteys.SaveChanges();
                        break;

                    case 2:
                        Console.Write("Enter new description: ");
                        string newDescription = Console.ReadLine();
                        topicToEdit.Description = newDescription;
                        yhteys.SaveChanges();
                        break;

                    case 3:
                        Console.Write("New estimated time to master: ");
                        int newEstimatedTime = Topic.ReadInputInt();
                        topicToEdit.TimeToMaster = newEstimatedTime;
                        yhteys.SaveChanges();
                        break;

                    case 4:
                        Console.Write("Enter new source: ");
                        string newSource = Console.ReadLine();
                        topicToEdit.Source = newSource;
                        yhteys.SaveChanges();
                        break;

                    case 5:
                        Console.WriteLine("Enter new start date for learning(e.g 01/01/2000):");
                        DateTime newStartDate = Topic.ReadDateTime();
                        topicToEdit.StartLearningDate = newStartDate;
                        yhteys.SaveChanges();
                        break;

                    case 6:
                        Console.WriteLine("Enter new finish date(01/01/2000): ");
                        DateTime newCompletitionDate = Topic.ReadDateTime();
                        topicToEdit.CompletitionDate = newCompletitionDate;
                        yhteys.SaveChanges();
                       
                        break;

                    case 7:
                        ShowMenu();
                        break;
                 
                    default:
                        break;
                }
                }

        
            
        }

        static void deleteTopic(Dictionary<int, Topic> topicKokoelma)
        {
            ShowTopics();
            Console.Write("Please input id for the topic you would like to delete:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Are you sure you want to delete this topic?(yes/no)");
            string input = Console.ReadLine();
            if(input.Equals("yes"))
            {
                using(DiaryAppContext yhteys = new DiaryAppContext())
                {
                    var topicToDelete = yhteys.Topics.Where(topic => topic.Id == id).FirstOrDefault();
                    yhteys.Topics.Remove(topicToDelete);
                    yhteys.SaveChanges();

                }
            }





            //foreach (var item in topicKokoelma)
            //{
            //    Console.WriteLine(item.Value);
            //    Console.WriteLine("-------------------------------");
            //}
            //Console.Write("Please input id for the topic you would like to delete: ");
            //int id = int.Parse(Console.ReadLine());
            //if (topicKokoelma.ContainsKey(id))
            //{
            //    Console.WriteLine(topicKokoelma[id].ToString());
            //    Console.WriteLine("Are you sure you want to delete this topic(yes/no): ");
            //    string input = Console.ReadLine();
            //    if (input.Equals("yes")) {
            //        topicKokoelma.Remove(id);
            //        WriteTopicsToJson(topicKokoelma);
            //    }
            //    else
            //    { Console.WriteLine("Delete cancelled");
            //      Console.WriteLine("-------------------------------");
            //    }
            //}

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


    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime Deadline { get; set; }
        public Enum Priority { get; set; }
        public bool Done { get; set; }

        public Task (int taskId, string taskTitle, string taskDescription, string taskNotes, DateTime taskDeadline, bool taskDone)
        {
            TaskId = taskId;
            Title = taskTitle;
            Description = taskDescription;
            Notes = taskNotes;
            Deadline = taskDeadline;
            //Priority = taskPriority;
            Done = taskDone;
        }

        public Task ()
        {

        }

        public override string ToString()
        {
            return "TaskId: " + TaskId +
                "\n Tasks Title: " + Title +
                "\n Description: " + Description +
                "\n Notes: " + Notes +
                "\n Deadline: " + Deadline.ToString("dd/MM/yyyy") +
                "\n Done: " + Done;
        }
    }


}










         



















