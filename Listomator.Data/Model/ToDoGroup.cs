using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listomator.Data.Model
{
    public class ToDoGroup
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Key]
        public string ToDoGroupName { get; set; } 

        public List<ToDoItem> ToDoItems { get; set; }
    }
}
