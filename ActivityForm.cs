using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ActivityForm : Form
    {
        public string selectedActivity { get; private set; }

        public ActivityForm()
        {
            InitializeComponent();
            InitializeButtonEvents();
            KeyPreview = true;
        }

        private void InitializeButtonEvents()
        {
            // Button Click Events
            btnnone.Click += (s, e) => SetStatusAndClose("");
            btnA1.Click += (s, e) => SetStatusAndClose("Traffic Stop");
            btnA2.Click += (s, e) => SetStatusAndClose("Vehicle Pursuit");
            btnA3.Click += (s, e) => SetStatusAndClose("Foot Pursuit");
            btnA4.Click += (s, e) => SetStatusAndClose("Routine Patrol");
            btnB1.Click += (s, e) => SetStatusAndClose("FTO/FTA");
            btnB2.Click += (s, e) => SetStatusAndClose("MCIU CALL");
            btnB3.Click += (s, e) => SetStatusAndClose("Felony Warrant");
            btnB4.Click += (s, e) => SetStatusAndClose("Domestic");
        }

        private void SetStatusAndClose(string status)
        {
            selectedActivity = status;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
