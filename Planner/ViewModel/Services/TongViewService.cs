using System.Collections.Generic;
using System.Windows.Controls;
using Planner.Extensions;
using Planner.Views.TongChoice;
using Prism.Regions;

namespace Planner.ViewModel.Services;

class TongViewService
{
    private readonly IRegionManager _regionManager;

    private readonly List<UserControl> _views = new()
    {
        new TongChoice0View(),
        new TongChoice1View(),
        new TongChoice2View(),
        new TongChoice3View()
    };

    private bool _isLoaded;
    private UserControl? currentView;

    public TongViewService(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void Load()
    {
        if (_isLoaded)
        {
            return;
        }

        IRegion? region = _regionManager.Regions[PrismManager.TongViewRegionName];
        if (region != null)
        {
            foreach (UserControl view in _views)
            {
                region.Add(view);
                region.Deactivate(view);
            }
        }

        _isLoaded = true;
    }

    public void Navigate(int choice)
    {
        if (choice is < 0 or > 4)
        {
            return;
        }

        UserControl newView = _views[choice];
        if (newView == currentView)
        {
            return;
        }

        IRegion? region = _regionManager.Regions[PrismManager.TongViewRegionName];
        if (region == null)
        {
            return;
        }

        if (currentView != null)
        {
            region.Deactivate(currentView);
        }
        region.Activate(newView);

        currentView = newView;
    }
}