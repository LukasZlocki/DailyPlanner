using DailyPlanner.IOAdapters;
using DailyPlanner.postIt;
using DailyPlanner.VersionManager;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DailyPlanner
{
    /// <summary>
    /// DailyPlanner by 3Dev 
    /// Ver 1.0
    /// Programmed : somewhere in 2016
    /// 
    /// what's new:
    /// [14.10.2018] -> add colors of Note Windows & add big/small letters to Notes
    /// [25.04.2018] -> Hiding windows on task bar
    /// [22.04.2018] -> Small/big fonts button added
    /// [19.08.2017] -> Fixed bugs : 1 - more than 99 days of dalay was not shown on screen / 2 - first time running the program shown errors with opening the file , this is fixed now 
    /// [05.03.2017] -> Show / Hide function for Notes and Tasks added 
    /// [22.02.2017] -> Notes function added to programm
    /// [01.02.2016] -> Adding finish button for finishing Main Tasks
    /// [All   2016] -> Finishing the program and tests 
    /// </summary>


    // TODO :
    // TODO : #1 -> DailyReminder
    // TODO : #2 -> Rozszerzanie okienka z postIt przez usera


    public partial class MainWindow : Window
    {
        private static int NOTES_MAX_QUANTITY = 50;
        private static int TASKER_MAX_QUANTITY = 50;
        private int PARTIAL_TASKS_MAX_QUANTITY = 10;
       
        private int GlobalTaskCounter = 0;
        private int GlobalNoteCounter = 0;

        private bool NotesAreVisible = true;
        private bool TasksAreVisible = true;

        // Tasks
        DailyTasker QuantitySettings = new DailyTasker(); // object with quantity settings
        DailyTasker[] Tasker = new DailyTasker[TASKER_MAX_QUANTITY];
        Tasker[] formTasker = new Tasker[TASKER_MAX_QUANTITY];

        // Notes
        DailyNote[] Note = new DailyNote[NOTES_MAX_QUANTITY]; // Dailynote class object
        Note[] formNote = new Note[NOTES_MAX_QUANTITY]; // <- Window object

        // Reminds
        List<DailyReminder> DailyReminders = new List<DailyReminder>();

        // forms position objects
        FormPosition ThisFormPosition = new FormPosition();
        FormPosition[] TaskerFormPosition = new FormPosition[TASKER_MAX_QUANTITY];
        FormPosition[] NoteFormPosition = new FormPosition[NOTES_MAX_QUANTITY];
      
        // IO object
        IOadapter IOadapt = new IOadapter();

        // Version manager
        VersionManage VersionManager = new VersionManage();


        public MainWindow()

        {
            // Version check and updates 
            VersionManager.CheckVersion(NOTES_MAX_QUANTITY);

            //setting quantities from class
            PARTIAL_TASKS_MAX_QUANTITY = QuantitySettings.tGetPartialTaskQuantity();

            // Objects inits
            mTaskerObjectInitializer(TASKER_MAX_QUANTITY);
            mTaskerPositionInitializer(TASKER_MAX_QUANTITY);

            mNotePositionInitializer(NOTES_MAX_QUANTITY);
            mNoteObjectInitializer(NOTES_MAX_QUANTITY);

            // loading data from program files
            mLoadThisFormPosition();
            mLoadTaskerFormPosition(TASKER_MAX_QUANTITY);
            mLoadDailyTaskerData(TASKER_MAX_QUANTITY);
            mLoadDailyNoteData(NOTES_MAX_QUANTITY);
            mLoadNoteFormPosition(NOTES_MAX_QUANTITY);


            // settings global 
            // GlobalTaskNb

            // setting possitions of this form
            this.Left = ThisFormPosition.fpGetXPos();
            this.Top = ThisFormPosition.fpGetYPos();
            
            // init forms
            InitializeComponent();
            mTaskerFormsInit(TASKER_MAX_QUANTITY);
            mNoteFormsInit(NOTES_MAX_QUANTITY);

        }


        # region Setting layout of this form like colors of buttons  

        private void RefreshLayout()
        {
            if (NotesAreVisible == true)
            {
                btnCloseAndShowNotes.Opacity = 1;
            }

            if (NotesAreVisible == false)
            {
                btnCloseAndShowNotes.Opacity = 0.4;
            }

            if (TasksAreVisible == true)
            {
                btnCloseAndShowTasks.Opacity = 1;
            }

            if (TasksAreVisible == false)
            {
                btnCloseAndShowTasks.Opacity = 0.4;
            }


        }

        #endregion

        # region Close Or Show Notes = DailyNote

        private void CloseOrShowNotes()
        {
            if (NotesAreVisible == true)
            {
                for (int i = 0; i < NOTES_MAX_QUANTITY; i++)
                {
                    if (Note[i].IsNoteActive() == true)
                    {
                     // MessageBox.Show(" zamykam okno nr i: " + i);
                        formNote[i].Close();
                    }
                }
                NotesAreVisible = false;
              // MessageBox.Show(" Status NotesAreVisiible :" + NotesAreVisible);
            }
            else
            {
                for (int i = 0; i < NOTES_MAX_QUANTITY; i++)
                {
                    if (Note[i].IsNoteActive() == true)
                    {
                  //      MessageBox.Show(" note jest aktywny: " + Note[i].IsNoteActive() + " iteracja i: " + i);
                        formNote[i] = new Note(Note[i], i, this);
                        formNote[i].Show();
                    }

                }
                NotesAreVisible = true;
             // MessageBox.Show(" Status NotesAreVisiible :" + NotesAreVisible);
            }
            RefreshLayout(); 
        }

        # endregion


        # region Close Or Show Tasks = DailyTask

        private void CloseOrShowTasks()
        {
            if (TasksAreVisible == true)
            {
                for (int i = 0; i < TASKER_MAX_QUANTITY ; i++)
                {
                    if (Tasker[i].tGetMainTaskIsActive() == true)
                    {
                        // MessageBox.Show(" zamykam okno nr i: " + i);
                        formTasker[i].Close();
                    }
                }
                TasksAreVisible = false;
                // MessageBox.Show(" Status NotesAreVisiible :" + NotesAreVisible);
            }
            else
            {
                for (int i = 0; i < TASKER_MAX_QUANTITY; i++)
                {
                    if (Tasker[i].tGetMainTaskIsActive() == true)
                    {
                        // MessageBox.Show(" note jest aktywny: " + Note[i].IsNoteActive() + " iteracja i: " + i);
                        formTasker[i] = new Tasker(i, this);
                        formTasker[i].Show();
                    }

                }
                TasksAreVisible = true;
                // MessageBox.Show(" Status TasksAreVisiible :" + NotesAreVisible);
            }
            RefreshLayout();
        }

        # endregion



        # region Buttons - DailyNote

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            if (NotesAreVisible == false) CloseOrShowNotes();
            GlobalNoteCounter = mGetNextFreeSpaceForNote();
            formNote[GlobalNoteCounter] = new Note(Note[GlobalNoteCounter], GlobalNoteCounter, this);
            formNote[GlobalNoteCounter].Show();
            Note[GlobalNoteCounter].SetIfNoteActive(true);
        }


        private void btnCloseAndShowNotes_Click(object sender, RoutedEventArgs e)
        {
            CloseOrShowNotes();
        }

        #endregion

        #region Buttons - DailyTasker


        private void btnCloseAndShowTasks_Click(object sender, RoutedEventArgs e)
        {
            CloseOrShowTasks();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mSaveThisFormPosition();
            mSaveTAskerFormPosition(TASKER_MAX_QUANTITY);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
            Environment.Exit(0); // closing all window forms
            this.Close();
        }


        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksAreVisible == false) CloseOrShowTasks();
            GlobalTaskCounter = mGetNextFreeSpaceForNewMainTask();
            formTasker[GlobalTaskCounter] = new Tasker(GlobalTaskCounter, this);
            formTasker[GlobalTaskCounter].Show();
            Tasker[GlobalTaskCounter].tSetMainTaskIsActive(true);

        }
/*
        private void btnCloseTasks_Click(object sender, RoutedEventArgs e)
        {
            // TODO : Add procedure to close open windows with tasks - see closing notes !
        }
        */

/*
        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Description Task nb 0: " + Tasker[0].tGetMainTask());
            MessageBox.Show(" Partial  0: " + Tasker[0].tGetPartialTask(0));

            MessageBox.Show(" Description Task nb 1: " + Tasker[1].tGetMainTask());
            MessageBox.Show(" Partial  0: " + Tasker[0].tGetPartialTask(0));

            MessageBox.Show(" Description Task nb 2: " + Tasker[2].tGetMainTask());
            MessageBox.Show(" Partial  0: " + Tasker[0].tGetPartialTask(0));
        }

 */
        #endregion
 
        #region Recieving data from Note form - DailyNote

        // udpate all DailyNote object at once
        public void mSetNoteObjectInMainWindow(int noteNb, DailyNote NoteToTransfer)
        {
            Note[noteNb] = NoteToTransfer;
            // autoSave metod to save all Note objects to file
            mSaveDailyNoteData(NOTES_MAX_QUANTITY);
        }

        public void mSetNoteInMainWindow(string note, bool isActive, int noteNb)
        {
            Note[noteNb].SetNote(note);
            Note[noteNb].SetIfNoteActive(isActive);
            // autoSave metod to save all Note objects to file
            mSaveDailyNoteData(NOTES_MAX_QUANTITY);
        }

        public void mSendFormPosToNoteClass(int nNb, double X, double Y)
        {
            NoteFormPosition[nNb].fpSetXPos(X);
            NoteFormPosition[nNb].fpSetYPos(Y);
            mSaveNoteFormPosition(TASKER_MAX_QUANTITY);
        }

        #endregion

        #region Recieving data from Tasker form - DailyTasker

        // *************************************************
        // ****** recieving data from Tasker form **********
        // *************************************************

        // ** Possition of task form on screen
        public void mSendFormPosToTaskerClass (int tNb, double X, double Y)
        {
            TaskerFormPosition[tNb].fpSetXPos(X);
            TaskerFormPosition[tNb].fpSetYPos(Y);
            mSaveTAskerFormPosition(TASKER_MAX_QUANTITY);
        }

        // MainTaskData after Implementation at TaskerEditor
        public void mSetMainTask (string Task, int tNb)
        {
            Tasker[tNb].tSetMainTask(Task);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        // MainTaskData DeadLine after Implementation at TaskerEditor
        public void mSetMainTaskDeadLine (string DL, int tNb)
        {
            Tasker[tNb].tSetMainTaskDeadLine(DL);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        // MainTaskData DeadLine after Implementation at TaskerEditor
        public void mSetMainTaskIsActive(bool isActive, int tNb)
        {
            Tasker[tNb].tSetMainTaskIsActive(isActive);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetMainTaskIsFinished(bool isFinished, int tNb)
        {
            Tasker[tNb].tSetMainTaskIsFinished(isFinished);
           if (isFinished==true) Tasker[tNb].tResetThisMainTaskAndItsPartials();
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }



        // partial tasks

        public void mSetPartialTaskFromTasker(string pTask,int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTask(pTask, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetPartialTaskDescriptionFromTasker(string pTaskDscp, int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTaskDescription(pTaskDscp, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetPartialTaskDeadLineFromTasker(string pTDL, int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTaskDeadLine(pTDL, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetPartialTaskProgressFromTasker(int progress, int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTaskProgress(progress, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetPartialTaskIsActiveFromTasker(bool isActv, int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTaskIsActive(isActv, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        public void mSetPartialTaskIsFinishedFromTasker(bool isfinished, int tNb, int ptNb)
        {
            Tasker[tNb].tSetPartialTaskIsFinished(isfinished, ptNb);
            mSaveDailyTaskerData(TASKER_MAX_QUANTITY);
        }

        #endregion

        #region Recieving data from DailyReminder

        public void mSetNewDailyReminder(string shortDescription, string description, string date)
        {
            DailyReminder dR = new DailyReminder();
            dR.SetDescription(shortDescription);
            dR.SetDescription(description);
            dR.SetActivationDate(date);
            // TODO : przetransfrowac zadanie do listy
        }




        #endregion



        #region Sending data to Note form - DailyNote

        // *****************************************************************
        // ******* Sending data with form possition to Tasker form *********
        // *****************************************************************

        // send form possition X
        public double mGetNoteFormPosX(int nNb)
        {
            return (NoteFormPosition[nNb].fpGetXPos());
        }

        // send form possition Y
        public double mGetNoteFormPosY(int nNb)
        {
            return (NoteFormPosition[nNb].fpGetYPos());
        }
        #endregion

        #region Sending data to Tasker form - DailyTasker

        // *****************************************************************
        // ******* Sending data with form possition to Tasker form *********
        // *****************************************************************

        // send form possition X
        public double mGetTaskerFormPosX(int tNb)
        {
            return (TaskerFormPosition[tNb].fpGetXPos());
        }

        // send form possition Y
        public double mGetTaskerFormPosY(int tNb)
        {
            return (TaskerFormPosition[tNb].fpGetYPos());
        }


        // *****************************************************************
        // ******* Sending data from class to Tasker form **********
        // *****************************************************************

       
        // main task

        public string mGetMainTask(int tNb)
        {
            return Tasker[tNb].tGetMainTask();
        }

        public string mGetMainTaskDeadLine(int tNb)
        {
            return Tasker[tNb].tGetMainTaskDeadLine();
        }

        public bool mGetMainTaskActive(int tNb)
        {
            return Tasker[tNb].tGetMainTaskIsActive();
        }



        // partial tasks

        public string mGetPartialTask(int tNb, int partialNb)
        {
            
        return Tasker[tNb].tGetPartialTask(partialNb);
        }

        public string mGetPartialTaskDescription (int tNb, int partialNb)
        {
            return (Tasker[tNb].tGetPartialTaskDescription(partialNb));
        }

        public string mGetPartialTaskDeadLine(int tNb, int partialNb)
        {
            
            return Tasker[tNb].tGetPartialTaskDeadLine(partialNb);
        }

        public int mGetPartialTaskProgress(int tNb, int partialNb)
        {
            return Tasker[tNb].tGetPartialTaskProgress(partialNb);
        }

          public bool mGetPartialTaskIsActive(int tNb, int partialNb)
        {
            
            return Tasker[tNb].tGetPartialTaskIsActive(partialNb);
        }

          public bool mGetPartialTaskIsFinished(int tNb, int partialNb)
        {
            
            return Tasker[tNb].tGetPartialTaskIsFinished(partialNb);
        }

        #endregion


        #region Objects inits - DailyNote
         
        // initialize objects responsible for notes
          void mNoteObjectInitializer(int maxObjects)
          {
              for (int i = 0; i < maxObjects; i++)
              {
                  Note[i] = new DailyNote();
                  Note[i].SetNote("");
                  Note[i].SetIfNoteActive(false);
                  Note[i].SetWindowNoteColor(0);
              }
          }


          // init objects responsible for Note form positions
          void mNotePositionInitializer(int maxObjects)
          {
              for (int i = 0; i < maxObjects; i++)
              {
                  NoteFormPosition[i] = new FormPosition();
              }

          }

          #endregion

        #region Objects inits - DailyTasker
// ********************************
// Objects and forms initialisation 
// ********************************


        // ************************************
        // ****** Objects initialisation ******
        // ************************************

          void mNoteFormsInit(int quantity)
          {
              for (int i = 0; i < quantity; i++)
              {
                  if (Note[i].IsNoteActive())
                  {
                      formNote[i] = new Note(Note[i], i, this);
                      formNote[i].Show();
                      //      MessageBox.Show(" Note form init: " + i);
                  }

              }
          }

        // initialize objects responsible for tasks
        void mTaskerObjectInitializer(int maxObjects)
        {
            for (int i=0;i<maxObjects;i++)
            {
                Tasker[i] = new DailyTasker();

                     // init of string array in class
                     for (int k=0; k<PARTIAL_TASKS_MAX_QUANTITY; k++)
                      {
                            Tasker[i].tSetPartialTask("",k);
                            Tasker[i].tSetPartialTaskDescription("", k);
                            Tasker[i].tSetPartialTaskDeadLine("", k);
                            Tasker[i].tSetPartialTaskProgress(0, k);
                            Tasker[i].tSetPartialTaskIsActive(false, k);
                            Tasker[i].tSetPartialTaskIsFinished(false, k);
                      }
            }
        }
       

        // init objects responsible for Tasker form positions
         void mTaskerPositionInitializer(int maxObjects)
        {
             for(int i = 0; i < maxObjects; i++)
             {
                 TaskerFormPosition[i] = new FormPosition();
             }
            
        }


         // ******************************************
         // ****** Forms Objects initialisation ******
         // ******************************************

        // init Tasker Forms 
        void mTaskerFormsInit(int quantity)
         {
            for (int i = 0; i < quantity; i++ )
            {
                if (Tasker[i].tGetMainTaskIsActive())
                {
                formTasker[i] = new Tasker(i,this);
                formTasker[i].Show();
             //   MessageBox.Show(" tasker form init: " + i);
                }
            }
            

         }

          #endregion

        #region Moving this form

        // ******************************
        // ****** Moving this form ******
        // ******************************

        // Moving This form by mouse    
        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

            ThisFormPosition.fpSetXPos(this.Left);
            ThisFormPosition.fpSetYPos(this.Top);        
        }

        #endregion

        #region IO Operation -> Note form possition / DailyNote data

        // *****************************************
        // ****** IO DAILY NOTE CLASS ************
        // *****************************************
        
        
        void mLoadDailyNoteData(int maxNotes)
        {
            IOadapt.LoadDailyNoteData(maxNotes, ref Note);    
        }

        void mSaveDailyNoteData(int maxNotes)
        {
            IOadapt.SaveDailyNote(maxNotes, ref Note);
        }


        // *************************************
        // ****** IO Note Form Possition *******
        // *************************************

        
        void mLoadNoteFormPosition(int maxTaskers)
        {
            IOadapt.LoadDailyNoteWindowPosition(maxTaskers, ref NoteFormPosition);          
        }
        
        void mSaveNoteFormPosition(int maxTaskers)
        {
            IOadapt.SaveDailyNoteWindowPosition(ref NoteFormPosition);                      
        }



        #endregion

        #region IO Operation -> IO Load & Save this form possition / tasker form possition / DailyTasker data / DailyReminder data

        // transfered to IOAdapter
        void mLoadThisFormPosition()
        {
            IOadapt.LoadMainWindowPosition(ref ThisFormPosition);         
        }

        // transfered to IOAdapter
        void mSaveThisFormPosition()
        {
            IOadapt.SaveMainWindowPosition(ref ThisFormPosition);           
        }

        // transfered to IOAdapter
        void mLoadTaskerFormPosition(int maxTaskers)
        {
            IOadapt.LoadDailyTaskerWindowPosition(maxTaskers, ref TaskerFormPosition);                   
        }

        // transfered to IOAdapter
        void mSaveTAskerFormPosition(int maxTaskers)
        {
            IOadapt.SaveDailyTaskerWindowPosition(ref TaskerFormPosition);          
        }

        // transfered to IOAdapter
        void mLoadDailyTaskerData(int maxTaskers)
        {
            IOadapt.LoadDailyTaskerData(maxTaskers, ref Tasker);
        }

        // transfered to IOAdapter
        void mSaveDailyTaskerData(int maxTaskers)
        {
            IOadapt.SaveDailyTaskerData(ref Tasker);         
        }

        void mLoadDailyReminderData()
        {
            // TODO : Write code to load data
        }

        void mSaveDailyreminderData()
        {
            // TODO : Write code to write data
        }

        #endregion

        public int mGetNextFreeSpaceForNewMainTask()
        {
            int nextNumber = 666;

            for (int i = 0; i < TASKER_MAX_QUANTITY; i++ )
            {
                if (Tasker[i].tGetMainTaskIsActive()==false) return (i);
            }
                return nextNumber;
        }


        public int mGetNextFreeSpaceForNote()
        {
            int nextNumber = 666;

            for (int i = 0; i < NOTES_MAX_QUANTITY; i++)
            {
                if (Note[i].IsNoteActive() == false) return (i);
            }
            return nextNumber;
        }


 

    }
}
