using System.Windows;
using System.Windows.Input;

namespace biorand.desktop.Behaviors.KeyBehaviors;

public partial class KeyBehaviors
{
    public static readonly DependencyProperty EnterKeyUpCommandProperty = DependencyProperty.RegisterAttached("EnterKeyUpCommand", typeof(ICommand), typeof(KeyBehaviors), new FrameworkPropertyMetadata(new PropertyChangedCallback(EnterKeyUpCommandChanged)));

    private static void EnterKeyUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = (FrameworkElement)d;
        element.KeyUp += Element_KeyUp;
    }

    private static void Element_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        var element = (FrameworkElement)sender;
        var command = GetEnterKeyUpCommand(element);
        command.Execute(e);
    }

    public static void SetEnterKeyUpCommand(UIElement element, ICommand value)
    {
        element.SetValue(EnterKeyUpCommandProperty, value);
    }

    public static ICommand GetEnterKeyUpCommand(UIElement element)
    {
        return (ICommand)element.GetValue(EnterKeyUpCommandProperty);
    }
}
