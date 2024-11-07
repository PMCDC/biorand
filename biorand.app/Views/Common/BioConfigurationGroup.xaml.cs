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

namespace biorand.app.Views.Common
{
    /// <summary>
    /// Interaction logic for BioConfigurationGroup.xaml
    /// </summary>
    public partial class BioConfigurationGroup : UserControl
    {
        public BioConfigurationGroup()
        {
            InitializeComponent();
        }

        //content
        public static readonly DependencyProperty GroupContentProperty = DependencyProperty.Register("GroupContent", typeof(object), typeof(BioConfigurationGroup), new PropertyMetadata(null));
        public object GroupContent { get { return GetValue(GroupContentProperty); } set { SetValue(GroupContentProperty, value); } }

        //title
        public static readonly DependencyProperty GroupTitleProperty = DependencyProperty.Register("GroupTitle", typeof(string), typeof(BioConfigurationGroup), new PropertyMetadata(null));
        public string GroupTitle { get { return (string)GetValue(GroupTitleProperty); } set { SetValue(GroupTitleProperty, value); } }

        //is enabled
        public static readonly DependencyProperty IsGroupEnabledProperty = DependencyProperty.Register("IsGroupEnabled", typeof(bool), typeof(BioConfigurationGroup), new PropertyMetadata(null));
        public bool IsGroupEnabled { get { return (bool)GetValue(IsGroupEnabledProperty); } set { SetValue(IsGroupEnabledProperty, value); } }

        //is togglelable
        public static readonly DependencyProperty IsGroupTogglelableProperty = DependencyProperty.Register("IsGroupTogglelable", typeof(bool), typeof(BioConfigurationGroup), new PropertyMetadata(null));
        public bool IsGroupTogglelable { get { return (bool)GetValue(IsGroupTogglelableProperty); } set { SetValue(IsGroupTogglelableProperty, value); } }

        public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register("ContentBackground", typeof(SolidColorBrush), typeof(BioConfigurationGroup), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(29, 29, 29))));
        public SolidColorBrush ContentBackground { get { return (SolidColorBrush)GetValue(ContentBackgroundProperty); } set { SetValue(ContentBackgroundProperty, value); } }
    }
}
