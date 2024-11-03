using System.Windows;
using System.Windows.Controls.Primitives;

namespace biorand.desktop.Controls.ToggleButtons;

public enum CustomToggleButtonIconStyle
{
    Circle,
    Square
}

public class CustomToggleButton : ToggleButton
{
    public static readonly DependencyProperty ToggleTextProperty = DependencyProperty.Register(nameof(ToggleText), typeof(string), typeof(CustomToggleButton), new PropertyMetadata(string.Empty));
    public string ToggleText { get => (string)GetValue(ToggleTextProperty); set => SetValue(ToggleTextProperty, value); }

    public static readonly DependencyProperty IconStyleProperty = DependencyProperty.Register(nameof(IconStyle), typeof(CustomToggleButtonIconStyle), typeof(CustomToggleButton), new PropertyMetadata(CustomToggleButtonIconStyle.Circle));
    public CustomToggleButtonIconStyle IconStyle { get => (CustomToggleButtonIconStyle)GetValue(IconStyleProperty); set => SetValue(IconStyleProperty, value); }
}
