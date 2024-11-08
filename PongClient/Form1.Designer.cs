namespace PongClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pb = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
            SuspendLayout();
            // 
            // pb
            // 
            pb.BackColor = SystemColors.GradientActiveCaption;
            pb.Dock = DockStyle.Fill;
            pb.Location = new Point(0, 0);
            pb.Name = "pb";
            pb.Size = new Size(800, 450);
            pb.TabIndex = 0;
            pb.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pb);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pb;
    }
}
