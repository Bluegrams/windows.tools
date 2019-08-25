using System;
using System.Windows.Forms;
using System.Drawing;

namespace Bluegrams.Windows.Tools
{
    /// <summary>
    /// Provides an overlay over the screen that allows the selection of a window.
    /// </summary>
    public class OverlayForm : Form
    {
        private bool transp = false;

        /// <summary>
        /// The rectangle of the selected window.
        /// </summary>
        public Rectangle SelectionRectangle { get; private set; }
        /// <summary>
        /// The native handle of the selected window.
        /// </summary>
        public IntPtr SelectionHandle { get; private set; }
        /// <summary>
        /// Gets or sets the color used to highlight the selected window.
        /// </summary>
        public Color SelectionColor { get; set; } = Color.Red;

        /// <summary>
        /// Creates a new instance of class OverlayForm.
        /// </summary>
        public OverlayForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.Opacity = 0.15;
            this.DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Cursor = Cursors.Cross;
            this.Location = SystemInformation.VirtualScreen.Location;
            this.Size = SystemInformation.VirtualScreen.Size;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) //WM_NCHITTEST (called in response to WindowFromPoint)
            {
                // Set HTTRANSPARENT as result to ignore this window in WindowFromPoint.
                if (transp)
                {
                    m.Result = (IntPtr)(-1);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
            base.OnKeyDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            try
            {
                var point = PointToScreen(e.Location);
                transp = true;
                SelectionHandle = NativeWindowSelection.GetWindowFromPoint(point, out Rectangle windowRect);
                SelectionRectangle = windowRect;
                transp = false;
                this.Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            using (var brush = new SolidBrush(SelectionColor))
            {
                e.Graphics.FillRectangle(brush, RectangleToClient(SelectionRectangle));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (SelectionRectangle != null)
                this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }
    }
}
