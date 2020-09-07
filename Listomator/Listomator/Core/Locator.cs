using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Listomator.Data;
using Listomator.Models;
using Listomator.ViewModels;
using Listomator.Views;

namespace Listomator.Core
{
    public class Locator
    {
        public const string ListomatorShell = "ListomatorShell";
        public const string ManageGroup = "ManageGroup";
        public const string ItemsList = "ItemsList";
        public const string ManageItem = "ManageItem";
        public const string MainPage = "MainPage";

        static Locator()
        {

            // Navigation
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigation = new NavigationService();

            navigation.Configure(ListomatorShell, typeof(ListomatorShell));
            navigation.Configure(ManageGroup, typeof(ManageGroup));
            navigation.Configure(ItemsList, typeof(ItemsList));
            navigation.Configure(ManageItem, typeof(ManageItem));
            navigation.Configure(MainPage, typeof(MainPage));
            
            // D.I.

            // Models
            SimpleIoc.Default.Register<ToDoItem>();
            SimpleIoc.Default.Register<ToDoGroup>();

            // ViewModels
            SimpleIoc.Default.Register<ListomatorShellViewModel>();
            SimpleIoc.Default.Register<ManageGroupViewModel>();
            SimpleIoc.Default.Register<ItemsListViewModel>();
            SimpleIoc.Default.Register<ManageItemViewModel>();

            // Repo
            SimpleIoc.Default.Register<ListomatorRepository>();

            // Nav
            SimpleIoc.Default.Register(() => navigation);
        }

        public NavigationService NavigationService => ServiceLocator.Current.GetInstance<NavigationService>();

        public static T GetClass<T>() where T : NotifyBase
        {
            var c = ServiceLocator.Current.GetInstance<T>();
            if (c != null)
                c.Init();
            else
                c = default;
            return c;
        }

        public static T GetClass<T>(object parameter) where T : NotifyBase
        {
            var c = ServiceLocator.Current.GetInstance<T>();
            if (c != null)
                c.Init(parameter);
            else
                c = default;
            return c;
        }
    }
}
