using Listomator.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Listomator.Data
{
    public class ListomatorRepository
    {
        public ListomatorRepository()
        {
            using (var db = new ListomatorContext())
            {
                //db.Database.Migrate();
                //db.Database.EnsureCreated();
            }
        }

        public async Task<List<ToDoGroup>> GetGroupsAsync()
        {
            using (var db = new ListomatorContext())
            {
                return await db.ToDoGroups.OrderBy(t => t.ToDoGroupName).ToListAsync();
            }
        }

        public async Task<List<ToDoItem>> GetGroupItemsAsync(string name)
        {
            using (var db = new ListomatorContext())
            {
                return await db.ToDoItems.Where(t => t.ToDoGroup.ToDoGroupName == name).ToListAsync();
            }
        }

        public async Task UpdateGroupNameAsync(string oldName, string newName)
        {
            using (var db = new ListomatorContext())
            {
                var group = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == oldName);
                if (group != null)
                {
                    group.ToDoGroupName = newName;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task AddGroupAsync(string name)
        {
            using (var db = new ListomatorContext())
            {
                if (!(await db.ToDoGroups.AnyAsync(t => t.ToDoGroupName == name)))
                {
                    await db.ToDoGroups.AddAsync(new ToDoGroup {ToDoGroupName = name});
                    await db.SaveChangesAsync();
                }
                else
                    throw new ConstraintException($"Group {name} already exists");
            }
        }

        public async Task RemoveGroupAsync(string name)
        {
            using (var db = new ListomatorContext())
            {
                try
                {

                    var group = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == name);
                    if (group != null)
                        db.ToDoGroups.Remove(group);

                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task AddItemToGroupAsync(string groupName, string itemName, bool isComplete, bool useDueDate, DateTime dueDate = default)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException($"Attempting to add {itemName} to a non-existant group name {groupName}");

                if (!(await db.ToDoItems.AnyAsync(t => t.ToDoGroup.ToDoGroupName == groupName && t.ToDoItemName == itemName)))
                {
                    DateTime completionDate = isComplete ? DateTime.Now : default;
                    await db.ToDoItems.AddAsync(new ToDoItem {ToDoItemName = itemName, DueDate = dueDate, IsComplete = isComplete, CompletionDate = completionDate, UseDueDate = dueDate != default, ToDoGroup = g});
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveItemFromGroupAsync(string groupName, string itemName)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException(
                        $"Attempting to remove {itemName} from a non-existant group name {groupName}");

                var i = g.ToDoItems.FirstOrDefault(t => t.ToDoItemName == itemName);

                if (i != null)
                    db.ToDoItems.Remove(i);
                else
                    throw new ArgumentOutOfRangeException($"Attempting to remove an item {itemName} that doesn't exist in group {groupName}");

                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateItemAsync(string groupName, string itemName, bool isComplete, bool useDueDate, DateTime dueDate, DateTime completionDate)
        {
            using (var db = new ListomatorContext())
            {
                var item = db.ToDoItems.FirstOrDefault(t => t.ToDoGroup.ToDoGroupName == groupName && t.ToDoItemName == itemName);
                if (item != null)
                {
                    item.DueDate = dueDate;
                    item.UseDueDate = useDueDate;
                    item.IsComplete = isComplete;
                    item.CompletionDate = completionDate;
                    await db.SaveChangesAsync();
                }
            }

        }

        public async Task UpdateItemNameAsync(string groupName, string oldItemName, string newItemName)
        {
            // WARNING THIS CHANGES A KEY
            // MUST FIRST DELETE THE ROW

            using (var db = new ListomatorContext())
            {
                var item = db.ToDoItems.FirstOrDefault(t => t.ToDoGroup.ToDoGroupName == groupName && t.ToDoItemName == oldItemName);

                var a = item;
                if (item != null)
                {
                    var copy = new ToDoItem { CompletionDate = item.CompletionDate, IsComplete = item.IsComplete, DueDate = item.DueDate, UseDueDate = item.UseDueDate, ToDoItemName = newItemName, ToDoGroup = item.ToDoGroup};
                    db.ToDoItems.Remove(item);
                    await db.SaveChangesAsync();

                    await db.ToDoItems.AddAsync(copy);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task ChangeItemGroupAsync(string oldGroupName, string newGroupName, string itemName)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == oldGroupName);
                if (g == null)  throw new ArgumentOutOfRangeException($"Attempting to update {itemName} to a non-existant group name {newGroupName}");

                var item = g.ToDoItems.FirstOrDefault(t => t.ToDoGroup.ToDoGroupName == oldGroupName && t.ToDoItemName == itemName);
                if (item != null)
                {
                    item.ToDoGroup = g;
                    await db.SaveChangesAsync();
                }
            }

        }

        public async Task UpdateItemCompletionAsync(string groupName, string itemName, bool isComplete)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException($"Attempting to update {itemName} to a non-existant group name {groupName}");

                var item = g.ToDoItems.FirstOrDefault(t => t.ToDoItemName == itemName);
                if (item != null)
                {
                    item.IsComplete = isComplete;
                    item.CompletionDate = isComplete ? DateTime.Now : default;

                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
