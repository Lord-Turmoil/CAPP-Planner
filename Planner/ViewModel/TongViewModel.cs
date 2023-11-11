using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ImTools;
using Planner.Extensions;
using Planner.Models;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Planner.Library.HoleProcess;
namespace Planner.ViewModel;

class TongViewModel : BindableBase
{
    private ObservableCollection<OptionItem> _blankTypeItems;
    private ObservableCollection<OptionItem> _ecoItems;
    private ObservableCollection<OptionItem> _materialItems;

    private readonly HoleProcessSelector _holeProcessSelector = new();

    private string _surfaceRoughnessText = "0.0";
    private string _holeDiameterText = "0.0";

    private ObservableCollection<ResultItem> _resultItems;

    public TongViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
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
            _holeProcessSelector.SelectedEcoPrecision = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.SelectedEcoPrecision;
    }

    public int SelectedMaterial {
        set {
            _holeProcessSelector.SelectedMaterial = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.SelectedMaterial;
    }

    public int SelectedBlankType {
        set {
            _holeProcessSelector.SelectedBlankType = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.SelectedBlankType;
    }

    public string SurfaceRoughnessText {
        set {
            if (string.IsNullOrEmpty(value))
            {
                value = "0.0";
            }

            if (double.TryParse(value, out double roughness))
            {
                _holeProcessSelector.SurfaceRoughness = roughness;
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
                _holeProcessSelector.HoleDiameter = diameter;
                _holeDiameterText = value;
                RaisePropertyChanged();
                UpdateResult();
            }
        }
        get => _holeDiameterText;
    }

    public bool BulkProduction {
        set {
            _holeProcessSelector.BulkProduction = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.BulkProduction;
    }

    public bool HighQualityHole {
        set {
            _holeProcessSelector.HighQualityHole = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.HighQualityHole;
    }

    public bool HighQualityNonFerrousMetal {
        set {
            _holeProcessSelector.HighQualityNonFerrousMetal = value;
            RaisePropertyChanged();
            UpdateResult();
        }
        get => _holeProcessSelector.HighQualityNonFerrousMetal;
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
        IEnumerable<int> results = _holeProcessSelector.GetPossibleProcess();
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