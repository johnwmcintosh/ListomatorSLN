using Listomator.Core;
using Listomator.Models;
using Listomator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listomator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageItem : ContentPage
    {
        public ManageItem()
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageItemViewModel>();
        }

        public ManageItem(ToDoItem item)
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageItemViewModel>(item);
        }

        public ManageItem(ToDoGroup group)
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ManageItemViewModel>(group);
        }

    }
}