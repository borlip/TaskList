using System;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int RecordId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public Priority Priority
        {
            get { return (Priority) PriorityValue; }
            set { PriorityValue = (int) value; }
        }

        public int PriorityValue { get; set; }
    }
}