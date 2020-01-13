using System.Windows;

namespace DailyPlanner.remindIt
{
    /// <summary>
    /// Interaction logic for Reminder.xaml
    /// </summary>
    public partial class Reminder : Window
    {
        public Reminder()
        {
            InitializeComponent();
        }

        #region Buttons
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseThisWindow();
        }


        #endregion


        private void CloseThisWindow()
        {
            this.Close();
        }

    }
}
