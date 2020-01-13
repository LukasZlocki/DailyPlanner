using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DailyPlanner
{
    /// <summary>
    /// Interaction logic for Tasker.xaml
    /// </summary>
    public partial class Tasker : Window
    {

        MainWindow comunicatorWithMainWindow;
        DailyTasker thisTask = new DailyTasker();

        int globalTaskNb=0;
        int globalLastEmptyPartialTask=666;
        int globalHeight;
        static int globalIncrementHeight = 80;

        bool slideOn = false;
        bool isMainTaskActive = false;
        bool partialModeOn = true;
        


        public Tasker(int tNb, MainWindow comunicatorWithMainWindow)
        {
            globalTaskNb = tNb;
            this.comunicatorWithMainWindow = comunicatorWithMainWindow;

            
            // setting form possition
            tSettingFormPosition(globalTaskNb);


            InitializeComponent();

            // getting partial task data and showing in txtBoxes in this form
            tGetAllPartialTaskDataFromMainWindow(globalTaskNb);
            tShowAllPartialTaskInTextBoxesOfThisForm();

            //set empty field for partial task
            globalLastEmptyPartialTask=tFindEmptyPartialTaskFieldAndSetValue();

            isMainTaskActive = tCheckIfMainTaskIsActive(globalTaskNb);
            tSettingFormHeight(tGetNumberOfActivePartialTasks(),false);

            tSetButtonLayoutInThisForm(slideOn,isMainTaskActive);
            

         //   MessageBox.Show("this form X: " + comunicatorWithMainWindow.mGetFormPosX(TaskNb) + " This form Y: " + comunicatorWithMainWindow.mGetFormPosY(TaskNb));
   

        }


        #region - Buttons

        // button for finishing Main Task and closing window
        private void btnFinishReady_Click(object sender, RoutedEventArgs e)
        {
            if (thisTask.tIsMainTaskCanBeFinished() == true)
            {
                comunicatorWithMainWindow.mSetMainTaskIsActive(false, globalTaskNb);
                comunicatorWithMainWindow.mSetMainTaskIsFinished(true, globalTaskNb);

                this.Close();
            }
        }


        // button for adding partial tasks
        private void btnAddNewPartial_Click(object sender, RoutedEventArgs e)
        {
            slideOn = true;
            tSettingFormHeight(tGetNumberOfActivePartialTasks()+1, false);

        }




        // main button opening main task editor OR sliding partial tasks down
        private void btnOpenTaskForm_Click(object sender, RoutedEventArgs e)
        {
        
            // No mainTask implemented so we need to open TaskerEditor and implement MainTask 
            if (isMainTaskActive==false)
            {
                partialModeOn = false;
                TaskerEditor taskerEditor = new TaskerEditor(globalTaskNb, this, partialModeOn);
                taskerEditor.Show();
            }
            // slide DOWN form and show partial tasks
            if (isMainTaskActive==true && slideOn==false)
            {
                slideOn = true;
             //   MessageBox.Show("[BUTTON] -> isMainTaskActive: " + isMainTaskActive + " slideOn: " + slideOn);
                tSettingFormHeight(tGetNumberOfActivePartialTasks(), false);      
            }
            else
            { 
            // slide UP form and show partial tasks
            // old part of code ->   if (isMainTaskActive == true && slideOn == true)
            // old part of code ->  {  
                slideOn = false;
              //  MessageBox.Show("[BUTTON] -> isMainTaskActive: " + isMainTaskActive + " slideOn: " + slideOn);
                tSettingFormHeight(tGetNumberOfActivePartialTasks(), false);
            }                        
        }

        
        private void btnAddPartial1_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(0);
        }

        private void btnAddPartial2_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(1);
        }

        private void btnAddPartial3_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(2);
        }

        private void btnAddPartial4_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(3);
        }

        private void btnAddPartial5_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(4);
        }

        private void btnAddPartial6_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(5);
        }

        private void btnAddPartial7_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(6);
        }

        private void btnAddPartial8_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(7);
        }

        private void btnAddPartial9_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(8);
        }

        private void btnAddPartial10_Click(object sender, RoutedEventArgs e)
        {
            AddPartialTaskByButton(9);
        }

        // function supporting AddingPartialTaksks Buttons 
        private void AddPartialTaskByButton(int buttonNb)
        {
            partialModeOn = true;
            TaskerEditor taskerEditor = new TaskerEditor(buttonNb, this, partialModeOn);
            taskerEditor.Show();
        }


        #endregion


        #region Other methods


        // ***************************
        // *** OTHER METHODs *********
        // ***************************


        // finding empty field of partial task - returning nb of field
        private int tFindEmptyPartialTaskFieldAndSetValue()
        {
            int emptyField = 666;

            for (int i = 0; i < thisTask.tGetPartialTaskQuantity(); i++)
            {
                if (thisTask.tGetPartialTaskIsActive(i) == false) return (emptyField=i);
            }
         return 666; // 666 -> no empty fields
        }


        private int tGetNumberOfActivePartialTasks()
        {
            int counter=0;

            for (int i = 0; i < thisTask.tGetPartialTaskQuantity(); i++)
            {
                if (thisTask.tGetPartialTaskIsActive(i) == true) counter++; 
            }
            return counter;
        }


        
        // Show data in text box in this form   
       private void tShowAllPartialTaskInTextBoxesOfThisForm()
        {

            txtMainTaskDescr.Text = thisTask.tGetMainTask();
            txtDLImplementedbyUser.Text = thisTask.tGetMainTaskDeadLine();

            txtPartial1.Text = thisTask.tGetPartialTask(0);
            txtPartial2.Text = thisTask.tGetPartialTask(1);
            txtPartial3.Text = thisTask.tGetPartialTask(2);
            txtPartial4.Text = thisTask.tGetPartialTask(3);
            txtPartial5.Text = thisTask.tGetPartialTask(4);
            txtPartial6.Text = thisTask.tGetPartialTask(5);
            txtPartial7.Text = thisTask.tGetPartialTask(6);
            txtPartial8.Text = thisTask.tGetPartialTask(7);
            txtPartial9.Text = thisTask.tGetPartialTask(8);
            txtPartial10.Text = thisTask.tGetPartialTask(9);

            txtPartial1Description.Text = thisTask.tGetPartialTaskDescription(0);
            txtPartial2Description.Text = thisTask.tGetPartialTaskDescription(1);
            txtPartial3Description.Text = thisTask.tGetPartialTaskDescription(2);
            txtPartial4Description.Text = thisTask.tGetPartialTaskDescription(3);
            txtPartial5Description.Text = thisTask.tGetPartialTaskDescription(4);
            txtPartial6Description.Text = thisTask.tGetPartialTaskDescription(5);
            txtPartial7Description.Text = thisTask.tGetPartialTaskDescription(6);
            txtPartial8Description.Text = thisTask.tGetPartialTaskDescription(7);
            txtPartial9Description.Text = thisTask.tGetPartialTaskDescription(8);
            txtPartial10Description.Text = thisTask.tGetPartialTaskDescription(9);

            txtDLImplementedPartial1.Text = thisTask.tGetPartialTaskDeadLine(0);
            txtDLImplementedPartial2.Text = thisTask.tGetPartialTaskDeadLine(1);
            txtDLImplementedPartial3.Text = thisTask.tGetPartialTaskDeadLine(2);
            txtDLImplementedPartial4.Text = thisTask.tGetPartialTaskDeadLine(3);
            txtDLImplementedPartial5.Text = thisTask.tGetPartialTaskDeadLine(4);
            txtDLImplementedPartial6.Text = thisTask.tGetPartialTaskDeadLine(5);
            txtDLImplementedPartial7.Text = thisTask.tGetPartialTaskDeadLine(6);
            txtDLImplementedPartial8.Text = thisTask.tGetPartialTaskDeadLine(7);
            txtDLImplementedPartial9.Text = thisTask.tGetPartialTaskDeadLine(8);
            txtDLImplementedPartial10.Text = thisTask.tGetPartialTaskDeadLine(9);           

            // calculating Deadlines - Days left
            txtMainTaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetMainTaskDeadLine());

            txtPartial1TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(0));
            txtPartial2TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(1));
            txtPartial3TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(2));
            txtPartial4TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(3));
            txtPartial5TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(4));
            txtPartial6TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(5));
            txtPartial7TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(6));
            txtPartial8TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(7));
            txtPartial9TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(8));
            txtPartial10TaskDayLeft.Text = thisTask.tShowCounter(thisTask.tGetPartialTaskDeadLine(9));

           // percentage completition for partial tasks
            txtPartial1Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(0)) + '\u0025';
            txtPartial2Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(1)) + '\u0025';
            txtPartial3Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(2)) + '\u0025';
            txtPartial4Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(3)) + '\u0025';
            txtPartial5Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(4)) + '\u0025';
            txtPartial6Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(5)) + '\u0025';
            txtPartial7Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(6)) + '\u0025';
            txtPartial8Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(7)) + '\u0025';
            txtPartial9Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(8)) + '\u0025';
            txtPartial10Percentage.Text = Convert.ToString(thisTask.tGetPartialTaskProgress(9)) + '\u0025';

           // set layout of completed partial task
            if (thisTask.tGetPartialTaskProgress(0) == 100) tHideColoursOfFinishedPartialTask(0);
            if (thisTask.tGetPartialTaskProgress(1) == 100) tHideColoursOfFinishedPartialTask(1);
            if (thisTask.tGetPartialTaskProgress(2) == 100) tHideColoursOfFinishedPartialTask(2);
            if (thisTask.tGetPartialTaskProgress(3) == 100) tHideColoursOfFinishedPartialTask(3);
            if (thisTask.tGetPartialTaskProgress(4) == 100) tHideColoursOfFinishedPartialTask(4);
            if (thisTask.tGetPartialTaskProgress(5) == 100) tHideColoursOfFinishedPartialTask(5);
            if (thisTask.tGetPartialTaskProgress(6) == 100) tHideColoursOfFinishedPartialTask(6);
            if (thisTask.tGetPartialTaskProgress(7) == 100) tHideColoursOfFinishedPartialTask(7);
            if (thisTask.tGetPartialTaskProgress(8) == 100) tHideColoursOfFinishedPartialTask(8);
            if (thisTask.tGetPartialTaskProgress(9) == 100) tHideColoursOfFinishedPartialTask(9);

            // check if FinishReady Button can be displayed
            if (thisTask.tIsMainTaskCanBeFinished() == true)
            {
                btnFinishReady.Opacity = 1;
            }
            else
            {
                btnFinishReady.Opacity = 0.25;
            }

        }


        bool tCheckIfMainTaskIsActive(int tNb)
        {
            return comunicatorWithMainWindow.mGetMainTaskActive(tNb);
        }



        // **************** Do Not Try to Delete This ***********
        private void txtMainTaskDescr_TextChanged(object sender, TextChangedEventArgs e)
        {


        }




        #endregion


        # region Comunication module with MainWindow
      
        // ***********************************
        // *** SENDING Data to MainWindow ****
        // ***********************************
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

            comunicatorWithMainWindow.mSendFormPosToTaskerClass(globalTaskNb, this.Left, this.Top);
        }



        // sending form possition - ta metoda chyba nie działa wykasowac !
        private void Task_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //  if (e.ChangedButton == MouseButton.Left)
            //     this.DragMove();

            //  comunicatorWithMainWindow.mSendFormPosToTaskerClass(TaskNb, this.Left, this.Top);
        }



        // ***************************************
        // *** GETTING Data from MainWindow ******
        // ***************************************


        // Getting Partial Task data from class 
        void tGetAllPartialTaskDataFromMainWindow(int tNb)
        {
            thisTask.tSetMainTask(comunicatorWithMainWindow.mGetMainTask(tNb));
            thisTask.tSetMainTaskDeadLine(comunicatorWithMainWindow.mGetMainTaskDeadLine(tNb));
            thisTask.tSetMainTaskIsActive(comunicatorWithMainWindow.mGetMainTaskActive(tNb));

            for (int i = 0; i < thisTask.tGetPartialTaskQuantity(); i++)
            {
                thisTask.tSetPartialTask(comunicatorWithMainWindow.mGetPartialTask(tNb, i), i);
                thisTask.tSetPartialTaskDescription(comunicatorWithMainWindow.mGetPartialTaskDescription(tNb, i), i);
                thisTask.tSetPartialTaskDeadLine(comunicatorWithMainWindow.mGetPartialTaskDeadLine(tNb, i), i);
                thisTask.tSetPartialTaskProgress(comunicatorWithMainWindow.mGetPartialTaskProgress(tNb, i), i);
                thisTask.tSetPartialTaskIsActive(comunicatorWithMainWindow.mGetPartialTaskIsActive(tNb, i), i);
                thisTask.tSetPartialTaskIsFinished(comunicatorWithMainWindow.mGetPartialTaskIsFinished(tNb, i), i);
            }

        }

        #endregion


        #region Comunication module with TaskerEditor


        public void tSetMainTaskFromTaskerEditor(string Task, int tNb)
        {
            comunicatorWithMainWindow.mSetMainTask(Task, tNb);
            comunicatorWithMainWindow.mSetMainTaskIsActive(true, tNb);

            thisTask.tSetMainTask(Task);
            thisTask.tSetMainTaskIsActive(true);
            isMainTaskActive = true;

            txtMainTaskDescr.Text = Task;
            tSetButtonLayoutInThisForm(false, true); // refreshing button

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all

        }


        public void tSetMainTaskDeadLineFromTaskerEditor (string deadLine, int tNb)
        {
            comunicatorWithMainWindow.mSetMainTaskDeadLine(deadLine, tNb);
            thisTask.tSetMainTaskDeadLine(deadLine);
           
          //  txtDLImplementedbyUser.Text = deadLine; // refreshing DeadLine on screen

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all

        }

        public void tSetMainTaskIsActiveFromTaskerEditor (bool isActive, int tNb)
        {
            comunicatorWithMainWindow.mSetMainTaskIsActive(isActive, tNb);

            thisTask.tSetMainTaskIsActive(isActive);
            
            isMainTaskActive = isActive;
        }

        public void tSetMainTaskIsFinished (bool isFinished, int tNb)
        {
            thisTask.tSetMainTaskIsFinished(isFinished);

            comunicatorWithMainWindow.mSetMainTaskIsFinished(isFinished, tNb);
        }


        public void tSetPartialTaskFromTaskerEditor(string pTask, int ptNb)
        {
           thisTask.tSetPartialTask(pTask, ptNb);
           comunicatorWithMainWindow.mSetPartialTaskFromTasker(pTask, globalTaskNb, ptNb);

         //  if (ptNb == 0) txtPartial1.Text = pTask;
         //  if (ptNb == 1) txtPartial2.Text = pTask;
         //  if (ptNb == 2) txtPartial3.Text = pTask;
         //  if (ptNb == 3) txtPartial4.Text = pTask;
         //  if (ptNb == 4) txtPartial5.Text = pTask;

           tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all
        }


        public void tSetPartialTaskDescritpionFromTaskerEditor(string pTaskDesc, int ptNb)
        {
            thisTask.tSetPartialTaskDescription(pTaskDesc, ptNb);
            comunicatorWithMainWindow.mSetPartialTaskDescriptionFromTasker(pTaskDesc, globalTaskNb, ptNb);

         //   if (ptNb == 0) txtPartial1Description.Text = pTaskDesc;
          //  if (ptNb == 1) txtPartial2Description.Text = pTaskDesc;
         //   if (ptNb == 2) txtPartial3Description.Text = pTaskDesc;
          //  if (ptNb == 3) txtPartial4Description.Text = pTaskDesc;
          //  if (ptNb == 4) txtPartial5Description.Text = pTaskDesc;

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all
        }


        public void tSetPartialTaskDeadLineFromTaskerEditor(string partialDL, int ptNb)
        {
            thisTask.tSetPartialTaskDeadLine(partialDL, ptNb);

            comunicatorWithMainWindow.mSetPartialTaskDeadLineFromTasker (partialDL, globalTaskNb, ptNb);

         //   if (ptNb == 0) txtDLImplementedPartial1.Text = partialDL;
         //   if (ptNb == 1) txtDLImplementedPartial1.Text = partialDL;
         //   if (ptNb == 2) txtDLImplementedPartial1.Text = partialDL;
         //   if (ptNb == 3) txtDLImplementedPartial1.Text = partialDL;
        //    if (ptNb == 4) txtDLImplementedPartial1.Text = partialDL;

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all
        }

        public void tSetPartialTaskProgress(int progress, int ptNb)
        {
            thisTask.tSetPartialTaskProgress(progress, ptNb);
            comunicatorWithMainWindow.mSetPartialTaskProgressFromTasker(progress, globalTaskNb, ptNb);

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all
        }

        public void tSetPartialTaskIsActive(bool set, int ptNb)
        {
            thisTask.tSetPartialTaskIsActive(set, ptNb);
            comunicatorWithMainWindow.mSetPartialTaskIsActiveFromTasker(set, globalTaskNb, ptNb);

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all
        }

        public void tSetPartialTaskIsFinished (bool isFinished, int ptNb)
        {
            thisTask.tSetPartialTaskIsFinished(isFinished, ptNb);
            comunicatorWithMainWindow.mSetPartialTaskIsFinishedFromTasker(isFinished, globalTaskNb, ptNb);

            tShowAllPartialTaskInTextBoxesOfThisForm(); // general refrewash all

        }

        
        
        // ***********************************
        // ****** sending data to TaskerEditor 
        // ***********************************

        public string tGetPartialTaskDescription(int partialNb )
        {
            string stringToReturn = "";

            if (partialNb == 0) stringToReturn = txtPartial1Description.Text;
            if (partialNb == 1) stringToReturn = txtPartial2Description.Text;
            if (partialNb == 2) stringToReturn = txtPartial3Description.Text;
            if (partialNb == 3) stringToReturn = txtPartial4Description.Text; 
            if (partialNb == 4) stringToReturn = txtPartial5Description.Text;
            if (partialNb == 5) stringToReturn = txtPartial6Description.Text;
            if (partialNb == 6) stringToReturn = txtPartial7Description.Text;
            if (partialNb == 7) stringToReturn = txtPartial8Description.Text;
            if (partialNb == 8) stringToReturn = txtPartial9Description.Text;
            if (partialNb == 9) stringToReturn = txtPartial10Description.Text; 

            return stringToReturn;
        }

        public string tGetPartialTask(int partialNb)
        {
            return thisTask.tGetPartialTask(partialNb);
        }

        public int tGetPartialTaskProgress(int partialNb)
        {
            return thisTask.tGetPartialTaskProgress(partialNb);
        }

        public string tGetPartialTaskDeadLine(int partialNb)
        {
            return thisTask.tGetPartialTaskDeadLine(partialNb);
        }

        public bool tGetPartialTaskIsActive(int partialNb)
        {
            return thisTask.tGetPartialTaskIsActive(partialNb);
        }


        public string tGetMainTaskDescription ()
        {
            return (thisTask.tGetMainTask());
        }

        public bool tGetMainTaskIsActive()
        {
            return thisTask.tGetMainTaskIsActive();
        }

        

        #endregion


        #region This form settings like layout possition / length / hight 

        void tSettingFormPosition(int tNb)
        {
            this.Left = comunicatorWithMainWindow.mGetTaskerFormPosX(tNb);
            this.Top = comunicatorWithMainWindow.mGetTaskerFormPosY(tNb);
        }


        // setting form height in many variants -> without or with MainTask active / incrementation by button /  at start when partial tasks are present
        void tSettingFormHeight(int nbTaskPosition, bool isProcessIncrementing)
        {
            /// setting base hight with value of 38 px
            this.Height = 60;
                   
            // setting of height if no MainTask available
            if (isMainTaskActive==false || slideOn==false)
            {
              this.Height = 34;
              btnAddNewPartial.Visibility = Visibility.Hidden;
            }

            // setting of height on start of form - height depand on quantity of partial tasks
            if (isProcessIncrementing == false && isMainTaskActive==true && slideOn==true)
            {
                btnAddNewPartial.Visibility = Visibility.Visible;

                // add code to calculate form height according to partial task quantity
              //  if (nbTaskPosition == 666) this.Height = this.Height + 0 * globalIncrementHeight;
                if (nbTaskPosition == 0) this.Height = this.Height + 0 * globalIncrementHeight;
                if (nbTaskPosition == 1) this.Height = this.Height + 1 * globalIncrementHeight;
                if (nbTaskPosition == 2) this.Height = this.Height + 2 * globalIncrementHeight;
                if (nbTaskPosition == 3) this.Height = this.Height + 3 * globalIncrementHeight;
                if (nbTaskPosition == 4) this.Height = this.Height + 4 * globalIncrementHeight;
                if (nbTaskPosition == 5) this.Height = this.Height + 5 * globalIncrementHeight;
                if (nbTaskPosition == 6) this.Height = this.Height + 6 * globalIncrementHeight;
                if (nbTaskPosition == 7) this.Height = this.Height + 7 * globalIncrementHeight;
                if (nbTaskPosition == 8) this.Height = this.Height + 8 * globalIncrementHeight;
                if (nbTaskPosition == 9) this.Height = this.Height + 9 * globalIncrementHeight;
                

            }

            // setting of height during adding (by button) partial tasks 
            if (isProcessIncrementing==true && isMainTaskActive==true && slideOn==true)
            {
                MessageBox.Show("Global partial task <--: "+nbTaskPosition);
                if (nbTaskPosition == 0) this.Height = 128 + 1 * globalIncrementHeight;
                if (nbTaskPosition == 1) this.Height = 128 + 2 * globalIncrementHeight;
                if (nbTaskPosition == 2) this.Height = 128 + 3 * globalIncrementHeight;
                if (nbTaskPosition == 3) this.Height = 128 + 4 * globalIncrementHeight;
                if (nbTaskPosition == 4) this.Height = 128 + 5 * globalIncrementHeight;
                if (nbTaskPosition == 5) this.Height = 128 + 6 * globalIncrementHeight;
                if (nbTaskPosition == 6) this.Height = 128 + 7 * globalIncrementHeight;
                if (nbTaskPosition == 7) this.Height = 128 + 8 * globalIncrementHeight;
                if (nbTaskPosition == 8) this.Height = 128 + 9 * globalIncrementHeight;
                if (nbTaskPosition == 9) this.Height = 128 + 10 * globalIncrementHeight;

              //  this.Height = this.Height + globalIncrementHeight;
            }


        //    MessageBox.Show(" ustawienie formy height " + this.Height);

            // *** setting button Layout ***
           tSetButtonLayoutInThisForm(slideOn, isMainTaskActive);
        }



        // Button Layout Settings
        void tSetButtonLayoutInThisForm(bool slideon, bool mainTaskActive)
        {

            /// ******** Button view **********
            /// 
            if (mainTaskActive == false)
            {
                btnSlideDownForm.Content = "<-";
            }

            if (mainTaskActive == true && slideon == false)
            {
                btnSlideDownForm.Content = "v";
            }

            if (mainTaskActive == true && slideon == true)
            {
                btnSlideDownForm.Content = "^";
            }
        }

        #endregion


        #region Colour settings at areas


        void tHideColoursOfFinishedPartialTask(int ptToHideText)
    {
            if (ptToHideText==0)
            {
                txtPartial1.Foreground = Brushes.Gray;
                txtPartial1Description.Foreground = Brushes.Gray;
                txtPartial1TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial1Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy.Foreground = Brushes.Gray;
                txtDLImplementedPartial1.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 1)
            {
                txtPartial2.Foreground = Brushes.Gray;
                txtPartial2Description.Foreground = Brushes.Gray;
                txtPartial2TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial2Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy1.Foreground = Brushes.Gray;
                txtDLImplementedPartial2.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 2)
            {
                txtPartial3.Foreground = Brushes.Gray;
                txtPartial3Description.Foreground = Brushes.Gray;
                txtPartial3TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial3Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy2.Foreground = Brushes.Gray;
                txtDLImplementedPartial3.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 3)
            {
                txtPartial4.Foreground = Brushes.Gray;
                txtPartial4Description.Foreground = Brushes.Gray;
                txtPartial4TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial4Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy3.Foreground = Brushes.Gray;
                txtDLImplementedPartial4.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 4)
            {
                txtPartial5.Foreground = Brushes.Gray;
                txtPartial5Description.Foreground = Brushes.Gray;
                txtPartial5TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial5Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy4.Foreground = Brushes.Gray;
                txtDLImplementedPartial5.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 5)
            {
                txtPartial6.Foreground = Brushes.Gray;
                txtPartial6Description.Foreground = Brushes.Gray;
                txtPartial6TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial6Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy6.Foreground = Brushes.Gray;
                txtDLImplementedPartial6.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 6)
            {
                txtPartial7.Foreground = Brushes.Gray;
                txtPartial7Description.Foreground = Brushes.Gray;
                txtPartial7TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial7Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy7.Foreground = Brushes.Gray;
                txtDLImplementedPartial7.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 7)
            {
                txtPartial8.Foreground = Brushes.Gray;
                txtPartial8Description.Foreground = Brushes.Gray;
                txtPartial8TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial8Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy8.Foreground = Brushes.Gray;
                txtDLImplementedPartial8.Foreground = Brushes.Gray;
            }


            if (ptToHideText == 8)
            {
                txtPartial9.Foreground = Brushes.Gray;
                txtPartial9Description.Foreground = Brushes.Gray;
                txtPartial9TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial9Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy9.Foreground = Brushes.Gray;
                txtDLImplementedPartial9.Foreground = Brushes.Gray;
            }

            if (ptToHideText == 9)
            {
                txtPartial10.Foreground = Brushes.Gray;
                txtPartial10Description.Foreground = Brushes.Gray;
                txtPartial10TaskDayLeft.Foreground = Brushes.Gray;
                txtPartial10Percentage.Foreground = Brushes.Gray;
                txtDLDescr_Copy10.Foreground = Brushes.Gray;
                txtDLImplementedPartial10.Foreground = Brushes.Gray;
            }

    }

        #endregion


    }
}
