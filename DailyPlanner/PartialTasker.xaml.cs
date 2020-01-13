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

namespace DailyPlanner
{
    /// <summary>
    /// Interaction logic for PartialTasker.xaml
    /// </summary>
    public partial class PartialTasker : Window
    {
        public PartialTasker()
        {
            InitializeComponent();
        }







// ****************************
// ** Rectangle - moving form**
// ****************************


        private void RectangleTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
