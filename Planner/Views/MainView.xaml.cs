using System.Windows;
using Prism.Events;

namespace Planner.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView(IEventAggregator aggregator)
    {
        InitializeComponent();
    }
}