using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace biorand.desktop.Views.Common
{
    /// <summary>
    /// Interaction logic for BioSlider.xaml
    /// </summary>
    public partial class BioSlider : UserControl
    {
        public BioSlider()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SliderTitleProperty = DependencyProperty.Register("SliderTitle", typeof(string), typeof(BioSlider), new PropertyMetadata(null));
        public string SliderTitle { get { return (string)GetValue(SliderTitleProperty); } set { SetValue(SliderTitleProperty, value); } }

        public static readonly DependencyProperty LowTextProperty = DependencyProperty.Register("LowText", typeof(string), typeof(BioSlider), new PropertyMetadata(null));
        public string LowText { get { return (string)GetValue(LowTextProperty); } set { SetValue(LowTextProperty, value); } }

        public static readonly DependencyProperty HighTextProperty = DependencyProperty.Register("HighText", typeof(string), typeof(BioSlider), new PropertyMetadata(null));
        public string HighText { get { return (string)GetValue(HighTextProperty); } set { SetValue(HighTextProperty, value); } }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(BioSlider), new PropertyMetadata(null));
        public double Maximum { get { return (double)GetValue(MaximumProperty); } set { SetValue(MaximumProperty, value); } }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider slider) { double newValue = Math.Round(slider.Value / slider.SmallChange) * slider.SmallChange; if (slider.Value != newValue) { slider.Value = newValue; } }
        }
    }
}
