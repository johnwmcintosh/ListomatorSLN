using Listomator.Core;
using Listomator.Models;
using Listomator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listomator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsList : ContentPage
    {
        public ItemsList()
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ItemsListViewModel>();
        }

        public ItemsList(ToDoGroup group)
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ItemsListViewModel>(group);
        }
    }
}