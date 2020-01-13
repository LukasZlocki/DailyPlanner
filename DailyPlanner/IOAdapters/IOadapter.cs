using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

namespace DailyPlanner.IOAdapters
{
    class IOadapter
    {
        private static int PARTIAL_TASKS_MAX_QUANTITY = 10;


       // DailyNote Note;
       // DailyTasker Tasker;
       
      //  FormPosition NoteFormPosition;
      //  FormPosition MainWindowPosition;
       // FormPosition TaskerWindowPosition;


        #region IO Daily Note

        public void LoadDailyNoteData(int maxNotes, ref DailyNote [] _note)
        {
             BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("DailyNote.dat", FileMode.Open));
            }

            catch (IOException e)
            {
               // MessageBox.Show("Cannot open file");
                return;
            }

            try
            {
                for (int i = 0; i < maxNotes; i++)
                {
                    _note[i].SetIfNoteActive(br.ReadBoolean());
                    _note[i].SetNote(br.ReadString());
                    _note[i].SetWindowNoteColor(br.ReadInt32());
                    _note[i].SetNoteFontSize(br.ReadDouble());
                    _note[i].SetNoteWindowWidth(br.ReadInt32());
                    _note[i].SetNoteWindowLength(br.ReadInt32());
                }
            }   
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close();
        }
        public void LoadDailyNoteWindowPosition(int maxTaskers, ref FormPosition [] _noteFormPosition)
        {
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("NoteFormPosition.dat", FileMode.Open));
            }

            catch (IOException e)
            {
                //  MessageBox.Show("Cannot open file");
                return;
            }

            try
            {
                for (int i = 0; i < maxTaskers; i++)
                {
                    _noteFormPosition[i].fpSetXPos(br.ReadDouble());
                    _noteFormPosition[i].fpSetYPos(br.ReadDouble());

                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close();
        }

        public void SaveDailyNote(int maxNotes, ref DailyNote[] _note)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("DailyNote.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                for (int i = 0; i < maxNotes; i++)
                {
                    bw.Write(_note[i].IsNoteActive());
                    bw.Write(_note[i].GetNote());
                    bw.Write(_note[i].NoteWindowColor);
                    bw.Write(_note[i].NoteFontSize);
                    bw.Write(_note[i].NoteWindowSizeWidth);
                    bw.Write(_note[i].NoteWindowSizeLength);
                }
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }
        public void SaveDailyNoteWindowPosition(ref FormPosition[] _noteFormPosition)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("NoteFormPosition.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                for (int i = 0; i < _noteFormPosition.Length; i++)
                {
                    bw.Write(_noteFormPosition[i].fpGetXPos());
                    bw.Write(_noteFormPosition[i].fpGetYPos());
                }
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }

        #endregion


        #region IO Daily Tasker
       
        public void LoadDailyTaskerData(int maxTaskers, ref DailyTasker [] _dailyTasker)
        {
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("DailyTasker.dat", FileMode.Open));
            }

            catch (IOException e)
            {
                //  MessageBox.Show("Cannot open file");
                return;
            }

            try
            {
                for (int i = 0; i < maxTaskers; i++)
                {
                    _dailyTasker[i].tSetMainTask(br.ReadString());
                    _dailyTasker[i].tSetMainTaskDeadLine(br.ReadString());
                    _dailyTasker[i].tSetMainTaskIsActive(br.ReadBoolean());
                    _dailyTasker[i].tSetMainTaskIsFinished(br.ReadBoolean());

                    for (int k = 0; k < PARTIAL_TASKS_MAX_QUANTITY; k++)
                    {
                        _dailyTasker[i].tSetPartialTask(br.ReadString(), k);
                        _dailyTasker[i].tSetPartialTaskDescription(br.ReadString(), k);
                        _dailyTasker[i].tSetPartialTaskDeadLine(br.ReadString(), k);
                        _dailyTasker[i].tSetPartialTaskProgress(br.ReadInt32(), k);
                        _dailyTasker[i].tSetPartialTaskIsActive(br.ReadBoolean(), k);
                        _dailyTasker[i].tSetPartialTaskIsFinished(br.ReadBoolean(), k);
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close(); 
        }
        public void LoadDailyTaskerWindowPosition(int maxTaskers, ref FormPosition [] _dailyTaskerFormPosition)
        {
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("TaskerFormPosition.dat", FileMode.Open));
            }

            catch (IOException e)
            {
                //  MessageBox.Show("Cannot open file");
                return;
            }

            try
            {
                for (int i = 0; i < maxTaskers; i++)
                {
                    _dailyTaskerFormPosition[i].fpSetXPos(br.ReadDouble());
                    _dailyTaskerFormPosition[i].fpSetYPos(br.ReadDouble());

                    //    MessageBox.Show(" load X: " + TaskerFormPosition[i].fpGetXPos() + " Load Y: " + TaskerFormPosition[i].fpGetYPos());

                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close();
        }

        public void SaveDailyTaskerData(ref DailyTasker [] _dailyTasker)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("DailyTasker.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                for (int i = 0; i < _dailyTasker.Length; i++)
                {

                    // tutaj wpisac kod zapisu calej klasy do pliku

                    bw.Write(_dailyTasker[i].tGetMainTask());
                    bw.Write(_dailyTasker[i].tGetMainTaskDeadLine());
                    bw.Write(_dailyTasker[i].tGetMainTaskIsActive());
                    bw.Write(_dailyTasker[i].tGetMainTaskIsFinished());

                    for (int k = 0; k < PARTIAL_TASKS_MAX_QUANTITY; k++)
                    {
                        bw.Write(_dailyTasker[i].tGetPartialTask(k));
                        bw.Write(_dailyTasker[i].tGetPartialTaskDescription(k));
                        bw.Write(_dailyTasker[i].tGetPartialTaskDeadLine(k));
                        bw.Write(_dailyTasker[i].tGetPartialTaskProgress(k));
                        bw.Write(_dailyTasker[i].tGetPartialTaskIsActive(k));
                        bw.Write(_dailyTasker[i].tGetPartialTaskIsFinished(k));
                    }

                }
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }
        public void SaveDailyTaskerWindowPosition(ref FormPosition[] _dailyTaskerFormPosition)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("TaskerFormPosition.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                for (int i = 0; i < _dailyTaskerFormPosition.Length; i++)
                {
                    bw.Write(_dailyTaskerFormPosition[i].fpGetXPos());
                    bw.Write(_dailyTaskerFormPosition[i].fpGetYPos());                 
                }
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }

        #endregion


        #region IO Main Window 

        public void LoadMainWindowPosition(ref FormPosition _mainWindowFormPosition)
        {
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("MainFormPosition.dat", FileMode.Open));
            }

            catch (IOException e)
            {
                // MessageBox.Show("Cannot read from file");
                return;
            }

            try
            {
                _mainWindowFormPosition.fpSetXPos(br.ReadDouble());
                _mainWindowFormPosition.fpSetYPos(br.ReadDouble());

            }
            catch (IOException e)
            {
                //  MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close();
        }

        public void SaveMainWindowPosition(ref FormPosition _mainWindowFormPosition)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("MainFormPosition.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                bw.Write(_mainWindowFormPosition.fpGetXPos());
                bw.Write(_mainWindowFormPosition.fpGetYPos());
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }

        #endregion


        #region IO Reminder

        public void LoadDailyReminderData(ref List<DailyReminder> ReminderListToLoad)
        {
            ReminderListToLoad = new List<DailyReminder>();
            BinaryReader br;

            try
            {
                br = new BinaryReader(new FileStream("DailyReminder.dat", FileMode.Open));
            }

            catch (IOException e)
            {
                // MessageBox.Show("Cannot open file");
                return;
            }

            try
            {

                // TODO : Tu ladujemy zatem nie for each ale jakies add to list !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
              
            }
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                return;
            }
            br.Close();
        }

        public void SaveDailyReminder(List<DailyReminder> ReminderListToSave)
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("DailyReminder.dat", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {
                foreach (var rem in ReminderListToSave)
                {
                    bw.Write(rem.ShortDescription);
                    bw.Write(rem.Description);
                    bw.Write(rem.ActivationDate);
                }
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            bw.Close();
        }


        #endregion



    }
}
