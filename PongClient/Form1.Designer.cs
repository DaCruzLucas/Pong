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
            HostBTN = new Button();
            label1 = new Label();
            JoinBTN = new Button();
            JoinInput = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
            SuspendLayout();
            // 
            // pb
            // 
            pb.BackColor = SystemColors.GradientActiveCaption;
            pb.Dock = DockStyle.Fill;
            pb.Location = new Point(0, 0);
            pb.Name = "pb";
            pb.Size = new Size(634, 661);
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
            // HostBTN
            // 
            HostBTN.Location = new Point(260, 345);
            HostBTN.Name = "HostBTN";
            HostBTN.Size = new Size(75, 23);
            HostBTN.TabIndex = 1;
            HostBTN.Text = "Host";
            HostBTN.UseVisualStyleBackColor = true;
            HostBTN.Click += HostBTN_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.GradientActiveCaption;
            label1.Font = new Font("Segoe UI", 30F);
            label1.Location = new Point(251, 89);
            label1.Name = "label1";
            label1.Size = new Size(114, 54);
            label1.TabIndex = 2;
            label1.Text = "Pong";
            // 
            // JoinBTN
            // 
            JoinBTN.Location = new Point(260, 302);
            JoinBTN.Name = "JoinBTN";
            JoinBTN.Size = new Size(75, 23);
            JoinBTN.TabIndex = 3;
            JoinBTN.Text = "Join";
            JoinBTN.UseVisualStyleBackColor = true;
            JoinBTN.Click += JoinBTN_Click;
            // 
            // JoinInput
            // 
            JoinInput.Location = new Point(251, 247);
            JoinInput.Name = "JoinInput";
            JoinInput.Size = new Size(100, 23);
            JoinInput.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 661);
            Controls.Add(JoinInput);
            Controls.Add(JoinBTN);
            Controls.Add(label1);
            Controls.Add(HostBTN);
            Controls.Add(pb);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Pong Game";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pb;
        private System.Windows.Forms.Timer tmr;
        private Button button1;
        private Label label1;
        private Button button2;
        private Button HostBTN;
        private Button JoinBTN;
        private TextBox textBox1;
        private TextBox JoinInput;
    }
}
