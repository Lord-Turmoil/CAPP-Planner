using System.Windows.Controls;

namespace Planner.Views;

/// <summary>
///     Interaction logic for UserControl1.xaml
/// </summary>
public partial class ProcessView : UserControl
{
    public ProcessView()
    {
        InitializeComponent();
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        ScrollViewer scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}