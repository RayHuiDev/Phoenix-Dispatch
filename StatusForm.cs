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
            btnOffDuty.Click += (s, e) => SetStatusAndClose("Off Duty");
            btnPursuit.Click += (s, e) => SetStatusAndClose("Pursuit");
            btnDispatch.Click += (s, e) => SetStatusAndClose("Dispatch");
        }

        private void StatusForm_KeyDown(object sender, KeyEventArgs ev)
        {

            switch (ev.KeyCode)
            {

                case Keys.NumPad1:
                    SetStatusAndClose("Available");
                    break;
                case Keys.NumPad2:
                    SetStatusAndClose("Busy");
                    break;
                case Keys.NumPad3:
                    SetStatusAndClose("Unavailable");
                    break;
                case Keys.NumPad4:
                    SetStatusAndClose("En Route");
                    break;
                case Keys.NumPad5:
                    SetStatusAndClose("On Scene");
                    break;
                case Keys.NumPad6:
                    SetStatusAndClose("Off Duty");
                    break;
                case Keys.NumPad7:
                    SetStatusAndClose("Pursuit");
                    break;
                case Keys.NumPad8:
                    SetStatusAndClose("Dispatch");
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
