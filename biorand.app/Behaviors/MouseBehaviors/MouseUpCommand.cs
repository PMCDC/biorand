using System.Windows;
using System.Windows.Input;

namespace biorand.app.Behaviors.MouseBehaviors
{
    public partial class MouseBehaviors
    {
        public static readonly DependencyProperty MouseUpCommandProperty = DependencyProperty.RegisterAttached("MouseUpCommand", typeof(ICommand), typeof(MouseBehaviors), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseUpCommandChanged)));

        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;
            element.MouseUp += new MouseButtonEventHandler(Element_MouseUp);
        }

        private static void Element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;
            var command = GetMouseUpCommand(element);
            var parameter = GetMouseUpCommandParameter(element);
            command.Execute(parameter);
        }

        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }
    }
}


