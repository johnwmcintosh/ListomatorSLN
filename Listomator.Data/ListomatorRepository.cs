using Listomator.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Listomator.Data
{
    public class ListomatorRepository
    {
        public ListomatorRepository()
        {
            using (var db = new ListomatorContext())
                db.Database.EnsureCreated();
        }

        public async Task<List<ToDoGroup>> GetGroupsAsync()
        {
            using (var db = new ListomatorContext())
                return await db.ToDoGroups.OrderBy(t => t.ToDoGroupName).ToListAsync();
        }

        public async Task<List<ToDoItem>> GetGroupItemsAsync(string name)
        {
            using (var db = new ListomatorContext())
                return await db.ToDoItems.Where(t => t.ToDoGroup.ToDoGroupName == name).ToListAsync();
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
                var group = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == name);
                if (group != null)
                    db.ToDoGroups.Remove(group);
            }
        }

        public async Task AddItemToGroupAsync(string groupName, string itemName, DateTime dueDate = default)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException(
                        $"Attempting to add {itemName} to a non-existant group name {groupName}");

                if (!(await db.ToDoItems.AnyAsync(t =>
                    t.ToDoGroup.ToDoGroupName == groupName && t.ToDoItemName == itemName)))
                {
                    await db.ToDoItems.AddAsync(new ToDoItem
                        {DueDate = dueDate, UseDueDate = dueDate != default, ToDoGroup = g});
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateItemDueDateAsync(string groupName, string itemName, DateTime dueDate)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException(
                        $"Attempting to add {itemName} to a non-existant group name {groupName}");

                var item = g.ToDoItems.FirstOrDefault(t => t.ToDoItemName == itemName);
                if (item != null)
                {
                    item.DueDate = dueDate;
                    item.UseDueDate = true;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateItemNameAsync(string groupName, string oldItemName, string newItemName)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException($"Attempting to update {oldItemName} to a non-existant group name {groupName}");

                var item = g.ToDoItems.FirstOrDefault(t => t.ToDoItemName == oldItemName);
                if (item != null)
                {
                    item.ToDoItemName = newItemName;
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateItemUseDueDateAsync(string groupName, string itemName, bool useDueDate)
        {
            using (var db = new ListomatorContext())
            {
                var g = await db.ToDoGroups.FirstOrDefaultAsync(t => t.ToDoGroupName == groupName);
                if (g == null)
                    throw new ArgumentOutOfRangeException($"Attempting to update {itemName} to a non-existant group name {groupName}");

                var item = g.ToDoItems.FirstOrDefault(t => t.ToDoItemName == itemName);
                if (item != null)
                {
                    item.UseDueDate = useDueDate;
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
