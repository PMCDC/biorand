using System.Windows;
using System.Windows.Controls.Primitives;

namespace biorand.desktop.Controls.ToggleButtons;

public class GroupHeaderToggleButton : ToggleButton
{
    public static readonly DependencyProperty ToggleTextProperty = DependencyProperty.Register(nameof(ToggleText), typeof(string), typeof(GroupHeaderToggleButton), new PropertyMetadata(string.Empty));

    public string ToggleText
    {
        get => (string)GetValue(ToggleTextProperty);
        set => SetValue(ToggleTextProperty, value);
    }
}
