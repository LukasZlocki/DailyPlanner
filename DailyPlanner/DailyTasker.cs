using System;

namespace DailyPlanner
{
    class DailyTasker
    {

        // quantity of Partial Tasks
        public static int tPartialTaskQuantity = 10;



        // MainTask
        private string tMainTask = "";
        private string tMainTaskDeadLine ="";
        private bool tMainTaskIsActive = false;
        private bool tMainTaskIsFinished = false;
        
      
        // PartialTask - string arrays are init at MainWindow form
        private string[] tPartialTask = new string[tPartialTaskQuantity];
        private string[] tPartialTaskDescription = new string[tPartialTaskQuantity];
        private string [] tPartialTaskDeadLine = new string [tPartialTaskQuantity];
        private int[] tpartialTaskProgress = new int [tPartialTaskQuantity];  
        private bool[] tPartialTaskIsActive = new bool[tPartialTaskQuantity];
        private bool [] tPartialTaskIsFinished = new bool [tPartialTaskQuantity];




        #region MainTask - setting


        public void tSetMainTask(string mainTask)
        {
            tMainTask = mainTask;
        }

        public void tSetMainTaskDeadLine(string deadLine)
        {
            tMainTaskDeadLine = deadLine;
        }

        public void tSetMainTaskIsActive(bool isActive)
        {
            tMainTaskIsActive = isActive;
        }

        public void tSetMainTaskIsFinished(bool isFinished)
        {
            tMainTaskIsFinished = isFinished;
        }


        #endregion

        #region MainTask - getting


       public string tGetMainTask()
        {
            return (tMainTask);
        }

        public string tGetMainTaskDeadLine()
        {
            return(tMainTaskDeadLine);
        }

        public bool tGetMainTaskIsActive()
        {
            return(tMainTaskIsActive);
        }

        public bool tGetMainTaskIsFinished()
        {
            return(tMainTaskIsFinished);
        }


        #endregion

        #region PartialTask - setting


        public void tSetPartialTask(string partialTask, int ptNb)
        {
            tPartialTask[ptNb] = partialTask;
        }

        public void tSetPartialTaskDescription(string partialTaskDescritpion, int ptNb)
        {
            tPartialTaskDescription[ptNb] = partialTaskDescritpion;
        }

        public void tSetPartialTaskDeadLine(string partialDeadLine, int ptNb)
        {
            tPartialTaskDeadLine[ptNb] = partialDeadLine;
        }

        public void tSetPartialTaskProgress(int progress, int ptNb)
        {
            tpartialTaskProgress[ptNb] = progress;
        }

        public void tSetPartialTaskIsActive(bool isActive, int ptNb)
        {
            tPartialTaskIsActive[ptNb] = isActive;
        }

        public void tSetPartialTaskIsFinished(bool isFinished, int ptNb)
        {
            tPartialTaskIsFinished[ptNb] = isFinished;
        }


        #endregion

        #region PartialTask - getting

        public string tGetPartialTask(int ptNb)
        {
            return (tPartialTask[ptNb]);
        }

        public string tGetPartialTaskDescription(int ptNb)
        {
            return (tPartialTaskDescription[ptNb]);
        }

        public string tGetPartialTaskDeadLine(int ptNb)
        {
            return (tPartialTaskDeadLine[ptNb]);
        }

        public int tGetPartialTaskProgress(int ptNb)
        {
            return (tpartialTaskProgress[ptNb]);
        }

        public bool tGetPartialTaskIsActive(int ptNb)
        {
            return (tPartialTaskIsActive[ptNb]);
        }

        public bool tGetPartialTaskIsFinished(int ptNb)
        {
            return (tPartialTaskIsFinished[ptNb]);
        }


        // getting partial tasks quantity
        public int tGetPartialTaskQuantity()
        {
            return (tPartialTaskQuantity);
        }

        #endregion

        #region Timecalculation

        public string tShowCounter(string dl) // show how many days left or delated
        {
            if (!(dl == ""))
            {
                var dataNow = DateTime.Now;
                TimeSpan difference = tgetDateDifference(dataNow, Convert.ToDateTime(dl));
                return difference.Days.ToString();
            }
            else
            {
                return dl;
            }
        }


        TimeSpan tgetDateDifference(DateTime todaydate, DateTime deadL) // Calculate difference between today and dead line (in days)
        {
            TimeSpan ts = deadL - todaydate;
            int differenceInDays = ts.Days;
            string differenceAsString = differenceInDays.ToString();
            return ts;
        }

        #endregion



        // ******************* Other methods *****************



        // reset of all data in this Main Task
        public void tResetThisMainTaskAndItsPartials()
        {
            tMainTask = "";
            tMainTaskDeadLine = "";
            tMainTaskIsActive = false;
            tMainTaskIsFinished = false;
            for (int i = 0; i < tPartialTaskQuantity; i++)
            {
                tPartialTask[i] = "";
                tPartialTaskDescription[i] = "";
                tPartialTaskDeadLine[i]="";
                tpartialTaskProgress[i]=0;
                tPartialTaskIsActive[i]=false;
                tPartialTaskIsFinished[i] = false;
            }
        }




        // checking if MainTask can be finished - partial tasks finished
        public bool tIsMainTaskCanBeFinished()
        {
            bool status=false;
            int quantityActive=0;
            int quantityFinished=0;


            for (int i=0; i<tPartialTaskQuantity; i++)
            {
                if (tPartialTaskIsActive[i] == true) quantityActive++;
                if (tPartialTaskIsFinished[i] == true) quantityFinished++;
            }

            if (quantityActive - quantityFinished == 0) return (status = true);
         
                return status=false;
        }


    }
}
