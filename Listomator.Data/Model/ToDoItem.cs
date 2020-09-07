using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listomator.Data.Model
{
    public class ToDoItem
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Key]
        public string ToDoItemName { get; set; }
        public bool IsComplete { get; set; }
        public bool UseDueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public ToDoGroup ToDoGroup { get; set; }
    }
}
