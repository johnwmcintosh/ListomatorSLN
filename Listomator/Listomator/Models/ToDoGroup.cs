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

        private ObservableCollection<ToDoItems> _items = new ObservableCollection<ToDoItems>();
        public ObservableCollection<ToDoItems> Items { get => _items; set => SetProperty(ref _items, value); }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand AddListCommand { get; private set; }
        private void OnAddList(string name)
        {
            Items.Add(Locator.GetClass<ToDoItems>(name));
        }

        public ICommand RemoveListCommand { get; private set; }
        private void OnRemoveList(string name)
        {
            var td = Items.FirstOrDefault(t => t.ListName == name);
            if (td != null) { Items.Remove(td); }
        }

        public ICommand NavigateToGroupCommand { get; private set; }
        private void OnNavigateToGroup(ToDoGroup group)
        {
            App.Locator.NavigationService.NavigateTo(Locator.ManageList, group);
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoGroup()
        {
            AddListCommand = new Command<string>(OnAddList);
            RemoveListCommand = new Command<string>(OnRemoveList);
            NavigateToGroupCommand = new Command<ToDoGroup>(OnNavigateToGroup);
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
