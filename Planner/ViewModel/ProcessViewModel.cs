using Planner.Extensions;
using Planner.Library.HoleProcess;
using Planner.Models;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Planner.ViewModel;

class ProcessViewModel : BindableBase
{
    private ObservableCollection<OptionItem> _blankTypeItems;
    private ObservableCollection<OptionItem> _ecoItems;
    private ObservableCollection<OptionItem> _materialItems;

    private readonly HolePlanner _holePlanner = new();

    private string _surfaceRoughnessText = "0.0";
    private string _holeDiameterText = "0.0";

    private ObservableCollection<ResultItem> _resultItems;

    public ProcessViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        EcoItems = new ObservableCollection<OptionItem>();
        CreateEcoPrecisionItems();

        MaterialItems = new ObservableCollection<OptionItem>();
        CreateMaterialItems();

        BlankTypeItems = new ObservableCollection<OptionItem>();
        CreateBlankTypeItems();

        UpdateResult();
    }

    public ObservableCollection<OptionItem> EcoItems {
        set {
            _ecoItems = value;
            RaisePropertyChanged();
        }
        get => _ecoItems;
    }

    public ObservableCollection<OptionItem> MaterialItems {
        set {
            _materialItems = value;
            RaisePropertyChanged();
        }
        get => _materialItems;
    }

    public ObservableCollection<OptionItem> BlankTypeItems {
        set {
            _blankTypeItems = value;
            RaisePropertyChanged();
        }
        get => _blankTypeItems;
    }

    public int SelectedEcoPrecision {
        set {
            _holePlanner.SelectedEcoPrecision = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.SelectedEcoPrecision;
    }

    public int SelectedMaterial {
        set {
            _holePlanner.SelectedMaterial = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.SelectedMaterial;
    }

    public int SelectedBlankType {
        set {
            _holePlanner.SelectedBlankType = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.SelectedBlankType;
    }

    public string SurfaceRoughnessText {
        set {
            if (string.IsNullOrEmpty(value))
            {
                value = "0.0";
            }

            if (double.TryParse(value, out double roughness))
            {
                _holePlanner.SurfaceRoughness = roughness;
                _surfaceRoughnessText = value;
                RaisePropertyChanged();
                UpdateResult();
            }
        }
        get => _surfaceRoughnessText;
    }

    public string HoleDiameterText {
        set {
            if (string.IsNullOrEmpty(value))
            {
                value = "0.0";
            }

            if (double.TryParse(value, out double diameter))
            {
                _holePlanner.HoleDiameter = diameter;
                _holeDiameterText = value;
                RaisePropertyChanged();
                UpdateResult();
            }
        }
        get => _holeDiameterText;
    }

    public bool BulkProduction {
        set {
            _holePlanner.BulkProduction = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.BulkProduction;
    }

    public bool HighQualityHole {
        set {
            _holePlanner.HighQualityHole = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.HighQualityHole;
    }

    public bool HighQualityNonFerrousMetal {
        set {
            _holePlanner.HighQualityNonFerrousMetal = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holePlanner.HighQualityNonFerrousMetal;
    }

    public ObservableCollection<ResultItem> ResultItems {
        set {
            _resultItems = value;
            RaisePropertyChanged();
        }
        get => _resultItems;
    }

    private void CreateEcoPrecisionItems()
    {
        EcoItems.Add(new OptionItem(0, Texts.NotSelected));
        for (int i = 1; i <= 12; i++)
        {
            EcoItems.Add(new OptionItem(i, "IT " + i));
        }
    }

    private void CreateMaterialItems()
    {
        MaterialItems.Add(new OptionItem(0, Texts.NotSelected));
        MaterialItems.Add(new OptionItem(1, Texts.QuenchedSteel));
        MaterialItems.Add(new OptionItem(2, Texts.NonQuenchedSteel));
        MaterialItems.Add(new OptionItem(3, Texts.NonFerrousMetal));
    }

    private void CreateBlankTypeItems()
    {
        BlankTypeItems.Add(new OptionItem(0, Texts.NotSelected));
        BlankTypeItems.Add(new OptionItem(1, Texts.Solid));
        BlankTypeItems.Add(new OptionItem(2, Texts.CastHole));
        BlankTypeItems.Add(new OptionItem(3, Texts.ForgedHole));
    }

    private void UpdateResult()
    {
        IEnumerable<int> results = _holePlanner.GetPossibleProcess();
        List<string> procedures = results.Select(Procedures.GetProcedure).ToList().Select(result => result.Aggregate((a, b) => a + " -> " + b)).ToList();

        ObservableCollection<ResultItem> resultItems = new();

        if (procedures.Count == 0)
        {
            resultItems.Add(new ResultItem(0, Texts.NoResult));
        }
        else
        {
            int index = 0;
            foreach (string procedure in procedures)
            {
                resultItems.Add(new ResultItem(++index, procedure));
            }
        }

        ResultItems = resultItems;
    }
}