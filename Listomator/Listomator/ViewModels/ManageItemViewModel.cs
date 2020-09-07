using System;
using System.Collections.ObjectModel;
using Listomator.Core;
using Listomator.Data;
using Listomator.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ManageItemViewModel : NotifyBase
    {
        private ListomatorRepository _context;

        private ToDoItem _todoItem;
        private ObservableCollection<ToDoItem> _todoItems;
        private ToDoGroup _group;

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string _groupName;
        public string GroupName { get => _groupName; set => SetProperty(ref _groupName, value); }

        private string _itemName;
        public string ItemName { get => _itemName; set => SetProperty(ref _itemName, value); }

        private bool _isComplete;
        public bool IsComplete { get => _isComplete; set => SetProperty(ref _isComplete, value); }

        private bool _useDueDate;
        public bool UseDueDate { get => _useDueDate; set => SetProperty(ref _useDueDate, value); }

        private DateTime _dueDate;
        public DateTime DueDate { get => _dueDate; set => SetProperty(ref _dueDate, value); }

        private bool _isNameReadOnly;
        public bool IsNameReadOnly { get => _isNameReadOnly; set => SetProperty(ref _isNameReadOnly, value); }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // COMMANDS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand SaveItemCommand { get; private set; }
        private async void OnSaveItemName()
        {
            if (_todoItem == null)
            {
                await _context.AddItemToGroupAsync(_group.GroupName, ItemName, IsComplete, UseDueDate, DueDate);
                _todoItems.Add(new ToDoItem {ItemName = ItemName, UseDueDate = UseDueDate, DueDate = DueDate, IsComplete = IsComplete, Group = _group});
            }
            else
            {
                if (_todoItem.ItemName != ItemName)
                {
                    await _context.UpdateItemNameAsync(_group.GroupName, _todoItem.ItemName, ItemName);
                    _todoItem.ItemName = ItemName;
                }

                DateTime completionDate = default;
                if (IsComplete != _todoItem.IsComplete && IsComplete) completionDate = DateTime.Now;

                await _context.UpdateItemAsync(_group.GroupName, ItemName, IsComplete, UseDueDate, DueDate, completionDate);
                _todoItem.IsComplete = IsComplete;
                _todoItem.UseDueDate = UseDueDate;
                _todoItem.DueDate = DueDate;
                _todoItem.CompletionDate = completionDate;
            }

            App.Locator.NavigationService.GoBack();
        }

        public ICommand CancelItemChangeCommand { get; private set; }
        private void OnCancelItemChange()
        {
            App.Locator.NavigationService.GoBack();
        }

        public ICommand ItemNameTextChangedCommand { get; private set; }
        private void OnItemNameTextChanged()
        {
        }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITALIZERES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ManageItemViewModel(ListomatorRepository context)
        {
            _context = context;
            SaveItemCommand = new Command(OnSaveItemName);
            CancelItemChangeCommand = new Command(OnCancelItemChange);
            ItemNameTextChangedCommand = new Command(OnItemNameTextChanged);
        }

        public override void Init()
        {
            ItemName = string.Empty;
            IsComplete = false;
            UseDueDate = false;
            DueDate = default;
            _todoItems = null;
        }

        public override void Init(object data)
        {
            if (data is ToDoItem item)
            {
                IsNameReadOnly = true;
                _group = item.Group;
                _todoItem = item;
                _todoItems = null;
                ItemName = item.ItemName;
                IsComplete = item.IsComplete;
                UseDueDate = item.UseDueDate;
                DueDate = item.DueDate;
            }
            else if (data is ToDoGroup group)
            {
                IsNameReadOnly = false;
                _group = group;
                _todoItem = null;
                _todoItems = group.Items;
                ItemName = string.Empty;
                IsComplete = false;
                UseDueDate = false;
                DueDate = default;
            }
        }
    }
}
