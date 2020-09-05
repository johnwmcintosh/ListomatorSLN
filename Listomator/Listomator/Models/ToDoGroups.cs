using Listomator.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Listomator.Models
{
    public class ToDoGroups : NotifyBase
    {
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // OBSERVABLES
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<ToDoGroup> _groups;
        public ObservableCollection<ToDoGroup> Groups { get => _groups; set => SetProperty(ref _groups, value); }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Commands
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICommand AddGroupCommand { get; private set; }
        private void OnAddGroup(string name)
        {
            Groups.Add(Locator.GetClass<ToDoGroup>(name));
        }

        public ICommand RemoveListCommand { get; private set; }
        private void OnRemoveList(string name)
        {
            var td = Groups.FirstOrDefault(t => t.GroupName == name);
            if (td != null) { Groups.Remove(td); }
        }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // INITIALIZERS
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ToDoGroups()
        {
            AddGroupCommand = new Command<string>(OnAddGroup);
            RemoveListCommand = new Command<string>(OnRemoveList);
        }

        public override void Init()
        {
            
        }

        public override void Init(object data)
        {
            
        }
    }
}
