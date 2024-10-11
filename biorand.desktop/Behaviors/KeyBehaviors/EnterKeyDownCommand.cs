using System.Windows;
using System.Windows.Input;

namespace biorand.desktop.Behaviors.KeyBehaviors;

public partial class KeyBehaviors
{
    public static readonly DependencyProperty EnterKeyDownCommandProperty = DependencyProperty.RegisterAttached("EnterKeyDownCommand", typeof(ICommand), typeof(KeyBehaviors), new FrameworkPropertyMetadata(new PropertyChangedCallback(EnterKeyDownCommandChanged)));

    private static void EnterKeyDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var element = (FrameworkElement)d;
        element.KeyDown += Element_KeyDown;
    }

    private static void Element_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        var element = (FrameworkElement)sender;
        var command = GetEnterKeyDownCommand(element);
        command.Execute(e);
    }

    public static void SetEnterKeyDownCommand(UIElement element, ICommand value)
    {
        element.SetValue(EnterKeyDownCommandProperty, value);
    }

    public static ICommand GetEnterKeyDownCommand(UIElement element)
    {
        return (ICommand)element.GetValue(EnterKeyDownCommandProperty);
    }
}
