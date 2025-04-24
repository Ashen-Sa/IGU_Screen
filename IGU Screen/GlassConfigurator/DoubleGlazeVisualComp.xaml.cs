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
        public Image Surface1ServiceImg => imgSurface1;
        public Image Surface2ServiceImg => imgSurface2;
        public Image Surface3ServiceImg => imgSurface3;
        public Image Surface4ServiceImg => imgSurface4;

        public DoubleGlazeVisualComp()
        {            
            InitializeComponent();
            BaseImage = new BitmapImage(new Uri("pack://application:,,,/Images/Base.png"));
            imgBase.Source = BaseImage;
        }
    }
}
