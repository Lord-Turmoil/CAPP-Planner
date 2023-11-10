using System.Collections.ObjectModel;
using Planner.Models;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Planner.ViewModel;

class TongViewModel : BindableBase
{
    private ObservableCollection<EcoPrecisionItem> _ecoItems;

    public ObservableCollection<EcoPrecisionItem> EcoItems {
        set {
            _ecoItems = value;
            RaisePropertyChanged();
        }
        get => _ecoItems;
    }

    public TongViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        CreateEcoPrecisionItems();
    }


    private void CreateEcoPrecisionItems()
    {
        EcoItems = new ObservableCollection<EcoPrecisionItem>();
        for (int i = 1; i < 12; i++)
        {
            EcoItems.Add(new EcoPrecisionItem(i));
        }
    }
}
