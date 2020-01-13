namespace DailyPlanner
{
    public class DailyNote
    {

      // note 
      private bool NoteActive = false; 
      private string Note;
      
      // note window attributes
      public int NoteWindowColor { get; private set; } // 0 - green, 1 - yellow, 2 - blue, 3 - orange 
      public double NoteFontSize { get; private set; }
      public int NoteWindowSizeWidth { get; private set; }
      public int NoteWindowSizeLength { get; private set; }

        #region SETTERs

          public void SetNote (string note) 
    {
        this.Note = note;
    }

          public void SetIfNoteActive(bool isNoteActive)
    {
        this.NoteActive = isNoteActive;
    }

          public void SetWindowNoteColor(int color)
    {
        this.NoteWindowColor = color;
    }

          public void SetNoteFontSize(double fontSize)
    {
        this.NoteFontSize = fontSize;
    }

            public void SetNoteWindowWidth(int width)
            {
                this.NoteWindowSizeWidth = width;
            }

            public void SetNoteWindowLength(int length)
            {
                this.NoteWindowSizeLength = length;
            }

        #endregion


        #region GETTERs

        public string GetNote()
        {
             return this.Note;
        }

        public int GetFormNoteColor()
        {
             return this.NoteWindowColor;
        }

        public bool IsNoteActive()
        {
             return (this.NoteActive);
        }

        #endregion


    }

}
