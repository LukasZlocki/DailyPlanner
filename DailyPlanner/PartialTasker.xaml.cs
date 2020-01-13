using System.Windows;
using System.Windows.Input;

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
