using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BrowserSelect {
    class VButton : Button {
        StringFormat Fmt = new StringFormat();
        private string faketext;
        public VButton() {
            Fmt.Alignment = StringAlignment.Center;
            Fmt.LineAlignment = StringAlignment.Center;
        }
        protected override void OnTextChanged(EventArgs e) {
            if(Text.Length>0)
                faketext = Text;
            Text = "";
            base.OnTextChanged(e);
        }
        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);
            pevent.Graphics.TranslateTransform(Width, 0);
            pevent.Graphics.RotateTransform(90);
            pevent.Graphics.DrawString(faketext, Font, Brushes.Black, new Rectangle(0, 0, Height, Width), Fmt);
        }
    }
}
