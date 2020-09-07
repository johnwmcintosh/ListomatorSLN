using Listomator.Core;
using Listomator.Data;
using Listomator.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.ViewModels
{
    public class ItemsListViewModel : NotifyBase
    {
        private ListomatorRepository _context;

        private ToDoGroup _group;

        public string GroupName => _group.GroupName;

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems { get => _toDoItems; set => SetProperty(ref _toDoItems, value); }
        
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // COMMANDS
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand RefreshListCommand { get; private set; }
        private void OnRefreshList()
        {
            Init();
        }

        public ICommand ManangeItemCommand { get; private set; }
        private void OnManageItem(ToDoItem item)
        {
            if (item == null)
                App.Locator.NavigationService.NavigateTo(Locator.ManageItem, _group);
            else
                App.Locator.NavigationService.NavigateTo(Locator.ManageItem, item);
        }

        public ICommand DeleteItemCommand { get; private set; }
        private async void OnDeleteItem(ToDoItem item)
        {
            await _context.RemoveItemFromGroupAsync(_group.GroupName, item.ItemName);
            ToDoItems.Remove(item);
        }

        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITALIZERES
        // ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ItemsListViewModel(ListomatorRepository context)
        {
            _context = context;

            RefreshListCommand = new Command(OnRefreshList);
            ManangeItemCommand = new Command<ToDoItem>(OnManageItem);
            DeleteItemCommand = new Command<ToDoItem>(OnDeleteItem);
        }

        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            if (data is ToDoGroup group)
            {
                _group = group;
                ToDoItems = group.Items;
            }
        }
    }
}
