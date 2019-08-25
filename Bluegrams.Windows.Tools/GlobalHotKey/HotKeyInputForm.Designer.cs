namespace Bluegrams.Windows.Tools
{
    partial class HotKeyInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeyInputForm));
            this.butCancel = new System.Windows.Forms.Button();
            this.butSubmit = new System.Windows.Forms.Button();
            this.grpHotKey = new System.Windows.Forms.GroupBox();
            this.txtKeyInput = new System.Windows.Forms.TextBox();
            this.grpHotKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // butSubmit
            // 
            resources.ApplyResources(this.butSubmit, "butSubmit");
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.UseVisualStyleBackColor = true;
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // grpHotKey
            // 
            this.grpHotKey.Controls.Add(this.txtKeyInput);
            resources.ApplyResources(this.grpHotKey, "grpHotKey");
            this.grpHotKey.Name = "grpHotKey";
            this.grpHotKey.TabStop = false;
            // 
            // txtKeyInput
            // 
            resources.ApplyResources(this.txtKeyInput, "txtKeyInput");
            this.txtKeyInput.Name = "txtKeyInput";
            this.txtKeyInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyInput_KeyDown);
            // 
            // HotKeyInputForm
            // 
            this.AcceptButton = this.butSubmit;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.grpHotKey);
            this.Controls.Add(this.butSubmit);
            this.Controls.Add(this.butCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotKeyInputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Activated += new System.EventHandler(this.HotKeyInputForm_Activated);
            this.grpHotKey.ResumeLayout(false);
            this.grpHotKey.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.GroupBox grpHotKey;
        private System.Windows.Forms.TextBox txtKeyInput;
    }
}