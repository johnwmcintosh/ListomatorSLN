using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listomator.Data.Model
{
    public class ToDoGroup
    {
        [Key]
        public string ToDoGroupName { get; set; } 

        public List<ToDoItem> ToDoItems { get; set; }
    }
}
