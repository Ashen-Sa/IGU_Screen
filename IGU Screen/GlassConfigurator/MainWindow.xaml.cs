using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using GlassConfigurator;

namespace SPIL.IGUConfigurator
{
    public partial class MainWindow : Window
    {
        private enum GlassPart
        {
            None,
            DoubleGlazeIGU,
            TripleGlazeIGU,
            Coating,
            Patterned
        }

        private readonly BitmapImage sunnySide;
        private readonly BitmapImage doubleGlazeIGUImage;
        private readonly BitmapImage tripleGlazeIGUImage;
        private readonly BitmapImage coatingImage;
        private readonly BitmapImage patternedImage;
        private readonly BitmapImage patternedRightImage;
        private readonly BitmapImage patternedLeftImage;
        private readonly BitmapImage innerCoatingArrow;
        private readonly BitmapImage outerCoatingArrow;
        private readonly BitmapImage mirrorImage;
        private readonly BitmapImage sandBlastImage;
        private readonly BitmapImage doubleSidedArrow;


        // Surface Service Images
        #region Surface Service Images
        private BitmapImage _surface1ServiceImg;
        private BitmapImage Surface1ServiceImg
        {
            get => _surface1ServiceImg;
            set
            {
                if (_surface1ServiceImg != value)
                {
                    _surface1ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface2ServiceImg;
        private BitmapImage Surface2ServiceImg
        {
            get => _surface2ServiceImg;
            set
            {
                if (_surface2ServiceImg != value)
                {
                    _surface2ServiceImg = value;
                    UpdatePreview();
                }
            }
        }
        
        private BitmapImage _surface3ServiceImg;
        private BitmapImage Surface3ServiceImg
        {
            get => _surface3ServiceImg;
            set
            {
                if (_surface3ServiceImg != value)
                {
                    _surface3ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface4ServiceImg;
        private BitmapImage Surface4ServiceImg
        {
            get => _surface4ServiceImg;
            set
            {
                if (_surface4ServiceImg != value)
                {
                    _surface4ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface5ServiceImg;
        private BitmapImage Surface5ServiceImg
        {
            get => _surface5ServiceImg;
            set
            {
                if (_surface5ServiceImg != value)
                {
                    _surface5ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface6ServiceImg;
        private BitmapImage Surface6ServiceImg
        {
            get => _surface6ServiceImg;
            set
            {
                if (_surface6ServiceImg != value)
                {
                    _surface6ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface7ServiceImg;
        private BitmapImage Surface7ServiceImg
        {
            get => _surface7ServiceImg;
            set
            {
                if (_surface7ServiceImg != value)
                {
                    _surface7ServiceImg = value;
                    UpdatePreview();
                }
            }
        }

        private BitmapImage _surface8ServiceImg;
        private BitmapImage Surface8ServiceImg
        {
            get => _surface8ServiceImg;
            set
            {
                if (_surface8ServiceImg != value)
                {
                    _surface8ServiceImg = value;
                    UpdatePreview();
                }
            }
        }
        #endregion


        // Surface Services Strings
        #region Surface Services Strings
        private String _surface1Service;
        private string surface1Service
        {
            get => _surface1Service;
            set
            {
                _surface1Service = value;
                UpdatePreview();
            }
        }

        private String _surface2Service;
        private string surface2Service
        {
            get => _surface2Service;
            set
            {
                _surface2Service = value;
                UpdatePreview();
            }
        }

        private String _surface3Service;
        private string surface3Service
        {
            get => _surface3Service;
            set
            {
                _surface3Service = value;
                UpdatePreview();
            }
        }

        private String _surface4Service;
        private string surface4Service
        {
            get => _surface4Service;
            set
            {
                _surface4Service = value;
                UpdatePreview();
            }
        }

        private String _surface5Service;
        private string surface5Service
        {
            get => _surface5Service;
            set
            {
                _surface5Service = value;
                UpdatePreview();
            }
        }

        private String _surface6Service;
        private string surface6Service
        {
            get => _surface6Service;
            set
            {
                _surface6Service = value;
                UpdatePreview();
            }
        }

        private String _surface7Service;
        private string surface7Service
        {
            get => _surface7Service;
            set
            {
                _surface7Service = value;
                UpdatePreview();
            }
        }

        private String _surface8Service;
        private string surface8Service
        {
            get => _surface8Service;
            set
            {
                _surface8Service = value;
                UpdatePreview();
            }
        }
        #endregion




        //private String configOption = "DoubleGlaze";
        private String configOption = "TripleGlaze";
        //private String configOption = "QuadrupleGlaze";

        public MainWindow()
        {
            InitializeComponent();
            this.Focus();

            sunnySide = new BitmapImage(new Uri("pack://application:,,,/Images/SunnySide.png"));
            doubleGlazeIGUImage = new BitmapImage(new Uri("pack://application:,,,/Images/DoubleGlazeIGU.png"));
            tripleGlazeIGUImage = new BitmapImage(new Uri("pack://application:,,,/Images/TripleGlazeIGU.png"));

            coatingImage = new BitmapImage(new Uri("pack://application:,,,/Images/Coating.png"));
            patternedImage = new BitmapImage(new Uri("pack://application:,,,/Images/Patterned.png"));
            patternedRightImage = new BitmapImage(new Uri("pack://application:,,,/Images/PatternedRight.png"));
            patternedLeftImage = new BitmapImage(new Uri("pack://application:,,,/Images/PatternedLeft.png"));
            mirrorImage = new BitmapImage(new Uri("pack://application:,,,/Images/Mirror.png"));
            sandBlastImage = new BitmapImage(new Uri("pack://application:,,,/Images/SandBlast.png"));

            innerCoatingArrow = new BitmapImage(new Uri("pack://application:,,,/Images/Inner.png"));
            outerCoatingArrow = new BitmapImage(new Uri("pack://application:,,,/Images/Outer.png"));

            doubleSidedArrow = new BitmapImage(new Uri("pack://application:,,,/Images/DoubleSidedArrow.png"));

            imgSunnySide.Source = sunnySide;

            InitializeComboBoxes();
            LoadDynamicContent();
            UpdatePreview();
        }


        // Load Dynamic Visualization UI based on IGU Type
        private void LoadDynamicContent()
        {
            if (configOption == "DoubleGlaze")
            {
                DynamicVisualizationContent.Content = new DoubleGlazeVisualComp();
                disableOption2Controls();
                gridOption3.IsEnabled = false;
                gridOption4.IsEnabled = false;
                txtBlockInnerOrOuter2.Text = "Outer Glass";
            }
            else if (configOption == "TripleGlaze")
            {
                DynamicVisualizationContent.Content = new TripleGlazeVisualComp();
                disableOption3Controls();
                gridOption4.IsEnabled = false;
                txtBlockInnerOrOuter3.Text = "Outer Glass";

            }
            else if (configOption == "QuadrupleGlaze")
            {
                DynamicVisualizationContent.Content = new QuadrupleGlazeVisualComp();
                txtBlockInnerOrOuter4.Text = "Outer Glass";

            }
        }



        // Initialize ComboBoxes with Options
        private void InitializeComboBoxes()
        {
            PopulateTypeComboBoxes();

            PopulatePanelComboBoxes(txtThickness1, cmbCode1, cmbDescription1);
            PopulatePanelComboBoxes(txtThickness2, cmbCode2, cmbDescription2);
            PopulatePanelComboBoxes(txtThickness3, cmbCode3, cmbDescription3);
            PopulatePanelComboBoxes(txtThickness4, cmbCode4, cmbDescription4);

            PopulateSpacerComboBoxes(txtSpace1, cmbSpacerCode1, cmbSpacerType1);
            PopulateSpacerComboBoxes(txtSpace2, cmbSpacerCode2, cmbSpacerType2);
            PopulateSpacerComboBoxes(txtSpace3, cmbSpacerCode3, cmbSpacerType3);

            PopulateGasComboBoxes();
            PopulateSealantComboBoxes();

            PopulateEdgeServiceComboBoxes();

            PopulateOverallServiceComboBox();
        }




        // Combobox Population Methods
        #region combobox population methods

        // Populate Type ComboBoxes
        #region Populate Type ComboBoxes
        private void PopulateTypeComboBoxes()
        {
            try
            {
                var types = App.DatabaseService.GetTypes();

                if (!types.Contains("None"))
                {
                    cmbType1.Items.Add("None");
                }

                // Add the types from the database
                foreach (var t in types)
                {
                    cmbType1.Items.Add(t);
                }

                // Set initial index
                if (cmbType1.Items.Count > 0)
                {
                    cmbType1.SelectedIndex = 0;
                }


                if (cmbType2 != null)
                {
                    cmbType2.Items.Clear();
                    foreach (var item in cmbType1.Items)
                    {
                        cmbType2.Items.Add(item);
                    }
                    if (cmbType2.Items.Count > 0)
                    {
                        cmbType2.SelectedIndex = 0;
                    }
                }

                if (cmbType3 != null)
                {
                    cmbType3.Items.Clear();
                    foreach (var item in cmbType1.Items)
                    {
                        cmbType3.Items.Add(item);
                    }
                    if (cmbType3.Items.Count > 0)
                    {
                        cmbType3.SelectedIndex = 0;
                    }
                }

                if (cmbType4 != null)
                {
                    cmbType4.Items.Clear();
                    foreach (var item in cmbType1.Items)
                    {
                        cmbType4.Items.Add(item);
                    }
                    if (cmbType4.Items.Count > 0)
                    {
                        cmbType4.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading types: {ex.Message} to populate comboboxes", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion




        // Populate Panel ComboBoxes
        #region Populate Panel ComboBoxes
        private void PopulatePanelComboBoxes(TextBox thicknessTextBox, ComboBox codeComboBox, ComboBox typeComboBox)
        {
            try
            {
                if (int.TryParse(thicknessTextBox.Text, out var thickness))
                {
                    var panels = App.DatabaseService.GetPanelData(thickness);

                    if (!panels.Any(s => s.Code == "None"))
                    {                        
                        codeComboBox.Items.Add("None");
                        typeComboBox.Items.Add("None");
                    }

                    if (panels.Count <= 0)
                    {
                        thicknessTextBox.BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        thicknessTextBox.ClearValue(BorderBrushProperty);
                    }

                    // Add the spacer codes from the database  
                    foreach (var panel in panels)
                    {
                        codeComboBox.Items.Add(panel.Code);
                        typeComboBox.Items.Add(panel.Description);
                    }

                    // Set initial index  
                    if (codeComboBox.Items.Count > 0)
                    {
                        codeComboBox.SelectedIndex = 0;
                        typeComboBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    thicknessTextBox.BorderBrush = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading panel details: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtThickness1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtThickness1.Text, out var thickness) && thickness>0)
            {
                cmbCode1.Items.Clear();
                cmbDescription1.Items.Clear();
                PopulatePanelComboBoxes(txtThickness1, cmbCode1, cmbDescription1);
            }
            else
            {
                cmbCode1.Items.Clear();
                cmbDescription1.Items.Clear();
                cmbCode1.Items.Add("None");
                cmbCode1.SelectedIndex = 0;
                cmbDescription1.Items.Add("None");
                cmbDescription1.SelectedIndex = 0;
                txtThickness1.BorderBrush = Brushes.Red;
            }
        }

        private void txtThickness2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtThickness2.Text, out var thickness) && thickness > 0)
            {
                cmbCode2.Items.Clear();
                cmbDescription2.Items.Clear();
                PopulatePanelComboBoxes(txtThickness2, cmbCode2, cmbDescription2);
            }
            else
            {
                cmbCode2.Items.Clear();
                cmbDescription2.Items.Clear();
                cmbCode2.Items.Add("None");
                cmbCode2.SelectedIndex = 0;
                cmbDescription2.Items.Add("None");
                cmbDescription2.SelectedIndex = 0;
                txtThickness2.BorderBrush = Brushes.Red;
            }
        }

        private void txtThickness3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtThickness3.Text, out var thickness) && thickness > 0)
            {
                cmbCode3.Items.Clear();
                cmbDescription3.Items.Clear();
                PopulatePanelComboBoxes(txtThickness3, cmbCode3, cmbDescription3);
            }
            else
            {
                cmbCode3.Items.Clear();
                cmbDescription3.Items.Clear();
                cmbCode3.Items.Add("None");
                cmbCode3.SelectedIndex = 0;
                cmbDescription3.Items.Add("None");
                cmbDescription3.SelectedIndex = 0;
                txtThickness3.BorderBrush = Brushes.Red;
            }
        }

        private void txtThickness4_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtThickness4.Text, out var thickness) && thickness > 0)
            {
                cmbCode4.Items.Clear();
                cmbDescription4.Items.Clear();
                PopulatePanelComboBoxes(txtThickness4, cmbCode4, cmbDescription4);
            }
            else
            {
                cmbCode4.Items.Clear();
                cmbDescription4.Items.Clear();
                cmbCode4.Items.Add("None");
                cmbCode4.SelectedIndex = 0;
                cmbDescription4.Items.Add("None");
                cmbDescription4.SelectedIndex = 0;
                txtThickness4.BorderBrush = Brushes.Red;
            }
        }
        #endregion


        // Populate Spacer ComboBoxes
        #region Populate Spacer ComboBoxes
        private void PopulateSpacerComboBoxes(TextBox spacerThicknessTextBox, ComboBox codeComboBox, ComboBox typeComboBox)
        {
            try
            {
                if (int.TryParse(spacerThicknessTextBox.Text, out var spacerThickness))
                {
                    var spacers = App.DatabaseService.GetSpacer(spacerThickness);

                    if (!spacers.Any(s => s.Code == "None"))
                    {
                        codeComboBox.Items.Add("None");
                        typeComboBox.Items.Add("None");
                    }

                    if (spacers.Count <= 0)
                    {
                        spacerThicknessTextBox.BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        spacerThicknessTextBox.ClearValue(BorderBrushProperty);
                    }

                    // Add the spacer codes from the database
                    foreach (var spacer in spacers)
                    {
                        codeComboBox.Items.Add(spacer.Code);
                        typeComboBox.Items.Add(spacer.Description);
                    }

                    // Set initial index
                    if (codeComboBox.Items.Count > 0)
                    {
                        codeComboBox.SelectedIndex = 0;
                        typeComboBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    spacerThicknessTextBox.BorderBrush = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading spacers: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSpace1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtSpace1.Text, out var spacerThickness))
            {
                cmbSpacerCode1.Items.Clear();
                cmbSpacerType1.Items.Clear();
                PopulateSpacerComboBoxes(txtSpace1, cmbSpacerCode1, cmbSpacerType1);
            }
            else
            {
                cmbSpacerCode1.Items.Clear();
                cmbSpacerType1.Items.Clear();
                cmbSpacerCode1.Items.Add("None");
                cmbSpacerCode1.SelectedIndex = 0;
                cmbSpacerType1.Items.Add("None");
                cmbSpacerType1.SelectedIndex = 0;
                txtSpace1.BorderBrush = Brushes.Red;
                            }
        }

        private void txtSpace2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtSpace2.Text, out var spacerThickness))
            {
                cmbSpacerCode2.Items.Clear();
                cmbSpacerType2.Items.Clear();
                PopulateSpacerComboBoxes(txtSpace2, cmbSpacerCode2, cmbSpacerType2);
            }
            else
            {
                cmbSpacerCode2.Items.Clear();
                cmbSpacerType2.Items.Clear();
                cmbSpacerCode2.Items.Add("None");
                cmbSpacerCode2.SelectedIndex = 0;
                cmbSpacerType2.Items.Add("None");
                cmbSpacerType2.SelectedIndex = 0;
                txtSpace2.BorderBrush = Brushes.Red;
            }
        }

        private void txtSpace3_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateOverallThickness();
            if (int.TryParse(txtSpace3.Text, out var spacerThickness))
            {
                cmbSpacerCode3.Items.Clear();
                cmbSpacerType3.Items.Clear();
                PopulateSpacerComboBoxes(txtSpace3, cmbSpacerCode3, cmbSpacerType3);
            }
            else
            {
                cmbSpacerCode3.Items.Clear();
                cmbSpacerType3.Items.Clear();
                cmbSpacerCode3.Items.Add("None");
                cmbSpacerCode3.SelectedIndex = 0;
                cmbSpacerType3.Items.Add("None");
                cmbSpacerType3.SelectedIndex = 0;
                txtSpace3.BorderBrush = Brushes.Red;
            }
        }
        #endregion




        // Populate Gas ComboBoxes
        #region Populate Gas ComboBoxes
        private void PopulateGasComboBoxes()
        {
            try
            {
                var gas = App.DatabaseService.GetGas();

                if (!gas.Contains("None"))
                {
                    cmbFillGas1.Items.Add("None");
                }

                // Add the spacer codes from the database
                foreach (var g in gas)
                {
                    cmbFillGas1.Items.Add(g);
                }

                // Set initial index
                if (cmbFillGas1.Items.Count > 0)
                {
                    cmbFillGas1.SelectedIndex = 0;
                }


                if (cmbFillGas2 != null)
                {
                    cmbFillGas2.Items.Clear();
                    foreach (var item in cmbFillGas1.Items)
                    {
                        cmbFillGas2.Items.Add(item);
                    }
                    if (cmbFillGas2.Items.Count > 0)
                    {
                        cmbFillGas2.SelectedIndex = 0;
                    }
                }

                if (cmbFillGas3 != null)
                {
                    cmbFillGas3.Items.Clear();
                    foreach (var item in cmbFillGas1.Items)
                    {
                        cmbFillGas3.Items.Add(item);
                    }
                    if (cmbFillGas3.Items.Count > 0)
                    {
                        cmbFillGas3.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading gas: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion




        // Populate Gas ComboBoxes
        #region Populate Sealant ComboBoxes
        private void PopulateSealantComboBoxes()
        {
            try
            {
                var gas = App.DatabaseService.GetSealant();

                if (!gas.Contains("None"))
                {
                    cmbFillSealant1.Items.Add("None");
                }

                // Add the spacer codes from the database
                foreach (var g in gas)
                {
                    cmbFillSealant1.Items.Add(g);
                }

                // Set initial index
                if (cmbFillSealant1.Items.Count > 0)
                {
                    cmbFillSealant1.SelectedIndex = 0;
                }


                if (cmbFillSealant2 != null)
                {
                    cmbFillSealant2.Items.Clear();
                    foreach (var item in cmbFillSealant1.Items)
                    {
                        cmbFillSealant2.Items.Add(item);
                    }
                    if (cmbFillSealant2.Items.Count > 0)
                    {
                        cmbFillSealant2.SelectedIndex = 0;
                    }
                }

                if (cmbFillSealant3 != null)
                {
                    cmbFillSealant3.Items.Clear();
                    foreach (var item in cmbFillSealant1.Items)
                    {
                        cmbFillSealant3.Items.Add(item);
                    }
                    if (cmbFillSealant3.Items.Count > 0)
                    {
                        cmbFillSealant3.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sealant: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion




        // Populate Edge Service ComboBoxes
        #region Populate Edge Service ComboBoxes
        private void PopulateEdgeServiceComboBoxes()
        {
            try
            {
                var services = App.DatabaseService.GetEdgeServices();

                if (!services.Contains("None"))
                {
                    cmbTopEdgeService1.Items.Add("None");
                    cmbTopEdgeService2.Items.Add("None");
                    cmbTopEdgeService3.Items.Add("None");
                    cmbTopEdgeService4.Items.Add("None");
                    cmbRightEdgeService1.Items.Add("None");
                    cmbRightEdgeService2.Items.Add("None");
                    cmbRightEdgeService3.Items.Add("None");
                    cmbRightEdgeService4.Items.Add("None");
                    cmbBottomEdgeService1.Items.Add("None");
                    cmbBottomEdgeService2.Items.Add("None");
                    cmbBottomEdgeService3.Items.Add("None");
                    cmbBottomEdgeService4.Items.Add("None");
                    cmbLeftEdgeService1.Items.Add("None");
                    cmbLeftEdgeService2.Items.Add("None");
                    cmbLeftEdgeService3.Items.Add("None");
                    cmbLeftEdgeService4.Items.Add("None");
                    cmbAllEdgeService1.Items.Add("None");
                    cmbAllEdgeService2.Items.Add("None");
                    cmbAllEdgeService3.Items.Add("None");
                    cmbAllEdgeService4.Items.Add("None");
                }

                // Add the spacer codes from the database
                foreach (var es in services)
                {
                    cmbTopEdgeService1.Items.Add(es);
                    cmbTopEdgeService2.Items.Add(es);
                    cmbTopEdgeService3.Items.Add(es);
                    cmbTopEdgeService4.Items.Add(es);
                    cmbRightEdgeService1.Items.Add(es);
                    cmbRightEdgeService2.Items.Add(es);
                    cmbRightEdgeService3.Items.Add(es);
                    cmbRightEdgeService4.Items.Add(es);
                    cmbBottomEdgeService1.Items.Add(es);
                    cmbBottomEdgeService2.Items.Add(es);
                    cmbBottomEdgeService3.Items.Add(es);
                    cmbBottomEdgeService4.Items.Add(es);
                    cmbLeftEdgeService1.Items.Add(es);
                    cmbLeftEdgeService2.Items.Add(es);
                    cmbLeftEdgeService3.Items.Add(es);
                    cmbLeftEdgeService4.Items.Add(es);
                    cmbAllEdgeService1.Items.Add(es);
                    cmbAllEdgeService2.Items.Add(es);
                    cmbAllEdgeService3.Items.Add(es);
                    cmbAllEdgeService4.Items.Add(es);
                }

                // Set initial index
                if (cmbTopEdgeService1.Items.Count > 0)
                {
                    cmbTopEdgeService1.SelectedIndex = 0;
                    cmbTopEdgeService2.SelectedIndex = 0;
                    cmbTopEdgeService3.SelectedIndex = 0;
                    cmbTopEdgeService4.SelectedIndex = 0;
                    cmbRightEdgeService1.SelectedIndex = 0;
                    cmbRightEdgeService2.SelectedIndex = 0;
                    cmbRightEdgeService3.SelectedIndex = 0;
                    cmbRightEdgeService4.SelectedIndex = 0;
                    cmbBottomEdgeService1.SelectedIndex = 0;
                    cmbBottomEdgeService2.SelectedIndex = 0;
                    cmbBottomEdgeService3.SelectedIndex = 0;
                    cmbBottomEdgeService4.SelectedIndex = 0;
                    cmbLeftEdgeService1.SelectedIndex = 0;
                    cmbLeftEdgeService2.SelectedIndex = 0;
                    cmbLeftEdgeService3.SelectedIndex = 0;
                    cmbLeftEdgeService4.SelectedIndex = 0;
                    cmbAllEdgeService1.SelectedIndex = 0;
                    cmbAllEdgeService2.SelectedIndex = 0;
                    cmbAllEdgeService3.SelectedIndex = 0;
                    cmbAllEdgeService4.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading gas: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #endregion



        // Populate Overall Service Combobox
        private void PopulateOverallServiceComboBox()
        {
            try
            {
                var services = App.DatabaseService.GetFinishedServices();

                if (!services.Contains("None"))
                {
                    cmbOverallServices.Items.Add("None");
                }

                // Add the types from the database
                foreach (var s in services)
                {
                    cmbOverallServices.Items.Add(s);
                }

                // Set initial index
                if (cmbOverallServices.Items.Count > 0)
                {
                    cmbOverallServices.SelectedIndex = 0;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading types: {ex.Message} to populate comboboxes", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Update Preview Function
        private void UpdatePreview()
        {
            // Helper function to persist images
            BitmapImage PersistImage(BitmapImage currentImage, BitmapImage newImage, bool allowClear = false)
            {
                if (allowClear && newImage == null)
                {
                    return null;
                }
                return newImage ?? currentImage;
            }

            // Surface 1 image
            Surface1ServiceImg = PersistImage(Surface1ServiceImg, surface1Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedLeftImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface1ServiceImg
            }, allowClear: true);

            // Surface 2 image
            Surface2ServiceImg = PersistImage(Surface2ServiceImg, surface2Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedRightImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface2ServiceImg
            }, allowClear: true);

            // Surface 3 image
            Surface3ServiceImg = PersistImage(Surface3ServiceImg, surface3Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedLeftImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface3ServiceImg
            }, allowClear: true);

            // Surface 4 image
            Surface4ServiceImg = PersistImage(Surface4ServiceImg, surface4Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedRightImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface4ServiceImg
            }, allowClear: true);

            // Surface 5 image
            Surface5ServiceImg = PersistImage(Surface5ServiceImg, surface5Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedLeftImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface5ServiceImg
            }, allowClear: true);

            // Surface 6 image
            Surface6ServiceImg = PersistImage(Surface6ServiceImg, surface6Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedRightImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface6ServiceImg
            }, allowClear: true);

            // Surface 7 image
            Surface7ServiceImg = PersistImage(Surface7ServiceImg, surface7Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedLeftImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface7ServiceImg
            }, allowClear: true);

            // Surface 8 image
            Surface8ServiceImg = PersistImage(Surface8ServiceImg, surface8Service switch
            {
                "None" => null,
                "Coating" => coatingImage,
                "Patterned" => patternedRightImage,
                "Mirror" => mirrorImage,
                "Sand Blast" => sandBlastImage,
                _ => Surface8ServiceImg
            }, allowClear: true);


            // Dynamically update the images in the visual component
            if (DynamicVisualizationContent.Content is DoubleGlazeVisualComp doubleGlazeComp)
            {
                doubleGlazeComp.Surface1ServiceImg.Source = Surface1ServiceImg;
                doubleGlazeComp.Surface2ServiceImg.Source = Surface2ServiceImg;
                doubleGlazeComp.Surface3ServiceImg.Source = Surface3ServiceImg;
                doubleGlazeComp.Surface4ServiceImg.Source = Surface4ServiceImg;
            }
            else if (DynamicVisualizationContent.Content is TripleGlazeVisualComp tripleGlazeComp)
            {
                tripleGlazeComp.Surface1ServiceImg.Source = Surface1ServiceImg;
                tripleGlazeComp.Surface2ServiceImg.Source = Surface2ServiceImg;
                tripleGlazeComp.Surface3ServiceImg.Source = Surface3ServiceImg;
                tripleGlazeComp.Surface4ServiceImg.Source = Surface4ServiceImg;
                tripleGlazeComp.Surface5ServiceImg.Source = Surface5ServiceImg;
                tripleGlazeComp.Surface6ServiceImg.Source = Surface6ServiceImg;
            }
            else if (DynamicVisualizationContent.Content is QuadrupleGlazeVisualComp quadrupleGlazeComp)
            {
                quadrupleGlazeComp.Surface1ServiceImg.Source = Surface1ServiceImg;
                quadrupleGlazeComp.Surface2ServiceImg.Source = Surface2ServiceImg;
                quadrupleGlazeComp.Surface3ServiceImg.Source = Surface3ServiceImg;
                quadrupleGlazeComp.Surface4ServiceImg.Source = Surface4ServiceImg;
                quadrupleGlazeComp.Surface5ServiceImg.Source = Surface5ServiceImg;
                quadrupleGlazeComp.Surface6ServiceImg.Source = Surface6ServiceImg;
                quadrupleGlazeComp.Surface7ServiceImg.Source = Surface7ServiceImg;
                quadrupleGlazeComp.Surface8ServiceImg.Source = Surface8ServiceImg;
            }

        }


        
        // Update Panel Services Summary
        #region Update Panel Services Summary
        private void UpdatePanelServicesSummary(int panelNumber)
        {
            // Get text values with proper null/empty handling
            Func<string, string> formatValue = (value) => {
                return string.IsNullOrWhiteSpace(value) ? "N/A" : value.Trim();
            };

            // Determine which panel's controls to use
            string[] biteValues = new string[4];
            string[] lowEValues = new string[4];
            string[] stepValues = new string[4];
            string[] edgeServices = new string[4];
            TextBlock summaryTextBlock;

            // Get all values based on panel number
            switch (panelNumber)
            {
                case 1:
                    biteValues[0] = formatValue(txtTopBite1.Text);
                    biteValues[1] = formatValue(txtRightBite1.Text);
                    biteValues[2] = formatValue(txtBottomBite1.Text);
                    biteValues[3] = formatValue(txtLeftBite1.Text);

                    lowEValues[0] = formatValue(txtTopLowERmv1.Text);
                    lowEValues[1] = formatValue(txtRightLowERmv1.Text);
                    lowEValues[2] = formatValue(txtBottomLowERmv1.Text);
                    lowEValues[3] = formatValue(txtLeftLowERmv1.Text);

                    stepValues[0] = formatValue(txtTopStep1.Text);
                    stepValues[1] = formatValue(txtRightStep1.Text);
                    stepValues[2] = formatValue(txtBottomStep1.Text);
                    stepValues[3] = formatValue(txtLeftStep1.Text);

                    edgeServices[0] = formatValue(cmbTopEdgeService1.SelectedItem?.ToString() ?? "None");
                    edgeServices[1] = formatValue(cmbRightEdgeService1.SelectedItem?.ToString() ?? "None");
                    edgeServices[2] = formatValue(cmbBottomEdgeService1.SelectedItem?.ToString() ?? "None");
                    edgeServices[3] = formatValue(cmbLeftEdgeService1.SelectedItem?.ToString() ?? "None");

                    summaryTextBlock = txtBlockServices1;
                    break;
                case 2:
                    biteValues[0] = formatValue(txtTopBite2.Text);
                    biteValues[1] = formatValue(txtRightBite2.Text);
                    biteValues[2] = formatValue(txtBottomBite2.Text);
                    biteValues[3] = formatValue(txtLeftBite2.Text);

                    lowEValues[0] = formatValue(txtTopLowERmv2.Text);
                    lowEValues[1] = formatValue(txtRightLowERmv2.Text);
                    lowEValues[2] = formatValue(txtBottomLowERmv2.Text);
                    lowEValues[3] = formatValue(txtLeftLowERmv2.Text);

                    stepValues[0] = formatValue(txtTopStep2.Text);
                    stepValues[1] = formatValue(txtRightStep2.Text);
                    stepValues[2] = formatValue(txtBottomStep2.Text);
                    stepValues[3] = formatValue(txtLeftStep2.Text);

                    edgeServices[0] = formatValue(cmbTopEdgeService2.SelectedItem?.ToString() ?? "None");
                    edgeServices[1] = formatValue(cmbRightEdgeService2.SelectedItem?.ToString() ?? "None");
                    edgeServices[2] = formatValue(cmbBottomEdgeService2.SelectedItem?.ToString() ?? "None");
                    edgeServices[3] = formatValue(cmbLeftEdgeService2.SelectedItem?.ToString() ?? "None");

                    summaryTextBlock = txtBlockServices2;
                    break;
                case 3:
                    biteValues[0] = formatValue(txtTopBite3.Text);
                    biteValues[1] = formatValue(txtRightBite3.Text);
                    biteValues[2] = formatValue(txtBottomBite3.Text);
                    biteValues[3] = formatValue(txtLeftBite3.Text);

                    lowEValues[0] = formatValue(txtTopLowERmv3.Text);
                    lowEValues[1] = formatValue(txtRightLowERmv3.Text);
                    lowEValues[2] = formatValue(txtBottomLowERmv3.Text);
                    lowEValues[3] = formatValue(txtLeftLowERmv3.Text);

                    stepValues[0] = formatValue(txtTopStep3.Text);
                    stepValues[1] = formatValue(txtRightStep3.Text);
                    stepValues[2] = formatValue(txtBottomStep3.Text);
                    stepValues[3] = formatValue(txtLeftStep3.Text);

                    edgeServices[0] = formatValue(cmbTopEdgeService3.SelectedItem?.ToString() ?? "None");
                    edgeServices[1] = formatValue(cmbRightEdgeService3.SelectedItem?.ToString() ?? "None");
                    edgeServices[2] = formatValue(cmbBottomEdgeService3.SelectedItem?.ToString() ?? "None");
                    edgeServices[3] = formatValue(cmbLeftEdgeService3.SelectedItem?.ToString() ?? "None");

                    summaryTextBlock = txtBlockServices3;
                    break;
                case 4:
                    biteValues[0] = formatValue(txtTopBite4.Text);
                    biteValues[1] = formatValue(txtRightBite4.Text);
                    biteValues[2] = formatValue(txtBottomBite4.Text);
                    biteValues[3] = formatValue(txtLeftBite4.Text);

                    lowEValues[0] = formatValue(txtTopLowERmv4.Text);
                    lowEValues[1] = formatValue(txtRightLowERmv4.Text);
                    lowEValues[2] = formatValue(txtBottomLowERmv4.Text);
                    lowEValues[3] = formatValue(txtLeftLowERmv4.Text);

                    stepValues[0] = formatValue(txtTopStep4.Text);
                    stepValues[1] = formatValue(txtRightStep4.Text);
                    stepValues[2] = formatValue(txtBottomStep4.Text);
                    stepValues[3] = formatValue(txtLeftStep4.Text);

                    edgeServices[0] = formatValue(cmbTopEdgeService4.SelectedItem?.ToString() ?? "None");
                    edgeServices[1] = formatValue(cmbRightEdgeService4.SelectedItem?.ToString() ?? "None");
                    edgeServices[2] = formatValue(cmbBottomEdgeService4.SelectedItem?.ToString() ?? "None");
                    edgeServices[3] = formatValue(cmbLeftEdgeService4.SelectedItem?.ToString() ?? "None");

                    summaryTextBlock = txtBlockServices4;
                    break;
                default:
                    return;
            }

            // Format the display using string interpolation
            string edgeLabels = "Top, Right, Bottom, Left";

            summaryTextBlock.Text =
                $"Edge Order: {edgeLabels}\n" +
                $"• Bite Values: [{string.Join(", ", biteValues)}]\n" +
                $"• Low E Removal Values: [{string.Join(", ", lowEValues)}]\n" +
                $"• Step Values: [{string.Join(", ", stepValues)}]\n" +
                $"• Edge Services: [{string.Join(", ", edgeServices)}]\n\n";                
        }

        // Panel 1 Event Handlers
        private void Panel1EdgesChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePanelServicesSummary(1);
        }

        private void Panel1ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePanelServicesSummary(1);
        }

        // Panel 2 Event Handlers  
        private void Panel2EdgesChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePanelServicesSummary(2);
        }

        private void Panel2ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePanelServicesSummary(2);
        }

        // Panel 3 Event Handlers
        private void Panel3EdgesChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePanelServicesSummary(3);
        }

        private void Panel3ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePanelServicesSummary(3);
        }

        // Panel 4 Event Handlers
        private void Panel4EdgesChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePanelServicesSummary(4);
        }

        private void Panel4ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePanelServicesSummary(4);
        }

        #endregion


        

        // Coating Position Click Events (Arrows)
        #region Coating Position Click Events (Arrows)
        private void HandleCoatingPositionClick(Image image, ref string surfacePrimary, ref string surfaceSecondary)
        {
            // If both sides are set to "Coating" via SurfaceServices, clear outer side first
            if (surfacePrimary == "Coating" && surfaceSecondary == "Coating")
            {
                surfaceSecondary = "None";
                image.Source = outerCoatingArrow;
                UpdatePreview();
                return;
            }

            // Toggle between inner, outer, and null
            if (image.Source == null)
            {
                image.Source = outerCoatingArrow;
                surfaceSecondary = "None";
                surfacePrimary = "Coating";
            }
            else if (image.Source == outerCoatingArrow)
            {
                image.Source = innerCoatingArrow;
                surfacePrimary = "None";
                surfaceSecondary = "Coating";
            }
            else
            {
                image.Source = null;
                surfacePrimary = "None";
                surfaceSecondary = "None";
            }

            UpdatePreview();
        }



        private void coatingPosition1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleCoatingPositionClick(coatingPosition1Img, ref _surface1Service, ref _surface2Service);

            surface1Service = _surface1Service;
            surface2Service = _surface2Service;
        }

        private void coatingPosition2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleCoatingPositionClick(coatingPosition2Img, ref _surface3Service, ref _surface4Service);

            surface3Service = _surface3Service;
            surface4Service = _surface4Service;
        }

        private void coatingPosition3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleCoatingPositionClick(coatingPosition3Img, ref _surface5Service, ref _surface6Service);

            surface5Service = _surface5Service;
            surface6Service = _surface6Service;
        }


        private void coatingPosition4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleCoatingPositionClick(coatingPosition4Img, ref _surface7Service, ref _surface8Service);

            surface7Service = _surface7Service;
            surface8Service = _surface8Service;
        }

        #endregion


        // Surface Services Window Functions
        #region Surface Services Window Functions
        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F8)
            {
                OpenSurfaceServicesWindow();
            }
        }

        private void OpenSurfaceServicesWindow()
        {
            // Collect checkbox states
            var checkboxes = new[] { cbSurface1, cbSurface2, cbSurface3, cbSurface4, cbSurface5, cbSurface6, cbSurface7, cbSurface8 };
            var checkboxStates = checkboxes.Select(cb => cb?.IsChecked ?? false).ToArray();

            // SurfaceServices constructor
            SurfaceServices surfaceServices = new SurfaceServices(
                checkboxStates[0],
                checkboxStates[1],
                checkboxStates[2],
                checkboxStates[3],
                checkboxStates[4],
                checkboxStates[5],
                checkboxStates[6],
                checkboxStates[7]
            );

            if (surfaceServices.ShowDialog() == true)
            {
                var selectedServices = new[]
                {
                    surfaceServices.SelectedService1,
                    surfaceServices.SelectedService2,
                    surfaceServices.SelectedService3,
                    surfaceServices.SelectedService4,
                    surfaceServices.SelectedService5,
                    surfaceServices.SelectedService6,
                    surfaceServices.SelectedService7,
                    surfaceServices.SelectedService8
                };

                surface1Service = selectedServices[0];
                surface2Service = selectedServices[1];
                surface3Service = selectedServices[2];
                surface4Service = selectedServices[3];
                surface5Service = selectedServices[4];
                surface6Service = selectedServices[5];
                surface7Service = selectedServices[6];
                surface8Service = selectedServices[7];

                UpdatePreview();
            }
        }
        #endregion



        // Disable Option Controls
        #region Disable Option Controls
        private void disableOption2Controls()
        {
            lblSpace2.IsEnabled = false;
            txtSpace2.IsEnabled = false;
            lblSpacer2.IsEnabled = false;
            cmbSpacerCode2.IsEnabled = false;
            cmbSpacerType2.IsEnabled = false;
            lblFillGas2.IsEnabled = false;
            cmbFillGas2.IsEnabled = false;
            lblSeal2.IsEnabled = false;
            cmbFillSealant2.IsEnabled = false;
        }

        private void disableOption3Controls()
        {
            lblSpace3.IsEnabled = false;
            txtSpace3.IsEnabled = false;
            lblSpacer3.IsEnabled = false;
            cmbSpacerCode3.IsEnabled = false;
            cmbSpacerType3.IsEnabled = false;
            lblFillGas3.IsEnabled = false;
            cmbFillGas3.IsEnabled = false;
            lblSeal3.IsEnabled = false;
            cmbFillSealant3.IsEnabled = false;
        }
        #endregion



        // All Bites, Low E Removal, Steps, and Edge Service Input Handling
        #region All Bites, Low E Removal, Steps, and Edge Service Input Handling

        private void txtAllBite1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllBite1.Text.Length > 0)
            {
                txtLeftBite1.Text = txtAllBite1.Text;
                txtTopBite1.Text = txtAllBite1.Text;
                txtRightBite1.Text = txtAllBite1.Text;
                txtBottomBite1.Text = txtAllBite1.Text;
            }
            else
            {
                txtLeftBite1.Text = "";
                txtTopBite1.Text = "";
                txtRightBite1.Text = "";
                txtBottomBite1.Text = "";
            }
        }

        private void txtAllLowERmv1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllLowERmv1.Text.Length > 0)
            {
                txtLeftLowERmv1.Text = txtAllLowERmv1.Text;
                txtTopLowERmv1.Text = txtAllLowERmv1.Text;
                txtRightLowERmv1.Text = txtAllLowERmv1.Text;
                txtBottomLowERmv1.Text = txtAllLowERmv1.Text;
            }
            else
            {
                txtLeftLowERmv1.Text = "";
                txtTopLowERmv1.Text = "";
                txtRightLowERmv1.Text = "";
                txtBottomLowERmv1.Text = "";
            }
        }

        private void txtAllStep1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllStep1.Text.Length > 0)
            {
                txtLeftStep1.Text = txtAllStep1.Text;
                txtTopStep1.Text = txtAllStep1.Text;
                txtRightStep1.Text = txtAllStep1.Text;
                txtBottomStep1.Text = txtAllStep1.Text;
            }
            else
            {
                txtLeftStep1.Text = "";
                txtTopStep1.Text = "";
                txtRightStep1.Text = "";
                txtBottomStep1.Text = "";
            }
        }

        private void cmbAllEdgeService1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAllEdgeService1.SelectedItem != null)
            {
                cmbLeftEdgeService1.SelectedIndex = cmbAllEdgeService1.SelectedIndex;
                cmbTopEdgeService1.SelectedIndex = cmbAllEdgeService1.SelectedIndex;
                cmbRightEdgeService1.SelectedIndex = cmbAllEdgeService1.SelectedIndex;
                cmbBottomEdgeService1.SelectedIndex = cmbAllEdgeService1.SelectedIndex;
            }
            else
            {
                cmbLeftEdgeService1.SelectedItem = null;
                cmbTopEdgeService1.SelectedItem = null;
                cmbRightEdgeService1.SelectedItem = null;
                cmbBottomEdgeService1.SelectedItem = null;
            }

        }

        private void txtAllBite2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllBite2.Text.Length > 0)
            {
                txtLeftBite2.Text = txtAllBite2.Text;
                txtTopBite2.Text = txtAllBite2.Text;
                txtRightBite2.Text = txtAllBite2.Text;
                txtBottomBite2.Text = txtAllBite2.Text;
            }
            else
            {
                txtLeftBite2.Text = "";
                txtTopBite2.Text = "";
                txtRightBite2.Text = "";
                txtBottomBite2.Text = "";
            }
        }

        private void txtAllLowERmv2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllLowERmv2.Text.Length > 0)
            {
                txtLeftLowERmv2.Text = txtAllLowERmv2.Text;
                txtTopLowERmv2.Text = txtAllLowERmv2.Text;
                txtRightLowERmv2.Text = txtAllLowERmv2.Text;
                txtBottomLowERmv2.Text = txtAllLowERmv2.Text;
            }
            else
            {
                txtLeftLowERmv2.Text = "";
                txtTopLowERmv2.Text = "";
                txtRightLowERmv2.Text = "";
                txtBottomLowERmv2.Text = "";
            }
        }

        private void txtAllStep2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllStep2.Text.Length > 0)
            {
                txtLeftStep2.Text = txtAllStep2.Text;
                txtTopStep2.Text = txtAllStep2.Text;
                txtRightStep2.Text = txtAllStep2.Text;
                txtBottomStep2.Text = txtAllStep2.Text;
            }
            else
            {
                txtLeftStep2.Text = "";
                txtTopStep2.Text = "";
                txtRightStep2.Text = "";
                txtBottomStep2.Text = "";
            }
        }

        private void cmbAllEdgeService2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAllEdgeService2.SelectedItem != null)
            {
                cmbLeftEdgeService2.SelectedIndex = cmbAllEdgeService2.SelectedIndex;
                cmbTopEdgeService2.SelectedIndex = cmbAllEdgeService2.SelectedIndex;
                cmbRightEdgeService2.SelectedIndex = cmbAllEdgeService2.SelectedIndex;
                cmbBottomEdgeService2.SelectedIndex = cmbAllEdgeService2.SelectedIndex;
            }
            else
            {
                cmbLeftEdgeService2.SelectedItem = null;
                cmbTopEdgeService2.SelectedItem = null;
                cmbRightEdgeService2.SelectedItem = null;
                cmbBottomEdgeService2.SelectedItem = null;
            }
        }

        private void txtAllBite3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllBite3.Text.Length > 0)
            {
                txtLeftBite3.Text = txtAllBite3.Text;
                txtTopBite3.Text = txtAllBite3.Text;
                txtRightBite3.Text = txtAllBite3.Text;
                txtBottomBite3.Text = txtAllBite3.Text;
            }
            else
            {
                txtLeftBite3.Text = "";
                txtTopBite3.Text = "";
                txtRightBite3.Text = "";
                txtBottomBite3.Text = "";
            }
        }

        private void txtAllLowERmv3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllLowERmv3.Text.Length > 0)
            {
                txtLeftLowERmv3.Text = txtAllLowERmv3.Text;
                txtTopLowERmv3.Text = txtAllLowERmv3.Text;
                txtRightLowERmv3.Text = txtAllLowERmv3.Text;
                txtBottomLowERmv3.Text = txtAllLowERmv3.Text;
            }
            else
            {
                txtLeftLowERmv3.Text = "";
                txtTopLowERmv3.Text = "";
                txtRightLowERmv3.Text = "";
                txtBottomLowERmv3.Text = "";
            }
        }

        private void txtAllStep3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllStep3.Text.Length > 0)
            {
                txtLeftStep3.Text = txtAllStep3.Text;
                txtTopStep3.Text = txtAllStep3.Text;
                txtRightStep3.Text = txtAllStep3.Text;
                txtBottomStep3.Text = txtAllStep3.Text;
            }
            else
            {
                txtLeftStep3.Text = "";
                txtTopStep3.Text = "";
                txtRightStep3.Text = "";
                txtBottomStep3.Text = "";
            }
        }

        private void cmbAllEdgeService3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAllEdgeService3.SelectedItem != null)
            {
                cmbLeftEdgeService3.SelectedIndex = cmbAllEdgeService3.SelectedIndex;
                cmbTopEdgeService3.SelectedIndex = cmbAllEdgeService3.SelectedIndex;
                cmbRightEdgeService3.SelectedIndex = cmbAllEdgeService3.SelectedIndex;
                cmbBottomEdgeService3.SelectedIndex = cmbAllEdgeService3.SelectedIndex;
            }
            else
            {
                cmbLeftEdgeService3.SelectedItem = null;
                cmbTopEdgeService3.SelectedItem = null;
                cmbRightEdgeService3.SelectedItem = null;
                cmbBottomEdgeService3.SelectedItem = null;
            }
        }

        private void txtAllBite4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllBite4.Text.Length > 0)
            {
                txtLeftBite4.Text = txtAllBite4.Text;
                txtTopBite4.Text = txtAllBite4.Text;
                txtRightBite4.Text = txtAllBite4.Text;
                txtBottomBite4.Text = txtAllBite4.Text;
            }
            else
            {
                txtLeftBite4.Text = "";
                txtTopBite4.Text = "";
                txtRightBite4.Text = "";
                txtBottomBite4.Text = "";
            }
        }

        private void txtAllLowERmv4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllLowERmv4.Text.Length > 0)
            {
                txtLeftLowERmv4.Text = txtAllLowERmv4.Text;
                txtTopLowERmv4.Text = txtAllLowERmv4.Text;
                txtRightLowERmv4.Text = txtAllLowERmv4.Text;
                txtBottomLowERmv4.Text = txtAllLowERmv4.Text;
            }
            else
            {
                txtLeftLowERmv4.Text = "";
                txtTopLowERmv4.Text = "";
                txtRightLowERmv4.Text = "";
                txtBottomLowERmv4.Text = "";
            }
        }

        private void txtAllStep4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAllStep4.Text.Length > 0)
            {
                txtLeftStep4.Text = txtAllStep4.Text;
                txtTopStep4.Text = txtAllStep4.Text;
                txtRightStep4.Text = txtAllStep4.Text;
                txtBottomStep4.Text = txtAllStep4.Text;
            }
            else
            {
                txtLeftStep4.Text = "";
                txtTopStep4.Text = "";
                txtRightStep4.Text = "";
                txtBottomStep4.Text = "";
            }
        }

        private void cmbAllEdgeService4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAllEdgeService4.SelectedItem != null)
            {
                cmbLeftEdgeService4.SelectedIndex = cmbAllEdgeService4.SelectedIndex;
                cmbTopEdgeService4.SelectedIndex = cmbAllEdgeService4.SelectedIndex;
                cmbRightEdgeService4.SelectedIndex = cmbAllEdgeService4.SelectedIndex;
                cmbBottomEdgeService4.SelectedIndex = cmbAllEdgeService4.SelectedIndex;
            }
            else
            {
                cmbLeftEdgeService4.SelectedItem = null;
                cmbTopEdgeService4.SelectedItem = null;
                cmbRightEdgeService4.SelectedItem = null;
                cmbBottomEdgeService4.SelectedItem = null;
            }
        }
        #endregion



        // Calculate Overall Thickness
        private void CalculateOverallThickness()
        {
            int thickness1 = int.TryParse(txtThickness1.Text, out var t1) ? t1 : 0;
            int thickness2 = int.TryParse(txtThickness2.Text, out var t2) ? t2 : 0;
            int thickness3 = int.TryParse(txtThickness3.Text, out var t3) ? t3 : 0;
            int thickness4 = int.TryParse(txtThickness4.Text, out var t4) ? t4 : 0;
            int space1 = int.TryParse(txtSpace1.Text, out var s1) ? s1 : 0;
            int space2 = int.TryParse(txtSpace2.Text, out var s2) ? s2 : 0;
            int space3 = int.TryParse(txtSpace3.Text, out var s3) ? s3 : 0;

            int overallThickness = thickness1 + thickness2 + thickness3 + thickness4 + space1 + space2 + space3;
            txtBlockOverallThickness.Text = overallThickness.ToString() + "mm";
        }

        

    }
}