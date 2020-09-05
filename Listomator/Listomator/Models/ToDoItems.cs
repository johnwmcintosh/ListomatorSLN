using Listomator.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.Models
{
    public class ToDoItems : NotifyBase
    {
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private string _listName;
        public string ListName { get => _listName; set => SetProperty(ref _listName, value); }

        private ObservableCollection<ToDoItem> _items = new ObservableCollection<ToDoItem>();
        public ObservableCollection<ToDoItem> Items { get => _items; set => SetProperty(ref _items, value); }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand AddItemCommand { get; private set; }
        private void OnAddItem(string name)
        {
            Items.Add(Locator.GetClass<ToDoItem>(name));
        }

        public ICommand RemoveItemCommand { get; private set; }
        private void OnRemoveItem(string name)
        {
            var td = Items.FirstOrDefault(t => t.ItemName == name);
            if (td != null) {td.CleanUp(); Items.Remove(td);}
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoItems()
        {
            AddItemCommand = new Command<string>(OnAddItem);
        }
        
        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            ListName = (string) data;
        }
    }
}
