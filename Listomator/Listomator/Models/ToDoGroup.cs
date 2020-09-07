using Listomator.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.Models
{
    public class ToDoGroup : NotifyBase
    {
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string _groupName;
        public string GroupName { get => _groupName; set => SetProperty(ref _groupName, value); }

        private ObservableCollection<ToDoItem> _items = new ObservableCollection<ToDoItem>();
        public ObservableCollection<ToDoItem> Items { get => _items; set => SetProperty(ref _items, value); }


        public bool IsChildDue => Items.Any(t => t.IsDue);

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand AddListCommand { get; private set; }
        private void OnAddList(string name)
        {
            Items.Add(Locator.GetClass<ToDoItem>(name));
        }

        public ICommand RemoveListCommand { get; private set; }
        private void OnRemoveList(string name)
        {
            var td = Items.FirstOrDefault(t => t.ItemName == name);
            if (td != null) { Items.Remove(td); }
        }

        public ICommand NavigateToManageGroupCommand { get; private set; }
        private void OnNavigateToManageGroupCommand()
        {
            App.Locator.NavigationService.NavigateTo(Locator.ManageGroup, this);
        }

        public ICommand NavigateToItemsListCommand { get; private set; }
        private void OnNavigateToItemsList()
        {
            App.Locator.NavigationService.NavigateTo(Locator.ItemsList, this);
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoGroup()
        {
            AddListCommand = new Command<string>(OnAddList);
            RemoveListCommand = new Command<string>(OnRemoveList);
            NavigateToManageGroupCommand = new Command(OnNavigateToManageGroupCommand);
            NavigateToItemsListCommand = new Command(OnNavigateToItemsList);
        }

        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            GroupName = (string) data;
        }
    }
}
