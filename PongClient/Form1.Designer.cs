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
            components = new System.ComponentModel.Container();
            pb = new PictureBox();
            tmr = new System.Windows.Forms.Timer(components);
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
            pb.Paint += pb_Paint;
            // 
            // tmr
            // 
            tmr.Enabled = true;
            tmr.Interval = 20;
            tmr.Tick += tmr_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pb);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Pong Game";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pb;
        private System.Windows.Forms.Timer tmr;
    }
}
