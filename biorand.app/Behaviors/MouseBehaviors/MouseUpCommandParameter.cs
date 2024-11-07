using System.Windows;

namespace biorand.app.Behaviors.MouseBehaviors
{
    public partial class MouseBehaviors
    {
        public static readonly DependencyProperty MouseUpCommandParameterProperty = DependencyProperty.RegisterAttached("MouseUpCommandParameter", typeof(object), typeof(MouseBehaviors), new PropertyMetadata(null));

        public static void SetMouseUpCommandParameter(UIElement element, object value)
        {
            element.SetValue(MouseUpCommandParameterProperty, value);
        }

        public static object GetMouseUpCommandParameter(UIElement element)
        {
            return element.GetValue(MouseUpCommandParameterProperty);
        }
    }
}