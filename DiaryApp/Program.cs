using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiaryApp.Models;
using ClassLibraryForDates;
namespace DiaryApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            bool menu = true;
            while (menu)
            {
               int userInput = ShowMenu();
            
                switch (userInput)
                {
                    case 1:

                        AddNewTopic();
                        break;

                    case 2:
                        ShowTopics();
                        break;

                    case 3:
                        searchTopics();
                        break;

                    case 4:
                        UpdateTopic();
                        break;

                    case 5:
                        DeleteTopic();
                        break;

                    case 6:
                        //AddNewTask ei vielä konkreettisesti lisää taskia niinkuin pitäisi
                        // On vielä vaiheessa
                        AddNewTask();
                        break;
                    case 7:
                        ShowTasks();
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

        static async System.Threading.Tasks.Task AddNewTopic()
        {

            string title;
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


            string description;
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

            double estimatedTimeToMaster;
            while (true)
            {
                try
                {
                    Console.Write("Estimated time in days to learn the topic: ");
                    estimatedTimeToMaster = Topic.ReadDoubleInput();

                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a number in correct form.");
                    continue;
                }
                break;

            }

            string source;
            while (true)
            {
                Console.Write("What was your source website or book name: ");
                source = Topic.ReadInputString();
                if(source.Length > 255)
                {
                    Console.WriteLine("Maxixum length is 255 characters, please try again.");
                    continue;
                }
                break;
            }


            DateTime startLearningDate;
            while (true)
            {
                try
                {
                    Console.Write("Input your starting date (e.g 11/06/1993): ");
                    startLearningDate = Topic.ReadDateTime();

                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter the date in right format (e.g 11/06/1993)");
                    continue;
                }
                break;
            }

            DateTime completitionDate;
            while (true)
            {
                try
                {
                    Console.Write("Please input topics completition date (e.g 11/06/1993): ");
                    completitionDate = Topic.ReadDateTime();

                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter the date in right format (e.g 11/06/1993)");
                    continue;
                }
                break;
            }


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
               await yhteys.SaveChangesAsync();
            }

        }

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
        static async System.Threading.Tasks.Task AddNewTask()
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
               await yhteys.SaveChangesAsync();
                

            
            }

            
        }
        static void ShowTasks ()
        {
            foreach (var task in taskKokoelma.Values)
            {
                Console.WriteLine(task);
            }
        }

        static void searchTopics()
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
   
        }

        static async System.Threading.Tasks.Task UpdateTopic()
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
                        await yhteys.SaveChangesAsync();
                        break;

                    case 2:
                        Console.Write("Enter new description: ");
                        string newDescription = Console.ReadLine();
                        topicToEdit.Description = newDescription;
                        await yhteys.SaveChangesAsync();
                        break;

                    case 3:
                        Console.Write("New estimated time to master: ");
                        int newEstimatedTime = Topic.ReadInputInt();
                        topicToEdit.TimeToMaster = newEstimatedTime;
                       await yhteys.SaveChangesAsync();
                        break;

                    case 4:
                        Console.Write("Enter new source: ");
                        string newSource = Console.ReadLine();
                        topicToEdit.Source = newSource;
                        await yhteys.SaveChangesAsync();
                        break;

                    case 5:
                        Console.WriteLine("Enter new start date for learning(e.g 01/01/2000):");
                        DateTime newStartDate = Topic.ReadDateTime();
                        topicToEdit.StartLearningDate = newStartDate;
                        await yhteys.SaveChangesAsync();
                        break;

                    case 6:
                        Console.WriteLine("Enter new finish date(01/01/2000): ");
                        DateTime newCompletitionDate = Topic.ReadDateTime();
                        topicToEdit.CompletitionDate = newCompletitionDate;
                        await yhteys.SaveChangesAsync();
                       
                        break;

                    case 7:
                        ShowMenu();
                        break;
                 
                    default:
                        break;
                }
                }

        
            
        }

        static async System.Threading.Tasks.Task DeleteTopic()
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
                   await yhteys.SaveChangesAsync();

                   
                }
            }


        }



    }


}










         



















