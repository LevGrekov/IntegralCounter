namespace IntegralCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lowerLimit = new TextBox();
            upperLimit = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            functionBox = new TextBox();
            diff = new TextBox();
            pictureBox3 = new PictureBox();
            button1 = new Button();
            ValueBox = new TextBox();
            coord = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // lowerLimit
            // 
            lowerLimit.Location = new Point(12, 160);
            lowerLimit.Name = "lowerLimit";
            lowerLimit.Size = new Size(31, 31);
            lowerLimit.TabIndex = 0;
            lowerLimit.Text = "0";
            // 
            // upperLimit
            // 
            upperLimit.Location = new Point(12, 12);
            upperLimit.Name = "upperLimit";
            upperLimit.Size = new Size(31, 31);
            upperLimit.TabIndex = 1;
            upperLimit.Text = "1";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.integral;
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Cursor = Cursors.IBeam;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.ImageLocation = "";
            pictureBox1.Location = new Point(81, 69);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(0, 0);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.integral1;
            pictureBox2.Location = new Point(12, 49);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(31, 105);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(182, 83);
            label1.Name = "label1";
            label1.Size = new Size(33, 38);
            label1.TabIndex = 6;
            label1.Text = "d";
            // 
            // functionBox
            // 
            functionBox.Location = new Point(49, 90);
            functionBox.Name = "functionBox";
            functionBox.Size = new Size(127, 31);
            functionBox.TabIndex = 7;
            functionBox.Text = "x 2 ^";
            // 
            // diff
            // 
            diff.Location = new Point(209, 90);
            diff.Name = "diff";
            diff.Size = new Size(37, 31);
            diff.TabIndex = 8;
            diff.Text = "x";
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.BackColor = SystemColors.ControlLight;
            pictureBox3.Location = new Point(252, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(452, 464);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            pictureBox3.MouseMove += pictureBox3_MouseMove;
            pictureBox3.Resize += pictureBox3_Resize;
            // 
            // button1
            // 
            button1.Location = new Point(49, 196);
            button1.Name = "button1";
            button1.Size = new Size(127, 34);
            button1.TabIndex = 10;
            button1.Text = "найти площадь";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ValueBox
            // 
            ValueBox.Location = new Point(49, 236);
            ValueBox.Name = "ValueBox";
            ValueBox.Size = new Size(127, 31);
            ValueBox.TabIndex = 11;
            // 
            // coord
            // 
            coord.Location = new Point(49, 298);
            coord.Name = "coord";
            coord.Size = new Size(127, 31);
            coord.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(716, 488);
            Controls.Add(coord);
            Controls.Add(pictureBox3);
            Controls.Add(ValueBox);
            Controls.Add(button1);
            Controls.Add(diff);
            Controls.Add(functionBox);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(upperLimit);
            Controls.Add(lowerLimit);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox lowerLimit;
        private TextBox upperLimit;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private TextBox functionBox;
        private TextBox diff;
        private Button button1;
        private TextBox ValueBox;
        private PictureBox pictureBox3;
        private TextBox coord;
    }
}