using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StatusForm : Form
    {
        public string SelectedStatus { get; private set; }

        public StatusForm()
        {
            InitializeComponent();
            InitializeButtonEvents();
            KeyPreview = true;
            this.KeyDown += StatusForm_KeyDown;
        }

        private void InitializeButtonEvents()
        {
            // Button Click Events
            btnAvailable.Click += (s, e) => SetStatusAndClose("Available");
            btnBusy.Click += (s, e) => SetStatusAndClose("Busy");
            btnUnavailable.Click += (s, e) => SetStatusAndClose("Unavailable");
            btnEnRoute.Click += (s, e) => SetStatusAndClose("En Route");
            btnOnScene.Click += (s, e) => SetStatusAndClose("On Scene");
            btnOffDuty.Click += (s, e) => SetStatusAndClose("Off-Duty");
        }

        private void StatusForm_KeyDown(object sender, KeyEventArgs ev)
        {

            switch (ev.KeyCode)
            {

                case Keys.D1: 
                case Keys.NumPad1:
                    SetStatusAndClose("Available");
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    SetStatusAndClose("Busy");
                    break;
                case Keys.D3: 
                case Keys.NumPad3:
                    SetStatusAndClose("Unavailable");
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    SetStatusAndClose("En Route");
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    SetStatusAndClose("On Scene");
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    SetStatusAndClose("Off Duty");
                    break;
            }
        }

        private void SetStatusAndClose(string status)
        {
            SelectedStatus = status;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
