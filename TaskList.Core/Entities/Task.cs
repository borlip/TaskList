using System;

namespace TaskList.Core.Entities
{
    public enum Priority
    {
        Normal,
        Low,
        High
    }

    public class Task
    {
        public int RecordId { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }
    }
}