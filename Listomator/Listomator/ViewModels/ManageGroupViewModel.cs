using Listomator.Core;
using Listomator.Data;
using System.Windows.Input;
using Listomator.Models;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ManageGroupViewModel : NotifyBase
    {
        private ListomatorRepository _context;

        private ToDoGroup group;

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
            await _context.UpdateGroupNameAsync(group.GroupName, GroupName);
            group.GroupName = GroupName;
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
            
        }

        public override void Init(object data)
        {
            group = (ToDoGroup) data;
            GroupName = group.GroupName;
        }
    }
}
