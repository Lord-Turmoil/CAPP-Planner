using System.Collections.ObjectModel;
using Planner.Extensions;
using Planner.Models;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Planner.ViewModel;

class TongViewModel : BindableBase
{
    private ObservableCollection<BlankTypeItem> _blankTypeItems;
    private ObservableCollection<EcoPrecisionItem> _ecoItems;

    private ObservableCollection<MaterialItem> _materialItems;

    public TongViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        EcoItems = new ObservableCollection<EcoPrecisionItem>();
        CreateEcoPrecisionItems();

        MaterialItems = new ObservableCollection<MaterialItem>();
        CreateMaterialItems();

        BlankTypeItems = new ObservableCollection<BlankTypeItem>();
        CreateBlankTypeItems();
    }

    public ObservableCollection<EcoPrecisionItem> EcoItems {
        set {
            _ecoItems = value;
            RaisePropertyChanged();
        }
        get => _ecoItems;
    }

    public ObservableCollection<MaterialItem> MaterialItems {
        set {
            _materialItems = value;
            RaisePropertyChanged();
        }
        get => _materialItems;
    }

    public ObservableCollection<BlankTypeItem> BlankTypeItems {
        set {
            _blankTypeItems = value;
            RaisePropertyChanged();
        }
        get => _blankTypeItems;
    }


    private void CreateEcoPrecisionItems()
    {
        for (int i = 1; i <= 12; i++)
        {
            EcoItems.Add(new EcoPrecisionItem(i));
        }
    }

    private void CreateMaterialItems()
    {
        MaterialItems.Add(new MaterialItem(0, Texts.QuenchedSteel));
        MaterialItems.Add(new MaterialItem(1, Texts.NonQuenchedSteel));
        MaterialItems.Add(new MaterialItem(2, Texts.NonFerrousMetal));
    }

    private void CreateBlankTypeItems()
    {
        BlankTypeItems.Add(new BlankTypeItem(0, Texts.Solid));
        BlankTypeItems.Add(new BlankTypeItem(1, Texts.CastHole));
        BlankTypeItems.Add(new BlankTypeItem(2, Texts.ForgedHole));
    }
}