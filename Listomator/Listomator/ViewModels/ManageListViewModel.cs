using Listomator.Core;
using Listomator.Data;
using Listomator.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ManageListViewModel : NotifyBase
    {
        private ListomatorRepository _context;

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ToDoGroup _group;
        public ToDoGroup Group { get => _group ; set => SetProperty(ref _group, value); }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // COMMANDS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand RefreshListCommand { get; private set; }
        private void OnRefreshList()
        {
            Init();
        }

        public ICommand NavigateToManageListCommand { get; private set; }
        private void OnNavigateToManageList()
        {
        }

        public ICommand ManangeListsCommand { get; private set; }
        private void OnManageLists(ToDoGroup group)
        {
        }

        public ICommand DeleteListCommand { get; private set; }
        private async void OnDeleteList(ToDoItems items)
        {
        }


        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITALIZERES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ManageListViewModel(ListomatorRepository context)
        {
            _context = context;
            RefreshListCommand = new Command(OnRefreshList);
        }

        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            if (data is ToDoGroup group)
                Group = group;
        }
    }
}
