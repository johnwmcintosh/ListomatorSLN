using System;
using System.ComponentModel.DataAnnotations;

namespace Listomator.Data.Model
{
    public class ToDoItem
    {
        [Key]
        public string ToDoItemName { get; set; }
        public bool IsComplete { get; set; }
        public bool UseDueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public ToDoGroup ToDoGroup { get; set; }
    }
}
