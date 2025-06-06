﻿namespace WindowsFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.addPlayerList = new System.Windows.Forms.ListView();
            this.callSign = new System.Windows.Forms.RichTextBox();
            this.callSignLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.unitGridView = new System.Windows.Forms.DataGridView();
            this.activeGridView = new System.Windows.Forms.DataGridView();
            this.activeHeading = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timerPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // addPlayerList
            // 
            this.addPlayerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addPlayerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addPlayerList.HideSelection = false;
            this.addPlayerList.Location = new System.Drawing.Point(12, 8);
            this.addPlayerList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addPlayerList.Name = "addPlayerList";
            this.addPlayerList.Size = new System.Drawing.Size(1197, 153);
            this.addPlayerList.TabIndex = 0;
            this.addPlayerList.UseCompatibleStateImageBehavior = false;
            // 
            // callSign
            // 
            this.callSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.callSign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.callSign.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.callSign.ForeColor = System.Drawing.Color.White;
            this.callSign.Location = new System.Drawing.Point(129, 55);
            this.callSign.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.callSign.MaxLength = 5;
            this.callSign.Multiline = false;
            this.callSign.Name = "callSign";
            this.callSign.Size = new System.Drawing.Size(60, 28);
            this.callSign.TabIndex = 1;
            this.callSign.Text = "";
            this.callSign.TextChanged += new System.EventHandler(this.callSign_TextChanged_1);
            // 
            // callSignLabel
            // 
            this.callSignLabel.AutoSize = true;
            this.callSignLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.callSignLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.callSignLabel.ForeColor = System.Drawing.Color.White;
            this.callSignLabel.Location = new System.Drawing.Point(20, 32);
            this.callSignLabel.Name = "callSignLabel";
            this.callSignLabel.Size = new System.Drawing.Size(70, 21);
            this.callSignLabel.TabIndex = 2;
            this.callSignLabel.Text = "CallSign:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(20, 90);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(55, 21);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Name:";
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.name.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ForeColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(24, 113);
            this.name.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.name.MaxLength = 16;
            this.name.Multiline = false;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(177, 29);
            this.name.TabIndex = 3;
            this.name.Text = "";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(208, 113);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add Unit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddUnit_Click);
            // 
            // unitGridView
            // 
            this.unitGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.unitGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.unitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unitGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.unitGridView.Location = new System.Drawing.Point(110, 165);
            this.unitGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.unitGridView.Name = "unitGridView";
            this.unitGridView.Size = new System.Drawing.Size(1099, 421);
            this.unitGridView.TabIndex = 6;
            this.unitGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.unitGridView_CellValueChanged);
            // 
            // activeGridView
            // 
            this.activeGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.activeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activeGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeGridView.Location = new System.Drawing.Point(12, 187);
            this.activeGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.activeGridView.Name = "activeGridView";
            this.activeGridView.Size = new System.Drawing.Size(87, 399);
            this.activeGridView.TabIndex = 7;
            // 
            // activeHeading
            // 
            this.activeHeading.AutoSize = true;
            this.activeHeading.BackColor = System.Drawing.Color.Transparent;
            this.activeHeading.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeHeading.ForeColor = System.Drawing.Color.White;
            this.activeHeading.Location = new System.Drawing.Point(11, 163);
            this.activeHeading.Name = "activeHeading";
            this.activeHeading.Size = new System.Drawing.Size(92, 21);
            this.activeHeading.TabIndex = 8;
            this.activeHeading.Text = "Active Units";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "LSPD",
            "LCSO",
            "SAHP",
            "PBPD",
            "SAMS",
            "SADOT"});
            this.comboBox1.Location = new System.Drawing.Point(24, 55);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 29);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "LSPD";
            // 
            // timerPanel
            // 
            this.timerPanel.Location = new System.Drawing.Point(111, 595);
            this.timerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timerPanel.Name = "timerPanel";
            this.timerPanel.Size = new System.Drawing.Size(1098, 103);
            this.timerPanel.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Impact", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(99, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 21);
            this.label3.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(98, 55);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(30, 28);
            this.label4.TabIndex = 14;
            this.label4.Text = "L-";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 595);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WindowsFormsApp1.Properties.Resources.Full_Map;
            this.pictureBox2.Location = new System.Drawing.Point(1219, 165);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(491, 421);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1724, 701);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timerPanel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.activeHeading);
            this.Controls.Add(this.activeGridView);
            this.Controls.Add(this.unitGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.name);
            this.Controls.Add(this.callSignLabel);
            this.Controls.Add(this.callSign);
            this.Controls.Add(this.addPlayerList);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dispatch";
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView addPlayerList;
        private System.Windows.Forms.RichTextBox callSign;
        private System.Windows.Forms.Label callSignLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.RichTextBox name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView unitGridView;
        private System.Windows.Forms.DataGridView activeGridView;
        private System.Windows.Forms.Label activeHeading;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel timerPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

