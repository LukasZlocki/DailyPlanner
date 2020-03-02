using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DailyPlanner.postIt
{

    // Additional features
    // [14102018] -> colors of Notes added
    // [14102018] -> Size of text added




    public partial class Note : Window
    {
        private double _FONT_INCREMENT = 1;
        private int _DEFAULT_FONT_SIZE = 16;

        /* part for resizing windows by user
        private bool drag = false;
        private Point startPt;
        private int wid;
        private int hei;
        private Point lastLoc;
        private double CanvasLeft, CanvasTop;
        */

        private int GlobalNoteNb;
     // private int GlobalNoteColor;

        

      DailyNote ThisNote = new DailyNote();
      MainWindow communicatorWithMainWindow;
      
      public Note(DailyNote Note, int noteNb, MainWindow communicatorWithMainWindow)
        {
            this.communicatorWithMainWindow = communicatorWithMainWindow;
          
            ThisNote = Note;
            ThisNote.SetIfNoteActive(true);
            GlobalNoteNb = noteNb;
            nSettingFormPosition(noteNb);

            InitializeComponent();
            
            RefreshScreen(ThisNote);
                 
        }

        #region Instant Saving Data Method

        private void InstantSaveText(object sender, KeyEventArgs e)
        {
            ThisNote.SetNote(txtNote.Text);
            ThisNote.SetIfNoteActive(true);
            // global update of DailyNote at MainWindow
            UpdateNoteDataAtMainWindow(ThisNote);

          //  communicatorWithMainWindow.mSetNoteInMainWindow(ThisNote.GetNote(), true, GlobalNoteNb);
        }

        #endregion

        #region Buttons


        #region Buttons - window color

        private void btnColorGreen_Click(object sender, RoutedEventArgs e)
        {
            SetColorOfNoterectangle(0);
            RefreshScreen(ThisNote);
        }
        private void btnColorYellow_Click(object sender, RoutedEventArgs e)
        {
            SetColorOfNoterectangle(1);
            RefreshScreen(ThisNote);
        }
        private void btnColorBlue_Click(object sender, RoutedEventArgs e)
        {
            SetColorOfNoterectangle(2);
            RefreshScreen(ThisNote);
        }
        private void btnColorOrange_Click(object sender, RoutedEventArgs e)
        {
            SetColorOfNoterectangle(3);
            RefreshScreen(ThisNote);
        }

        #endregion


        /* BUTTONS TO CHANGE COLOR BY SEPARATE WINDOW - implement if all problems are solved with additional window possition 
        private void btnColorChange_Click(object sender, RoutedEventArgs e)
        {
            // TODO : open window with color button near color function button

            Point position = btnColor.PointToScreen(new Point(0d, 0d)),
                controlPosition = this.PointToScreen(new Point(0d, 0d));

            position.X -= controlPosition.X;
            position.Y -= controlPosition.Y;


            double _windowStartingPoint = position.X;

            Point point = Mouse.GetPosition(this);


            NoteColorSelection noteColorWindow = new NoteColorSelection();
            noteColorWindow.Left = point.X;

            noteColorWindow.Top = point.Y;
            noteColorWindow.Show();
        }
        */

        private void btnCloseNote_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Do you want to delete and close this note","Dailyplanner - Message", MessageBoxButton.YesNo);
           if (result == MessageBoxResult.Yes)
            {
                ThisNote.SetNote("");
                communicatorWithMainWindow.mSetNoteInMainWindow("", false, GlobalNoteNb);
                this.Close();
            }
        }

   

        #region Letter Size Buttons

        private void btnLetterUp_Click(object sender, RoutedEventArgs e)
        {
            SetFontSizeOfNote("UP");
            RefreshScreen(ThisNote);
        }

        private void btnLetterDown_Click(object sender, RoutedEventArgs e)
        {
            SetFontSizeOfNote("DOWN");
            RefreshScreen(ThisNote);
        }

        #endregion

        #endregion

        #region Moving this form

        private void RectangleTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

            communicatorWithMainWindow.mSendFormPosToNoteClass(GlobalNoteNb, this.Left, this.Top);
        }
 
        #endregion

        #region Screen refresh
        
        private void RefreshScreen(DailyNote note)
        {
            double _fontSize;

            txtNote.Text = note.GetNote();
            _fontSize = note.NoteFontSize;
   
  
            if (_fontSize == 0) { txtNote.FontSize = _DEFAULT_FONT_SIZE; }
            else
            {
                txtNote.FontSize = _fontSize;
            }
            // global update of DailyNote at MainWindow

            SetColorOfNoterectangle(note.NoteWindowColor);
            UpdateNoteDataAtMainWindow(ThisNote);

        }
        private void SetColorOfNoterectangle(int color)
        {
            ThisNote.SetWindowNoteColor(color);

            byte _r1 = 0, _g1 = 0, _b1 = 0, _r2 = 0, _g2 = 0, _b2 = 0;

            if (color == 0) { _r1 = 56; _g1 = 53; _b1 = 53; _r2 = 74; _g2 = 92; _b2 = 86; }
            if (color == 1) { _r1 = 56; _g1 = 53; _b1 = 53; _r2 = 195; _g2 = 234; _b2 = 19; }
            if (color == 2) { _r1 = 56; _g1 = 53; _b1 = 53; _r2 = 17; _g2 = 123; _b2 = 224; }
            if (color == 3) { _r1 = 56; _g1 = 53; _b1 = 53; _r2 = 228; _g2 = 184; _b2 = 24; }


            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = new Point(0.5, 0);
            myLinearGradientBrush.EndPoint = new Point(0.5, 1);

            myLinearGradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(_r1, _g1, _b1), 1));
            myLinearGradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(_r2, _g2, _b2), 0));

            Point startPoint = new Point(0.5, 0);
            Point endPoint = new Point(0.5, 1);



            RectangleMain.Fill = myLinearGradientBrush;
        }

        #endregion

        #region Setting possition of this form possition

        void nSettingFormPosition(int nNb)
        {
            this.Left = communicatorWithMainWindow.mGetNoteFormPosX(nNb);
            this.Top = communicatorWithMainWindow.mGetNoteFormPosY(nNb);
        }

        #endregion


        #region Global update of Note in MainWindow

        private void UpdateNoteDataAtMainWindow(DailyNote note)
        {
            communicatorWithMainWindow.mSetNoteObjectInMainWindow(GlobalNoteNb, note);
        }

        #endregion



        #region Letters - changins size

        private void SetFontSizeOfNote(string upOrDown)
        {
            double _presentFontSize, _changedFontSize = 0;

            _presentFontSize = ThisNote.NoteFontSize;
            if (upOrDown == "UP") _changedFontSize = _presentFontSize + _FONT_INCREMENT;
            if (upOrDown == "DOWN") _changedFontSize = _presentFontSize - _FONT_INCREMENT;
            
            // check to avoid font size = -1
            if (_changedFontSize < 0) { _changedFontSize = _presentFontSize; } 
            ThisNote.SetNoteFontSize(_changedFontSize);
        }

        #endregion


        #region PostIt Note window size settings

        // TODO : Dodac mozliwosc zwiekszania, zmniejszania PostITa
        /*     
              selectionRectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rect1_MouseDown);
              selectionRectangle.MouseMove += new MouseEventHandler(Rectangle_MouseMove_1);
              selectionRectangle.MouseUp += new MouseButtonEventHandler(Rect1_MouseUp);


              private void RectangleResize_MouseDown(object sender, MouseButtonEventArgs e)
              {

                  drag = true;
                  Cursor = Cursors.Hand;
                  startPt = e.GetPosition(CanvasResize);
                  wid = (int)RectangleResize.Width;
                  hei = (int)RectangleResize.Height;
                  lastLoc = new Point(Canvas.GetLeft(RectangleResize), Canvas.GetTop(RectangleResize));
                  Mouse.Capture((IInputElement)sender);
              }

              private void RectangleResize_MouseMove(object sender, MouseEventArgs e)
              {

                  try
                  {
                      if (drag)
                      {
                          var newX = (startPt.X + (e.GetPosition(CanvasResize).X - startPt.X));
                          var newY = (startPt.Y + (e.GetPosition(CanvasResize).Y - startPt.Y));
                          Point offset = new Point((startPt.X - lastLoc.X), (startPt.Y - lastLoc.Y));
                          CanvasTop = newY - offset.Y;
                          CanvasLeft = newX - offset.X;
                          RectangleResize.SetValue(Canvas.TopProperty, CanvasTop);
                          RectangleResize.SetValue(Canvas.LeftProperty, CanvasLeft);

                      }
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message);
                  }

              }


              private void RectangleResize_MouseUp(object sender, MouseButtonEventArgs e)
              {
                  drag = false;
                  Cursor = Cursors.Arrow;
                  Mouse.Capture(null);
              }
      */
        #endregion


       



        private void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
