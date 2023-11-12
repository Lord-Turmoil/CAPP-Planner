using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using ImTools;
using Planner.Extensions;
using Planner.Library.TongSelection;
using Planner.Models;
using Planner.ViewModel.Services;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Planner.ViewModel;

class TongViewModel : BindableBase
{
    private readonly IContainerProvider _containerProvider;
    private readonly IRegionManager _regionManager;
    private readonly TongSelector _selector = new();
    private readonly TongViewService _service;

    private string _lengthDiameterRatioText = "0.0";
    private ObservableCollection<OptionItem> _numFacesItems;
    private ObservableCollection<OptionItem> _processMethods;
    private ObservableCollection<OptionItem> _processSideItems;

    private ObservableCollection<ResultItem> _resultItems;

    public TongViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        _containerProvider = containerProvider;
        _regionManager = regionManager;
        _service = new TongViewService(_regionManager);

        LoadedCommand = new DelegateCommand(OnLoaded);

        ProcessMethods = new ObservableCollection<OptionItem>();
        CreateProcessMethods();

        NumFacesItems = new ObservableCollection<OptionItem>();
        CreateNumFacesItems();

        ProcessSideItems = new ObservableCollection<OptionItem>();
        CreateProcessSideItems();
    }

    public DelegateCommand LoadedCommand { get; }

    public ObservableCollection<OptionItem> ProcessMethods {
        get => _processMethods;
        set => SetProperty(ref _processMethods, value);
    }

    public ObservableCollection<OptionItem> NumFacesItems {
        get => _numFacesItems;
        set => SetProperty(ref _numFacesItems, value);
    }

    public ObservableCollection<OptionItem> ProcessSideItems {
        get => _processSideItems;
        set => SetProperty(ref _processSideItems, value);
    }

    public ObservableCollection<ResultItem> ResultItems {
        set => SetProperty(ref _resultItems, value);
        get => _resultItems;
    }

    public int ProcessMethod {
        set {
            _selector.ProcessMethod = value;
            UpdateResult();
        }
        get => _selector.ProcessMethod;
    }

    public int NumFaces {
        set {
            _selector.NumFaces = value;
            UpdateResult();
        }
        get => _selector.NumFaces;
    }

    public string LengthDiameterRatioText {
        set {
            if (string.IsNullOrEmpty(value))
            {
                value = "0.0";
            }

            if (double.TryParse(value, out double result))
            {
                SetProperty(ref _lengthDiameterRatioText, value);
                _selector.LengthDiameterRatio = result;
                UpdateResult();
            }
        }
        get => _lengthDiameterRatioText;
    }

    public int ProcessSide {
        set {
            _selector.ProcessSide = value;
            UpdateResult();
        }
        get => _selector.ProcessSide;
    }

    public bool IsSolid {
        set {
            _selector.IsSolid = value;
            UpdateResult();
        }
        get => _selector.IsSolid;
    }

    private void UpdateResult()
    {
        _service.Navigate(_selector.GetView());

        IEnumerable<int> results = _selector.GetPossibleMethods();
        List<string> methods = results.Select(result => TongMethod.GetMethod(result).Aggregate((a, b) => a + " + " + b))
            .ToList();

        ObservableCollection<ResultItem> resultItems = new();

        if (methods.Count == 0)
        {
            resultItems.Add(new ResultItem(0, Texts.NoResult));
        }
        else
        {
            int index = 0;
            foreach (string method in methods)
            {
                resultItems.Add(new ResultItem(++index, method));
            }
        }

        ResultItems = resultItems;
    }

    private void CreateProcessMethods()
    {
        ProcessMethods.Add(new OptionItem(0, Texts.NotSelected));
        ProcessMethods.Add(new OptionItem(1, Texts.Method1));
        ProcessMethods.Add(new OptionItem(2, Texts.Method2));
        ProcessMethods.Add(new OptionItem(3, Texts.Method3));
    }

    private void CreateNumFacesItems()
    {
        NumFacesItems.Add(new OptionItem(0, Texts.NotSelected));
        NumFacesItems.Add(new OptionItem(1, Texts.SingleFace));
        NumFacesItems.Add(new OptionItem(2, Texts.MultipleFace));
    }

    private void CreateProcessSideItems()
    {
        ProcessSideItems.Add(new OptionItem(0, Texts.NotSelected));
        ProcessSideItems.Add(new OptionItem(1, Texts.InnerSide));
        ProcessSideItems.Add(new OptionItem(2, Texts.OuterSide));
    }

    private void OnLoaded()
    {
        _service.Load();
        _service.Navigate(ProcessMethod);
    }

    private void Navigate(int choice)
    {
        _service.Navigate(choice);
    }
}