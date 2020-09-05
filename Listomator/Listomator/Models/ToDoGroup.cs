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

        private ObservableCollection<ToDoItems> _group = new ObservableCollection<ToDoItems>();
        public ObservableCollection<ToDoItems> Group { get => _group; set => SetProperty(ref _group, value); }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand AddListCommand { get; private set; }
        private void OnAddList(string name)
        {
            Group.Add(Locator.GetClass<ToDoItems>(name));
        }

        public ICommand RemoveListCommand { get; private set; }
        private void OnRemoveList(string name)
        {
            var td = Group.FirstOrDefault(t => t.ListName == name);
            if (td != null) { Group.Remove(td); }
        }
        
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoGroup()
        {
            AddListCommand = new Command<string>(OnAddList);
            RemoveListCommand = new Command<string>(OnRemoveList);
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
