using System.Windows;

namespace DailyPlanner
{
    /// <summary>
    /// Interaction logic for TaskerEditor.xaml
    /// </summary>
    public partial class TaskerEditor : Window
    {
        Tasker comunicatorWithTaskerForm;
        

        
        int globalPartialNb;
        bool globalPartialModeOn;
        int globalProgressOfPartialTask = 0;

        public TaskerEditor(int partialNb, Tasker comunicatorWithTaskerForm, bool Mode)
        {
            this.comunicatorWithTaskerForm = comunicatorWithTaskerForm;

            //settings of local variable
            globalPartialModeOn = Mode;
            globalPartialNb = partialNb;
            globalProgressOfPartialTask = comunicatorWithTaskerForm.tGetPartialTaskProgress(partialNb);
            
            
            InitializeComponent();

            teSetLayoutOfThisForm(globalPartialModeOn);

            // refreshing screen
            tRefreshScreen();

        }



        # region Buttons

        // buttons for progress
        private void btnProgress0_Click(object sender, RoutedEventArgs e)
        {
            globalProgressOfPartialTask = 0;
            tResetOpacityInProgressButtonTo25();
            tRefreshScreen();
          //  btnProgress0.Opacity=1;
        }

        private void btnProgress25_Clic(object sender, RoutedEventArgs e)
        {
            globalProgressOfPartialTask = 25;
            tResetOpacityInProgressButtonTo25();
            tRefreshScreen();
           // btnProgress25.Opacity=1;
        }

        private void btnProgress50_Clic(object sender, RoutedEventArgs e)
        {
            globalProgressOfPartialTask = 50;
            tResetOpacityInProgressButtonTo25();
            tRefreshScreen();
           // btnProgress50.Opacity=1;
        }

        private void btnProgress75_Clic(object sender, RoutedEventArgs e)
        {
            globalProgressOfPartialTask = 75;
            tResetOpacityInProgressButtonTo25();
            tRefreshScreen();
          //  btnProgress75.Opacity=1;
        }

        private void btnProgress100_Clic(object sender, RoutedEventArgs e)
        {
            globalProgressOfPartialTask = 100;
            tResetOpacityInProgressButtonTo25();
            tRefreshScreen();
           // btnProgress100.Opacity=1;
        }



        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {

            // code Here 1 -> finishing main/partial task and sending all information to Tasker form

            if (globalPartialModeOn == false)
            {
                // put finishing information here
                comunicatorWithTaskerForm.tSetMainTaskIsActiveFromTaskerEditor(false, globalPartialNb);
            }

            if (globalPartialModeOn==true)
            {
                comunicatorWithTaskerForm.tSetMainTaskIsActiveFromTaskerEditor(false, globalPartialNb);

            }
        }




        private void btnAddAndSave_Click(object sender, RoutedEventArgs e)
        {
            tSendAllDataToTaskerForm();   
            this.Close();
        }




        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #endregion




// *****************************
// *********** METHODS *********
// *****************************

        // setting layput of form depends on run mode
        void teSetLayoutOfThisForm(bool partialMode)
        {
            if (globalPartialModeOn==false)
            {
                TxtPartialDescription.Visibility = System.Windows.Visibility.Hidden;

                lblCompletedIn.Visibility = System.Windows.Visibility.Hidden;
                btnProgress0.Visibility = System.Windows.Visibility.Hidden;
                btnProgress25.Visibility = System.Windows.Visibility.Hidden;
                btnProgress50.Visibility = System.Windows.Visibility.Hidden;
                btnProgress75.Visibility = System.Windows.Visibility.Hidden;
                btnProgress100.Visibility = System.Windows.Visibility.Hidden;

                this.Height = 74;
            }

            
        }

        // set all text boxes in this form and rewrite text of main task and partial tasks
        void tRefreshScreen()
        {
            // main task
            if ((globalPartialModeOn == false) && (comunicatorWithTaskerForm.tGetMainTaskIsActive())) txtPartialTask.Text = comunicatorWithTaskerForm.tGetMainTaskDescription();
           
            // partial task and descriptionb
            if ((globalPartialModeOn == true) && (comunicatorWithTaskerForm.tGetPartialTaskIsActive(globalPartialNb))) txtPartialTask.Text = comunicatorWithTaskerForm.tGetPartialTask(globalPartialNb);
            if ((globalPartialModeOn == true) && (comunicatorWithTaskerForm.tGetPartialTaskIsActive(globalPartialNb))) TxtPartialDescription.Text = comunicatorWithTaskerForm.tGetPartialTaskDescription(globalPartialNb);

            // set proper button
            if (comunicatorWithTaskerForm.tGetPartialTaskProgress(globalPartialNb) == 0 || globalProgressOfPartialTask == 0)
            {
                // button 0% visible
                tResetOpacityInProgressButtonTo25();
                btnProgress0.Opacity = 1;
                
            }




            // ******** Button refreshing ********

            if (comunicatorWithTaskerForm.tGetPartialTaskProgress(globalPartialNb) == 25 || globalProgressOfPartialTask == 25)
            {
                // button 25% visible
                tResetOpacityInProgressButtonTo25();
                btnProgress25.Opacity = 1;
            }

            if (comunicatorWithTaskerForm.tGetPartialTaskProgress(globalPartialNb) == 50 || globalProgressOfPartialTask == 50)
            {
                // button 50% visible
                tResetOpacityInProgressButtonTo25();
                btnProgress50.Opacity = 1;
            }

            if (comunicatorWithTaskerForm.tGetPartialTaskProgress(globalPartialNb) == 75 || globalProgressOfPartialTask == 075)
            {
                // button 75% visible
                tResetOpacityInProgressButtonTo25();
                btnProgress75.Opacity = 1;
            }

            if (comunicatorWithTaskerForm.tGetPartialTaskProgress(globalPartialNb) == 100 || globalProgressOfPartialTask == 100)
            {
                // button 100% visible
                tResetOpacityInProgressButtonTo25();
                btnProgress100.Opacity = 1;
            }
  
            // **************** DataPicker Refreshing
            DataDeadline.Text = comunicatorWithTaskerForm.tGetPartialTaskDeadLine(globalPartialNb);

        }

       
         void tResetOpacityInProgressButtonTo25()
        {
            btnProgress0.Opacity = 0.25;
            btnProgress25.Opacity = 0.25;
            btnProgress50.Opacity = 0.25;
            btnProgress75.Opacity = 0.25;
            btnProgress100.Opacity = 0.25;
        }

        // sending all information to tasker form
        void tSendAllDataToTaskerForm()
         {
             if (globalPartialModeOn == false)
             {
                 comunicatorWithTaskerForm.tSetMainTaskFromTaskerEditor(txtPartialTask.Text, globalPartialNb);
                 comunicatorWithTaskerForm.tSetMainTaskIsActiveFromTaskerEditor(true, globalPartialNb);
                 comunicatorWithTaskerForm.tSetMainTaskDeadLineFromTaskerEditor(DataDeadline.Text, globalPartialNb);
             }
            
            if (globalPartialModeOn == true)
             {

                 comunicatorWithTaskerForm.tSetPartialTaskFromTaskerEditor(txtPartialTask.Text, globalPartialNb);
                 comunicatorWithTaskerForm.tSetPartialTaskDescritpionFromTaskerEditor(TxtPartialDescription.Text, globalPartialNb);
                 comunicatorWithTaskerForm.tSetPartialTaskDeadLineFromTaskerEditor(DataDeadline.Text, globalPartialNb);
                 comunicatorWithTaskerForm.tSetPartialTaskProgress(globalProgressOfPartialTask, globalPartialNb);
                 comunicatorWithTaskerForm.tSetPartialTaskIsActive(true, globalPartialNb);

                 // <- partial task finished so status Isfinished will be change to true 
                if (globalProgressOfPartialTask==100) 
                {
                    comunicatorWithTaskerForm.tSetPartialTaskIsFinished(true, globalPartialNb);
                }

             }

         }



    }
}
