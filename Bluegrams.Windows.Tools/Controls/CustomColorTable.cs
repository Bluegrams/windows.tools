using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bluegrams.Windows.Tools
{
    /// <summary>
    /// A color table with a light and a dark theme.
    /// </summary>
    public class CustomColorTable : ProfessionalColorTable
    {
        public Color LightBackColor { get; set; } = SystemColors.Control;
        public Color DarkBackColor { get; set; } = Color.FromArgb(16, 16, 16);
        public Color LightHighlightBackColor { get; set; } = SystemColors.ControlLight;
        public Color DarkHighlightBackColor { get; set; } = Color.FromArgb(50, 50, 50);
        public Color LightBorderColor { get; set; } = SystemColors.ControlDark;
        public Color DarkBorderColor { get; set; } = Color.Black;

        private bool dark;
        private Color backColor, highlightBackColor, borderColor;

        /// <summary>
        /// Get or set a value indicating whether the dark theme is being used.
        /// </summary>
        public bool Dark
        {
            get { return dark; }
            set
            {
                dark = value;
                applyTheme();
            }
        }

        public CustomColorTable(bool dark)
        {
            this.Dark = dark;
        }

        private void applyTheme()
        {
            if (dark)
            {
                backColor = DarkBackColor;
                highlightBackColor = DarkHighlightBackColor;
                borderColor = DarkBorderColor;
            }
            else
            {
                backColor = LightBackColor;
                highlightBackColor = LightHighlightBackColor;
                borderColor = LightBorderColor;
            }
        }

        public override Color MenuItemSelectedGradientBegin => highlightBackColor;
        public override Color MenuItemSelectedGradientEnd => highlightBackColor;
        public override Color MenuItemSelected => highlightBackColor;

        public override Color MenuItemPressedGradientBegin => highlightBackColor;
        public override Color MenuItemPressedGradientEnd => highlightBackColor;

        public override Color MenuItemBorder => borderColor;
        public override Color MenuBorder => borderColor;

        public override Color ToolStripDropDownBackground => backColor;
        public override Color ImageMarginGradientBegin => backColor;
        public override Color ImageMarginGradientMiddle => backColor;
        public override Color ImageMarginGradientEnd => backColor;
    }
}
