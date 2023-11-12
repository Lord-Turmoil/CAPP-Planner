using System;
using System.Windows.Controls;
using System.Windows.Input;

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

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        var scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}