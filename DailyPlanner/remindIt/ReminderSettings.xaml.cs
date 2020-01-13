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

namespace DailyPlanner.remindIt
{
    /// <summary>
    /// Interaction logic for ReminderSettings.xaml
    /// </summary>
    public partial class ReminderSettings : Window
    {

        DailyReminder DailyReminder = new DailyReminder();

        public ReminderSettings()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            DailyReminder.SetShortDescription(txtShortDescription.Text);
            DailyReminder.SetDescription(txtDescription.Text);
            DailyReminder.SetActivationDate(dataPicker.SelectedDate.Value.Date.ToString());

            // TODO : send data to main window by communicator with main window



            this.Close();
        }



        

    }
}
