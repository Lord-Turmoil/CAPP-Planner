using System.Windows.Controls;
using System.Windows.Input;

namespace Planner.Views;

/// <summary>
///     Interaction logic for TongView.xaml
/// </summary>
public partial class TongView : UserControl
{
    public TongView()
    {
        InitializeComponent();
    }


    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}