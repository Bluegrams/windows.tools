using System;
using System.Windows.Forms;
using Bluegrams.Windows.Tools.Properties;

namespace Bluegrams.Windows.Tools
{
    /// <summary>
    /// Provides a dialog form for specifying a shortcut key combination.
    /// </summary>
    public partial class HotKeyInputForm : Form
    {
        private KeysConverter keyConv;

        public string DescriptionText
        {
            get => grpHotKey.Text;
            set => grpHotKey.Text = value;
        }

        /// <summary>
        /// If set to true, at least one modifier key is required for a valid key combination.
        /// </summary>
        public bool ModifiersRequired { get; set; } = true;

        /// <summary>
        /// The selected key combination.
        /// </summary>
        public Keys SelectedKeys { get; private set; }

        /// <summary>
        /// Creates a new instance of class HotKeyInputForm.
        /// </summary>
        /// <param name="initialKeys">The initial key combination entered in the textbox.</param>
        public HotKeyInputForm(Keys initialKeys = Keys.None)
        {
            InitializeComponent();
            keyConv = new KeysConverter();
            SelectedKeys = initialKeys;
            txtKeyInput.Text = keyConv.ConvertToString(SelectedKeys);
        }

        private void HotKeyInputForm_Activated(object sender, EventArgs e)
        {
            txtKeyInput.Focus();
        }

        private void txtKeyInput_KeyDown(object sender, KeyEventArgs e)
        {
            SelectedKeys = e.KeyData;
            txtKeyInput.Text = keyConv.ConvertToString(e.KeyData);
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            if (ModifiersRequired && (SelectedKeys & Keys.Modifiers) == 0)
            {
                MessageBox.Show(Resources.HotKeyInputForm_Invalid, Resources.HotKeyInputForm_Invalid_Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKeyInput.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
