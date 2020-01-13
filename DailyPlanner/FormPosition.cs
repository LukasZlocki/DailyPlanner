using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner
{
    class FormPosition
    {

        private double PosX=0;
        private double PosY=0;



        // *************
        // *** METHODS *
        // *************


        // settings possition
        public void fpSetXPos(double Xp)
        {
            PosX = Xp;
        }

       public void fpSetYPos(double Yp)
        {
            PosY = Yp;
        }


        // getting position
        public double fpGetXPos ()
        {
            return (PosX);
        }

        public double fpGetYPos()
        {
            return (PosY);
        }

    }
}
