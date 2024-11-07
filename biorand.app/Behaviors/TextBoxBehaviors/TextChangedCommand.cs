using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace biorand.app.Behaviors.TextBoxBehaviors
{
    public partial class TextBoxBehaviors
    {
        public static readonly DependencyProperty TextChangedCommandProperty = DependencyProperty.RegisterAttached("TextChangedCommand", typeof(ICommand), typeof(TextBoxBehaviors), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged)));

        public static ICommand GetTextChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(TextChangedCommandProperty);
        }

        public static void SetTextChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(TextChangedCommandProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.TextChanged -= TextBox_TextChanged;
                if (e.NewValue is ICommand)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                }
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null)
                return;

            var command = GetTextChangedCommand(textBox);
            var param = new TextBoxTextChangedArgs(textBox, e);
            if (command != null && command.CanExecute(param))
            {
                command.Execute(param);
            }
        }

        public class TextBoxTextChangedArgs
        {
            public TextBox TextBox { get; set; }
            public TextChangedEventArgs TextChangedEventArgs { get; set; }

            public TextBoxTextChangedArgs(TextBox textBox, TextChangedEventArgs textChangedEventArgs)
            {
                TextBox = textBox;
                TextChangedEventArgs = textChangedEventArgs;
            }
        }
    }
}


