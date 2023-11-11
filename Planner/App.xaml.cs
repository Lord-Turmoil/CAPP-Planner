using System.Windows;
using Planner.ViewModel;
using Planner.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace Planner;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
        containerRegistry.RegisterForNavigation<ProcessView, ProcessViewModel>();
        containerRegistry.RegisterForNavigation<TongView, TongViewModel>();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }
}