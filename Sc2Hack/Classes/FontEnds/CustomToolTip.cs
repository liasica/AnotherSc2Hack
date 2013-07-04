using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sc2Hack.Classes.FontEnds
{
    public partial class CustomToolTip : Form
    {
        public CustomToolTip()
        {
            InitializeComponent();

            _gDrawString = CreateGraphics();
        }

        private String _strMessage = String.Empty;
        private Control _cControl = null;
        private Control _cAdditionalControl = null;
        private MainHandler _mhHandle = null;
        private Int32 _iTime = 5;
        private Graphics _gDrawString;

        public void Show(String message, Control ctrl, MainHandler mnHnd, Control additionalControl)
        {
            _strMessage = message;
            _cControl = ctrl;
            _mhHandle = mnHnd;
            _cAdditionalControl = additionalControl;

            ShowString();
        }

        private void ShowString()
        {
            Show();
            

            tmrClose.Enabled = true;

            
        }

        private Int32 _iCountedTicks = 0;
        private void tmrClose_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position.X >= _cControl.Left + _mhHandle.Left + _cAdditionalControl.Left &&
                Cursor.Position.Y >= _cControl.Top + _mhHandle.Top + _cAdditionalControl.Top &&
                Cursor.Position.X <= _cControl.Width + _cControl.Left + _mhHandle.Location.X + _cAdditionalControl.Location.X &&
                Cursor.Position.Y <= _cControl.Height + _cControl.Top + _mhHandle.Location.Y + _cAdditionalControl.Location.Y)
            {
                _gDrawString.DrawString(_strMessage, Font, Brushes.Black, 5, 5);
                Location = Cursor.Position;
            }

            else
                Refresh();
          

            _iCountedTicks++;

        }
    }
}
