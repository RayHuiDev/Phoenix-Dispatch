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
            btnA4.Click += (s, e) => SetStatusAndClose("Code 1");
            btnB1.Click += (s, e) => SetStatusAndClose("Code 2");
            btnB2.Click += (s, e) => SetStatusAndClose("Code 3");
            btnB3.Click += (s, e) => SetStatusAndClose("Code 4");
            btnB4.Click += (s, e) => SetStatusAndClose("Code 5");
            btnC1.Click += (s, e) => SetStatusAndClose("District 1");
            btnC2.Click += (s, e) => SetStatusAndClose("Shots Fired");
            btnC3.Click += (s, e) => SetStatusAndClose("Traffic Accident");
            btnC4.Click += (s, e) => SetStatusAndClose("Starting Tour");
            btnD1.Click += (s, e) => SetStatusAndClose("District 2");
            btnD2.Click += (s, e) => SetStatusAndClose("Return To Station");
            btnD3.Click += (s, e) => SetStatusAndClose("Armed Suspect");
            btnD4.Click += (s, e) => SetStatusAndClose("Ending Tour");
            btnE1.Click += (s, e) => SetStatusAndClose("District 3");
            btnE2.Click += (s, e) => SetStatusAndClose("Officer Down");
            btnE3.Click += (s, e) => SetStatusAndClose("Imprp Parked Veh");
            btnE4.Click += (s, e) => SetStatusAndClose("Domestic");
            btnF1.Click += (s, e) => SetStatusAndClose("District 4");
            btnF2.Click += (s, e) => SetStatusAndClose("Reckless Driving");
            btnF3.Click += (s, e) => SetStatusAndClose("Stolen Vehicle");
            btnF4.Click += (s, e) => SetStatusAndClose("Intoxicated Driver");
            btnG1.Click += (s, e) => SetStatusAndClose("District 5");
            btnG2.Click += (s, e) => SetStatusAndClose("FTO/FTA");
            btnG3.Click += (s, e) => SetStatusAndClose("MCIU CALL");
            btnG4.Click += (s, e) => SetStatusAndClose("Supervisor Call");
        }

        private void SetStatusAndClose(string status)
        {
            selectedActivity = status;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
