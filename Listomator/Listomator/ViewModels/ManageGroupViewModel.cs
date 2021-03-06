﻿using Listomator.Core;
using Listomator.Data;
using Listomator.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ManageGroupViewModel : NotifyBase
    {
        private ListomatorRepository _context;

        private ToDoGroup _todoGroup;
        private ObservableCollection<ToDoGroup> _todoGroups;

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string _groupName;
        public string GroupName { get => _groupName; set => SetProperty(ref _groupName, value); }

        private bool _isSavingPossible = false;
        public bool IsSavingPossible { get => _isSavingPossible; set => SetProperty(ref _isSavingPossible, value); }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // COMMANDS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand SaveGroupNameCommand { get; private set; }
        private async void OnSaveGroupName()
        {
            if (_todoGroup == null)
            {
                await _context.AddGroupAsync(GroupName);
                _todoGroups.Add(new ToDoGroup{GroupName = GroupName });
            }
            else
            {
                await _context.UpdateGroupNameAsync(_todoGroup.GroupName, GroupName);
                _todoGroup.GroupName = GroupName;
            }
            
            App.Locator.NavigationService.GoBack();
        }

        public ICommand CancelGroupNameCommand { get; private set; }
        private void OnCancelGroupName()
        {
            App.Locator.NavigationService.GoBack();
        }

        public ICommand GroupNameTextChangedCommand { get; private set; }
        private void OnGroupNameTextChanged()
        {
            IsSavingPossible = (GroupName.Length > 0);
        }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITALIZERES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ManageGroupViewModel(ListomatorRepository context)
        {
            SaveGroupNameCommand = new Command(OnSaveGroupName);
            CancelGroupNameCommand = new Command(OnCancelGroupName);
            GroupNameTextChangedCommand = new Command(OnGroupNameTextChanged);
            
            _context = context;
        }

        public override void Init()
        {
            GroupName = string.Empty;
            _todoGroup = null;
        }

        public override void Init(object data)
        {
            if (data is ToDoGroup group)
            {
                _todoGroup = group;
                _todoGroups = null;
                GroupName = group.GroupName;
            }
            else if (data is ObservableCollection<ToDoGroup> groups)
            {
                _todoGroup = null;
                _todoGroups = groups;
                GroupName = string.Empty;
            }
        }
    }
}
