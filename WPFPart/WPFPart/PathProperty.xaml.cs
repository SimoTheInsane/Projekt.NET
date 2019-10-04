using Microsoft.Win32;
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

namespace WPFPart
{
    /// <summary>
    /// Interaction logic for PathProperty.xaml
    /// </summary>
    public partial class PathSelector : UserControl
    {
        public PathSelector()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.png;*.jpeg;*.bmp;*.tiff;*.jpg)|*.png;*.jpeg;*.bmp;*.tiff;*.jpg";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                SelectedPath = fileDialog.FileName;
            }
        }

        #region Dependency Properties

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
            "SelectedPath",
            typeof(string),
            typeof(PathSelector),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(SelectedPathChanged))
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        private static void SelectedPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PathSelector)d).PathTextBox.Text = e.NewValue.ToString();
        }

        #endregion             
    }
}
