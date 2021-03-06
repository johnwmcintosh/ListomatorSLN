﻿using Listomator.Core;
using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.Models
{
    /// <summary>
    /// Written by John McIntosh
    /// </summary>
    public class ToDoItem : NotifyBase
    {
        public delegate void DueDateAlert(ToDoItem item);
        public event DueDateAlert OnItemDue;

        private readonly Timer _dueDateTimer = new Timer();

        public ToDoGroup Group { get; set; }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string _itemName = string.Empty;
        public string ItemName { get => _itemName; set => SetProperty(ref _itemName, value); }

        private bool _isComplete;
        public bool IsComplete
        {
            get => _isComplete;
            set { _dueDateTimer.Stop(); SetProperty(ref _isComplete, value); TodoAttributes = IsComplete ? FontAttributes.Italic : FontAttributes.None; }
        }
        
        private bool _useDueDate;
        public bool UseDueDate
        {
            get { _dueDateTimer.Stop(); return _useDueDate; }
            set { SetProperty(ref _useDueDate, value); _dueDateTimer.Start(); }
        }

        private bool _isDue = false;
        public bool IsDue { get =>_isDue; set => SetProperty(ref _isDue, value); }

        private DateTime _dueDate;
        public DateTime DueDate { get => _dueDate; set => SetProperty(ref _dueDate, value); }

        private DateTime _completionDate;
        public DateTime CompletionDate { get => _completionDate; set => SetProperty(ref _completionDate, value); }

        private FontAttributes _todoAttributes;
        public FontAttributes TodoAttributes { get => _todoAttributes; set => SetProperty(ref _todoAttributes,value); }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand SetCompleteCommand { get; private set; }
        private void OnSetComplete()
        {
            IsComplete = true;
            IsDue = false;
            CompletionDate = DateTime.Now;
        }

        public ICommand RestoreCommand { get; private set; }
        private void OnRestore()
        {
            IsComplete = false;
            CompletionDate = default;
        }

        public ICommand NavigateToManageItemCommand { get; private set; }
        private void OnNavigateToMangeItem(ToDoItem item)
        {
            App.Locator.NavigationService.NavigateTo(Locator.ManageItem, item);
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HANDLERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void _dueDateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DueDate < DateTime.Now)
            {
                IsDue = true;
                _dueDateTimer.Stop();
                OnItemDue?.Invoke(this);
            }
        }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoItem()
        {
            SetCompleteCommand = new Command(OnSetComplete);
            RestoreCommand = new Command(OnRestore);
            NavigateToManageItemCommand = new Command<ToDoItem>(OnNavigateToMangeItem);

            _dueDateTimer.Elapsed += _dueDateTimer_Elapsed;
            _dueDateTimer.Interval = 5000;
        }

        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            if (data is Data.Model.ToDoItem item)
            {
                ItemName = item.ToDoItemName;
                CompletionDate = item.CompletionDate;
                IsComplete = item.IsComplete;
                DueDate = item.DueDate;
                UseDueDate = item.UseDueDate;
            }

            if (!IsComplete && UseDueDate && DueDate < DateTime.Now)
            {
                IsDue = true;
                OnItemDue?.Invoke(this);
            }
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // FINALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CleanUp()
        {
            _dueDateTimer.Stop();
            _dueDateTimer.Close();
        }
    }
}
