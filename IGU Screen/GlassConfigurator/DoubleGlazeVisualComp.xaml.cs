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

namespace SPIL.IGUConfigurator
{
    /// <summary>
    /// Interaction logic for DoubleGlazeVisualComp.xaml
    /// </summary>
    public partial class DoubleGlazeVisualComp : UserControl
    {
        private readonly BitmapImage BaseImage;
        private readonly BitmapImage DoubleSidedArrow;
        public Image Surface1ServiceImg => imgSurface1;
        public Image Surface2ServiceImg => imgSurface2;
        public Image Surface3ServiceImg => imgSurface3;
        public Image Surface4ServiceImg => imgSurface4;

        public TextBlock Panel1ThicknessText => txtBlockPanel1ThicknessVisual;
        public TextBlock Space1ThicknessText => txtBlockSpace1ThicknessVisual;
        public TextBlock Panel2ThicknessText => txtBlockPanel2ThicknessVisual;
        public TextBlock PanelOverallThicknessText => txtBlockOverallThicknessVisual;

        public DoubleGlazeVisualComp()
        {            
            InitializeComponent();
            BaseImage = new BitmapImage(new Uri("pack://application:,,,/Images/Base.png"));
            DoubleSidedArrow = new BitmapImage(new Uri("pack://application:,,,/Images/DoubleSidedArrow.png"));


            imgBase.Source = BaseImage;
            imgOverallThickness.Source = DoubleSidedArrow;
            imgPanel1Thickness.Source = DoubleSidedArrow;
            imgSpace1Thickness.Source = DoubleSidedArrow;
            imgPanel2Thickness.Source = DoubleSidedArrow;


        }
    }
}
