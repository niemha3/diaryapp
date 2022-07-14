using System;
namespace DiaryApp
{
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










         



















