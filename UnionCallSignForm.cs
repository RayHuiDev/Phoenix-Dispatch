using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UnionCallSignForm : Form
    {
        public string SelectedUnionCallSign { get; private set; }

        public UnionCallSignForm()
        {
            InitializeComponent();
            InitializeButtonEvents();
            KeyPreview = true;
            this.KeyDown += StatusForm_KeyDown;
        }

        private void InitializeButtonEvents()
        {
            // Button Click Events
            button0.Click += (s, e) => SetStatusAndClose("");
            button1.Click += (s, e) => SetStatusAndClose("Union 1");
            button2.Click += (s, e) => SetStatusAndClose("Union 2");
            button3.Click += (s, e) => SetStatusAndClose("Union 3");
            button4.Click += (s, e) => SetStatusAndClose("Union 4");
            button5.Click += (s, e) => SetStatusAndClose("Union 5");
        }

        private void StatusForm_KeyDown(object sender, KeyEventArgs ev)
        {

            switch (ev.KeyCode)
            {

                case Keys.D1:
                case Keys.NumPad1:
                    SetStatusAndClose("");
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    SetStatusAndClose("Union 1");
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    SetStatusAndClose("Union 2");
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    SetStatusAndClose("Union 3");
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    SetStatusAndClose("Union 4");
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    SetStatusAndClose("Union 5");
                    break;
            }
        }

        private void SetStatusAndClose(string status)
        {
            SelectedUnionCallSign = status;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
