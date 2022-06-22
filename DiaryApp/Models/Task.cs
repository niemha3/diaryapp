using System;
using System.Collections.Generic;

#nullable disable

namespace DiaryApp.Models
{
    public partial class Task
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime? Deadline { get; set; }
        public string Urgency { get; set; }
        public bool? Done { get; set; }
    }
}
