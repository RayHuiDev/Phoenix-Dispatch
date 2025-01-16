namespace WindowsFormsApp1
{
    partial class StatusForm
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
            this.btnAvailable = new System.Windows.Forms.Button();
            this.btnBusy = new System.Windows.Forms.Button();
            this.btnUnavailable = new System.Windows.Forms.Button();
            this.btnEnRoute = new System.Windows.Forms.Button();
            this.btnOnScene = new System.Windows.Forms.Button();
            this.btnOffDuty = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAvailable
            // 
            this.btnAvailable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAvailable.FlatAppearance.BorderSize = 0;
            this.btnAvailable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvailable.ForeColor = System.Drawing.Color.White;
            this.btnAvailable.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAvailable.Location = new System.Drawing.Point(12, 20);
            this.btnAvailable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAvailable.Name = "btnAvailable";
            this.btnAvailable.Size = new System.Drawing.Size(219, 28);
            this.btnAvailable.TabIndex = 6;
            this.btnAvailable.Text = "Available - 1";
            this.btnAvailable.UseVisualStyleBackColor = false;
            // 
            // btnBusy
            // 
            this.btnBusy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBusy.FlatAppearance.BorderSize = 0;
            this.btnBusy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBusy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBusy.ForeColor = System.Drawing.Color.White;
            this.btnBusy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBusy.Location = new System.Drawing.Point(12, 52);
            this.btnBusy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBusy.Name = "btnBusy";
            this.btnBusy.Size = new System.Drawing.Size(219, 28);
            this.btnBusy.TabIndex = 7;
            this.btnBusy.Text = "Busy - 2";
            this.btnBusy.UseVisualStyleBackColor = false;
            // 
            // btnUnavailable
            // 
            this.btnUnavailable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUnavailable.FlatAppearance.BorderSize = 0;
            this.btnUnavailable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnavailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnavailable.ForeColor = System.Drawing.Color.White;
            this.btnUnavailable.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUnavailable.Location = new System.Drawing.Point(12, 84);
            this.btnUnavailable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUnavailable.Name = "btnUnavailable";
            this.btnUnavailable.Size = new System.Drawing.Size(219, 28);
            this.btnUnavailable.TabIndex = 8;
            this.btnUnavailable.Text = "UnAvailable - 3";
            this.btnUnavailable.UseVisualStyleBackColor = false;
            // 
            // btnEnRoute
            // 
            this.btnEnRoute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEnRoute.FlatAppearance.BorderSize = 0;
            this.btnEnRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnRoute.ForeColor = System.Drawing.Color.White;
            this.btnEnRoute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnRoute.Location = new System.Drawing.Point(12, 116);
            this.btnEnRoute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEnRoute.Name = "btnEnRoute";
            this.btnEnRoute.Size = new System.Drawing.Size(219, 28);
            this.btnEnRoute.TabIndex = 9;
            this.btnEnRoute.Text = "En Route - 4";
            this.btnEnRoute.UseVisualStyleBackColor = false;
            // 
            // btnOnScene
            // 
            this.btnOnScene.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOnScene.FlatAppearance.BorderSize = 0;
            this.btnOnScene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnScene.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOnScene.ForeColor = System.Drawing.Color.White;
            this.btnOnScene.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOnScene.Location = new System.Drawing.Point(12, 148);
            this.btnOnScene.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOnScene.Name = "btnOnScene";
            this.btnOnScene.Size = new System.Drawing.Size(219, 28);
            this.btnOnScene.TabIndex = 10;
            this.btnOnScene.Text = "On Scene - 5";
            this.btnOnScene.UseVisualStyleBackColor = false;
            // 
            // btnOffDuty
            // 
            this.btnOffDuty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOffDuty.FlatAppearance.BorderSize = 0;
            this.btnOffDuty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffDuty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffDuty.ForeColor = System.Drawing.Color.White;
            this.btnOffDuty.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOffDuty.Location = new System.Drawing.Point(12, 180);
            this.btnOffDuty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOffDuty.Name = "btnOffDuty";
            this.btnOffDuty.Size = new System.Drawing.Size(219, 28);
            this.btnOffDuty.TabIndex = 11;
            this.btnOffDuty.Text = "Off Duty - 6";
            this.btnOffDuty.UseVisualStyleBackColor = false;
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(243, 230);
            this.ControlBox = false;
            this.Controls.Add(this.btnOffDuty);
            this.Controls.Add(this.btnOnScene);
            this.Controls.Add(this.btnEnRoute);
            this.Controls.Add(this.btnUnavailable);
            this.Controls.Add(this.btnBusy);
            this.Controls.Add(this.btnAvailable);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(259, 269);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(259, 269);
            this.Name = "StatusForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StatusForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAvailable;
        private System.Windows.Forms.Button btnBusy;
        private System.Windows.Forms.Button btnUnavailable;
        private System.Windows.Forms.Button btnEnRoute;
        private System.Windows.Forms.Button btnOnScene;
        private System.Windows.Forms.Button btnOffDuty;
    }
}