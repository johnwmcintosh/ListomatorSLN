using Listomator.Core;
using Listomator.Models;
using Listomator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listomator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageList : ContentPage
    {
        public ManageList()
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageListViewModel>();
        }

        public ManageList(ToDoGroup group)
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageListViewModel>(group);
        }
    }
}