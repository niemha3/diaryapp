﻿using System;
using System.Collections.Generic;
using System.IO;
namespace DiaryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string path = @"C:\Users\Harri\source\repos\DiaryApp\topics.txt";
            int id = 0;
            int taskId = 0;
            bool menu = true;

            while (menu)
            {
               int userInput = ShowMenu();
            
                switch (userInput)
                {
                    case 1:

                        id++;
                        AddNewTopic(path, id);
                        break;

                    case 2:

                        Console.WriteLine();
                        ReadTextFile(path);
                        Console.WriteLine();
                        break;

                    case 3:
                        //AddNewTask ei vielä konkreettisesti lisää taskia niinkuin pitäisi
                        // On vielä vaiheessa
                        taskId++;
                        AddNewTask(taskId);
                        break;

                    case 4:
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
            Console.WriteLine("3. Add new tasks ");
            Console.WriteLine("4. to quit");
            Console.Write("Input number to choose what to do:");
            int input = int.Parse(Console.ReadLine());
            return input;
        }

        static void AddNewTopic(string path, int id)
        {
            id++;
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



            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(topic.ToString());
                sw.WriteLine();
            }

        }

        static void ReadTextFile(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);

                }
            }
            Console.WriteLine();
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










         



















