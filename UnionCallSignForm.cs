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
            this.KeyDown += UnionForm_KeyDown;
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

        private void UnionForm_KeyDown(object sender, KeyEventArgs ev)
        {

            switch (ev.KeyCode)
            {

                case Keys.NumPad0:
                    SetStatusAndClose("");
                    break;
                case Keys.NumPad1:
                    SetStatusAndClose("Union 1");
                    break;
                case Keys.NumPad2:
                    SetStatusAndClose("Union 2");
                    break;
                case Keys.NumPad3:
                    SetStatusAndClose("Union 3");
                    break;
                case Keys.NumPad4:
                    SetStatusAndClose("Union 4");
                    break;
                case Keys.NumPad5:
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
