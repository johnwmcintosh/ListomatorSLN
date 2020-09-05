using Listomator.Core;
using Listomator.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listomator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListomatorShell : ContentPage
    {
        public ListomatorShell()
        {
            InitializeComponent();
            BindingContext = Locator.GetClass<ListomatorShellViewModel>();
        }
    }
}