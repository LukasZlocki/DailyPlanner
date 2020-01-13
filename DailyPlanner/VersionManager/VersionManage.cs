using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

/// <summary>
/// VERSIONS DESCRIPTION :
/// 1.5 -> Standard version 2017
/// 1.6 -> 2018 version with notes fonts increment / decrement,  note colors, window width/length
/// </summary>

// LOGIC :
// Step I : 
// check if update needed - by reading file with version and comparing it to BUILD_IN_VERSION
// Btw, if there is no file - no update needed 
// Step II (if update needed) :
// load the data according to lowest update
// Step III :
// load data to class , new fields at NOTE clase  fill up with default settings
// Step IV :
// Check if further update to further version is needed
// if yes -> see Step II
// if not -> save data to file


namespace DailyPlanner.VersionManager
{



    class VersionManage
    {

        private static double BUILD_IN_VERSION = 1.6;

        private double VersionToUpdate;
        private bool IsUpdateNeeded = false;


        public void CheckVersion(int maxNotesQuantity)
        {
            bool _neededUpdate = false;

            DailyNote[] Note = new DailyNote[maxNotesQuantity];
            for (int i = 0; i < maxNotesQuantity; i++)
            {
                Note[i] = new DailyNote();
            }

            
            do
            {
                // checking if update is needed
                _neededUpdate = CheckVersion(ref VersionToUpdate);


                if (_neededUpdate == true)
                {
                    LoadNoteTaskData(maxNotesQuantity, ref Note, VersionToUpdate);
                    
                    // Version updated 
                    if (VersionToUpdate == 1.5) { VersionToUpdate = 1.6; }
                    SaveNoteTaskData(maxNotesQuantity, Note, VersionToUpdate);
                    SaveVersionFile(VersionToUpdate);
                }

                _neededUpdate = CheckVersion(ref VersionToUpdate);

            } while (_neededUpdate);



        }

        #region Checking Version

        private bool CheckVersion(ref double _verToUpdate)
        {
            double _version = 0;

            LoadVersionFile(ref _version);

            if (_version == BUILD_IN_VERSION) return false;
            else
            {
                _verToUpdate = _version;
                return true;
            }
        }

        #endregion

        #region IO - Version

        private void LoadVersionFile(ref double version)
        {
            StreamReader sr;

            try
            {
                sr = new StreamReader(new FileStream("Version.txt", FileMode.Open));
            }

            catch (IOException e)
            {
                // MessageBox.Show("Cannot open file");
                // no file means buils in version installed
                version = BUILD_IN_VERSION;
                return;
            }

            try
            {
                version = Convert.ToDouble(sr.ReadLine());
            }
            catch (IOException e)
            {
                MessageBox.Show("Cannot read from file");
                // no file means buils in version installed
                version = BUILD_IN_VERSION;
                return;
            }
            sr.Close();

        }

        private void SaveVersionFile(double version)
        {

            StreamWriter sw;

            try
            {
                sw = new StreamWriter(new FileStream("Version.txt", FileMode.Create));
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot creat file");
                return;
            }

            try
            {           
               sw.WriteLine(Convert.ToString(version));                
            }

            catch (IOException e)
            {
                MessageBox.Show("Cannot write to file");
                return;
            }
            sw.Close();
        }

        #endregion

        #region IO - NOTE / TASKER

        private void LoadNoteTaskData(int maxNotes, ref DailyNote[] _note, double _version)
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
                    if (VersionToUpdate == 1.5)
                    {
                        _note[i].SetIfNoteActive(br.ReadBoolean());
                        _note[i].SetNote(br.ReadString());
                        _note[i].SetWindowNoteColor(br.ReadInt32());
                    }

                    if (VersionToUpdate == 1.6)
                    {
                        _note[i].SetIfNoteActive(br.ReadBoolean());
                        _note[i].SetNote(br.ReadString());
                        _note[i].SetWindowNoteColor(br.ReadInt32());
                        _note[i].SetNoteFontSize(br.ReadDouble());
                        _note[i].SetNoteWindowWidth(br.ReadInt32());
                        _note[i].SetNoteWindowLength(br.ReadInt32());
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

        private void SaveNoteTaskData(int maxNotes, DailyNote[] _note, double _version)
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
                        if (_version == 1.6)
                        {
                            bw.Write(_note[i].IsNoteActive());
                            bw.Write(_note[i].GetNote());
                            bw.Write(_note[i].NoteWindowColor);
                            bw.Write(_note[i].NoteFontSize);
                            bw.Write(_note[i].NoteWindowSizeWidth);
                            bw.Write(_note[i].NoteWindowSizeLength);
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

        #endregion

    }
}