using System.Collections.ObjectModel;
using Planner.Extensions;
using Planner.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Planner.ViewModel;

internal class MainViewModel : BindableBase
{
    private readonly IContainerProvider _containerProvider;
    private readonly IRegionManager _regionManager;
    private ObservableCollection<MenuTab> _menuTabs;
    private IRegionNavigationJournal? _navigationJournal;

    public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        _containerProvider = containerProvider;
        _regionManager = regionManager;

        CreateMenuTabs();

        NavigateCommand = new DelegateCommand<MenuTab>(Navigate);

        NavigateCommand.Execute(MenuTabs[0]);
    }

    public DelegateCommand<MenuTab> NavigateCommand { get; private set; }
    public DelegateCommand LoadedCommand => new DelegateCommand(() => { NavigateCommand.Execute(MenuTabs[0]); });

    public ObservableCollection<MenuTab> MenuTabs {
        set {
            _menuTabs = value;
            RaisePropertyChanged();
        }
        get => _menuTabs;
    }

    private void CreateMenuTabs()
    {
        MenuTabs = new ObservableCollection<MenuTab>
        {
            new("主页", "IndexView", "21371300 柳政尧《计算机辅助设计与制造》第 3 次作业"),
            new("孔加工方法链选择", "ProcessView", "孔加工方法链选择"),
            new("装夹方案选择决策", "TongView", "装夹方案选择决策")
        };
    }

    private void Navigate(MenuTab tab)
    {
        if (string.IsNullOrWhiteSpace(tab.NameSpace))
        {
            return;
        }

        if (_regionManager.Regions.ContainsRegionWithName(PrismManager.MainViewRegionName))
        {
            _regionManager.Regions[PrismManager.MainViewRegionName]
                .RequestNavigate(tab.NameSpace, back => {
                    _navigationJournal = back.Context.NavigationService.Journal;
                });
        }
    }
}