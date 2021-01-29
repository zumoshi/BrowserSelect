using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrowserSelect
{
    //=============================================================================================================
    class VerticalButton : Button
    //=============================================================================================================
    {
        private StringFormat placeholderFormat = new StringFormat();
        private string placeholderString;

        //-------------------------------------------------------------------------------------------------------------
        public VerticalButton()
        //-------------------------------------------------------------------------------------------------------------
        {
            placeholderFormat.Alignment = StringAlignment.Center;
            placeholderFormat.LineAlignment = StringAlignment.Center;
        }

        //-------------------------------------------------------------------------------------------------------------
        protected override void OnTextChanged(EventArgs e)
        //-------------------------------------------------------------------------------------------------------------
        {
            if (Text.Length > 0)
                placeholderString = Text;

            Text = "";

            base.OnTextChanged(e);
        }

        //-------------------------------------------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pevent)
        //-------------------------------------------------------------------------------------------------------------
        {
            base.OnPaint(pevent);

            pevent.Graphics.TranslateTransform(Width, 0);
            pevent.Graphics.RotateTransform(90);
            pevent.Graphics.DrawString(placeholderString, Font, Brushes.Black, new Rectangle(0, 0, Height, Width), placeholderFormat);
        }
    }
}
