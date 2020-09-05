
using Listomator.Core;
using Listomator.Models;
using Listomator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listomator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageGroup : ContentPage
    {
        public ManageGroup()
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageGroupViewModel>();
        }

        public ManageGroup(ToDoGroup group)
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageGroupViewModel>(group);
        }
    }
}