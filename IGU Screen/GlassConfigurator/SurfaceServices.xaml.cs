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
using System.Windows.Shapes;

namespace SPIL.IGUConfigurator
{
    /// <summary>
    /// Interaction logic for SurfaceServices.xaml
    /// </summary>
    public partial class SurfaceServices : Window
    {
        public bool IsOption1Checked { get; set; }
        public bool IsOption2Checked { get; set; }
        public bool IsOption3Checked { get; set; }
        public bool IsOption4Checked { get; set; }
        public bool IsOption5Checked { get; set; }
        public bool IsOption6Checked { get; set; }
        public bool IsOption7Checked { get; set; }
        public bool IsOption8Checked { get; set; }

        public string SelectedService1 => cmbSurface1Service.SelectedItem?.ToString();
        public string SelectedService2 => cmbSurface2Service.SelectedItem?.ToString();
        public string SelectedService3 => cmbSurface3Service.SelectedItem?.ToString();
        public string SelectedService4 => cmbSurface4Service.SelectedItem?.ToString();
        public string SelectedService5 => cmbSurface5Service.SelectedItem?.ToString();
        public string SelectedService6 => cmbSurface6Service.SelectedItem?.ToString();
        public string SelectedService7 => cmbSurface7Service.SelectedItem?.ToString();
        public string SelectedService8 => cmbSurface8Service.SelectedItem?.ToString();



        public SurfaceServices(bool isOption1Checked, bool isOption2Checked, bool isOption3Checked, bool isOption4Checked, bool isOption5Checked, bool isOption6Checked, bool isOption7Checked, bool isOption8Checked)
        {
            InitializeComponent();

            this.Left = 300; 
            this.Top = 200; 

            IsOption1Checked = isOption1Checked;
            IsOption2Checked = isOption2Checked;
            IsOption3Checked = isOption3Checked;
            IsOption4Checked = isOption4Checked;
            IsOption5Checked = isOption5Checked;
            IsOption6Checked = isOption6Checked;
            IsOption7Checked = isOption7Checked;
            IsOption8Checked = isOption8Checked;

            Loaded += Window_Loaded;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid2_1.IsEnabled = IsOption1Checked;
            grid2_2.IsEnabled = IsOption1Checked;
            grid3_1.IsEnabled = IsOption2Checked;
            grid3_2.IsEnabled = IsOption2Checked;
            grid4_1.IsEnabled = IsOption3Checked;
            grid4_2.IsEnabled = IsOption3Checked;
            grid5_1.IsEnabled = IsOption4Checked;
            grid5_2.IsEnabled = IsOption4Checked;
            grid6_1.IsEnabled = IsOption5Checked;
            grid6_2.IsEnabled = IsOption5Checked;
            grid7_1.IsEnabled = IsOption6Checked;
            grid7_2.IsEnabled = IsOption6Checked;
            grid8_1.IsEnabled = IsOption7Checked;
            grid8_2.IsEnabled = IsOption7Checked;
            grid9_1.IsEnabled = IsOption8Checked;
            grid9_2.IsEnabled = IsOption8Checked;

            var comboboxValues = new List<string> { "None", "Coating", "Patterned", "Mirror", "Sand Blast" };
            cmbSurface1Service.ItemsSource = comboboxValues;
            cmbSurface2Service.ItemsSource = comboboxValues;
            cmbSurface3Service.ItemsSource = comboboxValues;
            cmbSurface4Service.ItemsSource = comboboxValues;
            cmbSurface5Service.ItemsSource = comboboxValues;
            cmbSurface6Service.ItemsSource = comboboxValues;
            cmbSurface7Service.ItemsSource = comboboxValues;
            cmbSurface8Service.ItemsSource = comboboxValues;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
