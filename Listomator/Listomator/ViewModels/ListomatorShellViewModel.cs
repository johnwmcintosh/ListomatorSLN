using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Listomator.Core;
using Listomator.Data;
using Listomator.Models;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ListomatorShellViewModel : NotifyBase
    {
        private ListomatorRepository _context;


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<ToDoGroup> _toDoGroups = new ObservableCollection<ToDoGroup>();
        public ObservableCollection<ToDoGroup> ToDoGroups { get => _toDoGroups; set => SetProperty(ref _toDoGroups, value); }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // COMMANDS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand NavigateToManageGroupCommand { get; private set; }
        private void OnNavigateToManageGroup()
        {
            App.Locator.NavigationService.NavigateTo(Locator.ManageGroup);
        }

        public ICommand ManangeGroupCommand { get; private set; }
        private void OnManageGroup(ToDoGroup group)
        {
            App.Locator.NavigationService.NavigateTo(Locator.ManageGroup, group);
        }

        public ICommand DeleteGroupCommand { get; private set; }
        private async void OnDeleteGroup(ToDoGroup group)
        {
            await _context.RemoveGroupAsync(group.GroupName);
            ToDoGroups.Remove(group);
        }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ListomatorShellViewModel(ListomatorRepository context)
        {
            _context = context;

            NavigateToManageGroupCommand = new Command(OnNavigateToManageGroup);
            ManangeGroupCommand = new Command<ToDoGroup>(OnManageGroup);
            DeleteGroupCommand = new Command<ToDoGroup>(OnDeleteGroup);
        }


        public override async void Init()
        {
            var groups = await _context.GetGroupsAsync();

            foreach (var group in groups)
            {
                ToDoGroups.Add(new ToDoGroup { GroupName = group.ToDoGroupName });
            }
        }

        public override void Init(object data)
        {
            
        }
    }
}
