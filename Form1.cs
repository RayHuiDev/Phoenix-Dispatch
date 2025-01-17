using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static WindowsFormsApp1.Form1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public class Unit
        {
            public string CallSign { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Location { get; set; }
            public string Notes { get; set; }
            public string Activity { get; set; }
            public string UnionCallSign { get; set; }
            public string Department { get; set; }
        }

        List<Unit> units = new List<Unit>();
        private Dictionary<string, DateTime> onSceneTimers = new Dictionary<string, DateTime>();
        private Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            InitializeUnitGridView();
            InitializeActiveGridView();
            StyleActiveGridView();
            StyleUnitGridView();
            StyleComboBox();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Event handlers
            callSign.KeyDown += TextBox_KeyDown;
            name.KeyDown += TextBox_KeyDown;
            unitGridView.CellContentClick += unitGridView_CellContentClick;
            unitGridView.CellFormatting += unitGridView_CellFormatting;
            unitGridView.CellValueChanged += unitGridView_CellValueChanged;
            unitGridView.CurrentCellDirtyStateChanged += unitGridView_CurrentCellDirtyStateChanged;
            unitGridView.CellClick += unitGridView_CellClick;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            this.KeyPreview = true;
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem?.ToString();

            if (selectedValue == "LSPD")
            {
                label4.Text = "L-";
            }
            else if (selectedValue == "PBPD") // Ensure index matches the item in the list
            {
                label4.Text = "P-";
            }
            else if (selectedValue == "LCSO") // Ensure index matches the item in the list
            {
                label4.Text = "C-";
            }
            else if (selectedValue == "SAHP") // Ensure index matches the item in the list
            {
                label4.Text = "H-";
            }
            else if (selectedValue == "SAMS") // Ensure index matches the item in the list
            {
                label4.Text = "E-";
            }
            else if (selectedValue == "SADOT") // Ensure index matches the item in the list
            {
                label4.Text = "D-";
            }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 300,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    ForeColor = Color.White,
                    StartPosition = FormStartPosition.CenterScreen,
                    BackColor = Color.FromArgb(35, 35, 35),
                };

                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, AutoSize = true };
                TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 200 };

                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
            }
        }
        private void unitGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == unitGridView.Columns["Status"].Index)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();

                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Open the StatusForm to select a new status
                    using (StatusForm statusForm = new StatusForm())
                    {
                        if (statusForm.ShowDialog() == DialogResult.OK)
                        {
                            var selectedStatus = statusForm.SelectedStatus;
                            unitGridView.Rows[e.RowIndex].Cells["Status"].Value = selectedStatus;
                            unit.Status = selectedStatus; // Update the units list
                        }
                    }
                }
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == unitGridView.Columns["Activity"].Index)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Open the StatusForm to select a new status
                    using (ActivityForm activityForm = new ActivityForm())
                    {
                        if (activityForm.ShowDialog() == DialogResult.OK)
                        {
                            // Update the unit's status with the selected value
                            var selectedActivity = activityForm.selectedActivity;
                            unitGridView.Rows[e.RowIndex].Cells["Activity"].Value = selectedActivity;
                            unit.Activity = selectedActivity; // Update the units list
                        }
                    }
                }
            }
            
            if (e.RowIndex >= 0 && e.ColumnIndex == unitGridView.Columns["Location"].Index)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Open the StatusForm to select a new status
                   string location = Prompt.ShowDialog("Enter the location", "Location");
                    unitGridView.Rows[e.RowIndex].Cells["Location"].Value = location;
                    unit.Location = location;
                }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == unitGridView.Columns["UnionCallSign"].Index)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Open the StatusForm to select a new status
                    using (UnionCallSignForm unionCallSignForm = new UnionCallSignForm())
                    {
                        if (unionCallSignForm.ShowDialog() == DialogResult.OK)
                        {
                            // Update the unit's status with the selected value
                            var selectedUnionCallSign = unionCallSignForm.SelectedUnionCallSign;
                            unitGridView.Rows[e.RowIndex].Cells["UnionCallSign"].Value = selectedUnionCallSign;
                            unit.UnionCallSign = selectedUnionCallSign; // Update the units list
                        }
                    }
                }

            }
        }
        private void unitGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the "Notes" column
            if (e.RowIndex >= 0 && e.ColumnIndex == unitGridView.Columns["Notes"].Index)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Prompt the user to enter the notes
                    string notes = Prompt.ShowDialog("Enter the notes", "Notes");

                    // Update the "Notes" field with the entered text
                    unitGridView.Rows[e.RowIndex].Cells["Notes"].Value = notes;
                    unit.Notes = notes; // Update the units list
                    unitGridView.ClearSelection();
                    unitGridView.Refresh();

                }
            }

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if "1" on the numpad is pressed
            if (e.KeyCode == Keys.NumPad0)
            {
                // Find the selected row
                if (unitGridView.SelectedRows.Count > 0)
                {
                    int rowIndex = unitGridView.SelectedRows[0].Index;
                    if (rowIndex >= 0)
                    {
                        string callSign = unitGridView.Rows[rowIndex].Cells["CallSign"].Value?.ToString();
                        if (!string.IsNullOrEmpty(callSign))
                        {
                            var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                            if (unit == null) return;
                            // Open the StatusForm to select a new status
                            using (StatusForm statusForm = new StatusForm())
                            {
                                if (statusForm.ShowDialog() == DialogResult.OK)
                                {
                                    // Update the unit's status with the selected value
                                    var selectedStatus = statusForm.SelectedStatus;
                                    unitGridView.Rows[rowIndex].Cells["Status"].Value = selectedStatus;
                                    unit.Status = selectedStatus; // Update the units list
                                }
                            }
                        }
                    }
                }
            }
        }


        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            string CallSign = callSign.Text.Trim();
            string nameText = name.Text.Trim();
            string department = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(CallSign) || string.IsNullOrEmpty(nameText))
            {
                MessageBox.Show("Call Sign, Name, and Department are required!");
                return;
            }

            string callprefix = "";
            switch (department)
            {
                case "LSPD":
                    callprefix = "L-";
                    break;
                case "PBPD":
                    callprefix = "P-";
                    break;
                case "SAMS":
                    callprefix = "E-";
                    break;
                case "LCSO":
                    callprefix = "C-";
                    break;
                case "SAHP":
                    callprefix = "H-";
                    break;
                case "SADOT":
                    callprefix = "D-";
                    break;

            }

            Unit newUnit = new Unit
            {
                CallSign = callprefix + CallSign,
                Name = nameText,
                Status = "Available",
                Location = "Unknown",
                Notes = "",
                Activity = " ",
                UnionCallSign = " ",
                Department = department
            };

            units.Add(newUnit); // Add to BindingList
            unitGridView.Rows.Add(newUnit.CallSign, newUnit.Name, newUnit.Department, newUnit.Status, newUnit.Location, newUnit.Notes, newUnit.Activity, newUnit.UnionCallSign);
            callSign.Text = "";
            name.Text = "";

            AddToActiveGridView(callprefix + CallSign);
            

            unitGridView.Sort(unitGridView.Columns["CallSign"], System.ComponentModel.ListSortDirection.Ascending);
            unitGridView.ClearSelection();
            unitGridView.Refresh();
        }

        // Handle Enter key to add unit
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAddUnit_Click(sender, e);
                callSign.Focus();
            }
        }

        private void StyleComboBox()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // Add to active grid view
        private void AddToActiveGridView(string callSign)
        {
            if (!activeGridView.Rows.Cast<DataGridViewRow>().Any(row => row.Cells["CallSign"].Value?.ToString() == callSign))
            {
                activeGridView.Rows.Add(callSign);
                activeGridView.Sort(activeGridView.Columns["CallSign"], System.ComponentModel.ListSortDirection.Ascending);
            }

            // Refresh the active grid after adding
            activeGridView.Refresh();
        }

        // Initialize the active grid view
        private void InitializeActiveGridView()
        {
            activeGridView.Columns.Add("CallSign", "Call Sign");
            activeGridView.Columns["CallSign"].SortMode = DataGridViewColumnSortMode.NotSortable;
            activeGridView.AllowUserToAddRows = false;
            activeGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            activeGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            activeGridView.ColumnHeadersVisible = false;
            activeGridView.RowHeadersVisible = false;
            activeGridView.DefaultCellStyle.Font = new Font("Impact", 15);

            foreach (DataGridViewColumn column in activeGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // Initialize the unit grid view
        private void InitializeUnitGridView()
        {
            if (unitGridView.Columns.Count == 0)  // Check if columns are already initialized
            {
                unitGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "CallSign", HeaderText = "CallSign" });
                unitGridView.Columns.Add("Name", "Name");
                unitGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", ReadOnly = true });

                var cmbStatus = new DataGridViewButtonColumn { Name = "Status", HeaderText = "Status", Text = "Available" };
                unitGridView.Columns.Add(cmbStatus);

                var locationColumn = new DataGridViewButtonColumn { Name = "Location", HeaderText = "Location", UseColumnTextForButtonValue = false };
                unitGridView.Columns.Add(locationColumn);
                unitGridView.Columns.Add("Notes", "Notes");

                var activityColumn = new DataGridViewButtonColumn { Name = "Activity", HeaderText = "Activity", UseColumnTextForButtonValue = false };
                unitGridView.Columns.Add(activityColumn);
                /*activityColumn.Items.AddRange("",
                    "Dispatcher", "Routine Patrol", "D1 Patrol",
                    "D2 Patrol", "D3 Patrol", "D4 Patrol", "D5 Patrol",
                    "Traffic Stop", "Vehicle Pursiut", "Other(Notes)");*/

                var unionCallSignColumn = new DataGridViewButtonColumn { Name = "UnionCallSign", HeaderText = "Union Call Sign", UseColumnTextForButtonValue = false };
                unitGridView.Columns.Add(unionCallSignColumn);
               // unionCallSignColumn.Items.AddRange("", "Union 1", "Union 2", "Union 3", "Union 4", "Union 5", "Union 6");

                var btnRemove = new DataGridViewButtonColumn
                {
                    Name = "Remove",
                    HeaderText = "",
                    Text = "Remove",
                    UseColumnTextForButtonValue = true
                };
                unitGridView.Columns.Add(btnRemove);

                unitGridView.AllowUserToAddRows = false;
                unitGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                unitGridView.CellContentClick += btnRemoveUnit_Click;

                unitGridView.RowHeadersVisible = false;
                unitGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                unitGridView.Columns["CallSign"].Width = 40;
                unitGridView.Columns["Name"].Width = 50;
                unitGridView.Columns["Department"].Width = 50;
                unitGridView.Columns["Status"].Width = 55;
                unitGridView.Columns["Location"].Width = 40;
                unitGridView.Columns["Activity"].Width = 80;
                unitGridView.Columns["UnionCallSign"].Width = 50;
                unitGridView.Columns["Remove"].Width = 65;

                unitGridView.ReadOnly = true;
                foreach (DataGridViewColumn column in unitGridView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }
        }
        private void btnRemoveUnit_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != unitGridView.Columns["Remove"].Index) return;

            string callSignToRemove = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
            if (string.IsNullOrEmpty(callSignToRemove)) return;

            if (MessageBox.Show("Are you sure you want to remove this unit?", "Remove Unit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var unitToRemove = units.FirstOrDefault(unit => unit.CallSign == callSignToRemove);
                if (unitToRemove != null)
                {
                    units.Remove(unitToRemove);

                    // Remove the row directly from the grid
                    foreach (DataGridViewRow row in unitGridView.Rows)
                    {
                        if (row.Cells["CallSign"].Value?.ToString() == callSignToRemove)
                        {
                            unitGridView.Rows.Remove(row);
                            break;
                        }
                    }

                    // Clear and reapply the active grid logic
                    RemovefromActiveGrid(callSignToRemove);

                    // Refresh the grid to ensure proper redrawing
                    unitGridView.ClearSelection();
                    unitGridView.Refresh();
                }
            }
        }
        private void RemovefromActiveGrid(string callSignToRemove)
        {
            var activeRowToRemove = activeGridView.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => row.Cells["CallSign"].Value?.ToString() == callSignToRemove);
            if (activeRowToRemove != null)
                activeGridView.Rows.Remove(activeRowToRemove);

            // Refresh the active grid after removal
            activeGridView.Refresh();
        }
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private HashSet<string> announcedUnits = new HashSet<string>();

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimerPanel();

            // Iterate over the timers
            foreach (var kvp in onSceneTimers)
            {
                string callSign = kvp.Key;
                TimeSpan elapsedTime = DateTime.Now - kvp.Value;

                // Check if the timer exceeds 5 seconds and hasn't been announced yet
                if (elapsedTime >= TimeSpan.FromSeconds(5) && !announcedUnits.Contains(callSign))
                {
                    string text = $"Check Status for {callSign}";
                    string processedText = ReplaceLettersInCallsign(text);

                    // Announce and mark the unit as announced
                    synthesizer.SpeakAsync(processedText);
                    announcedUnits.Add(callSign); // Add only here after announcing
                }
            }
        }

        static string ReplaceLettersInCallsign(string text)
        {
            // Define replacements for individual letters
            var replacements = new System.Collections.Generic.Dictionary<char, string>
            {
                { 'L', "Lincoln" },
                { 'C', "Charlie" },
                { 'P', "Paul" },
                { 'H', "Henry" },
                { 'M', "Mike" },
                { 'F', "Foxtrot" },
                { 'D', "Delta" },
                { 'V', "Victor" },
                { 'E', "Edward" },
                // Add more replacements if needed
            };

            // Find the "on" keyword and extract the callSign part
            int index = text.LastIndexOf("for ");
            if (index == -1 || index + 3 >= text.Length)
            {
                // If "on" not found or no callSign after it, return text unchanged
                return text;
            }

            string prefix = text.Substring(0, index + 3); // "Check Status on "
            string callSign = text.Substring(index + 3); // Extract "Lincoln-201"

            // Process the callSign
            StringBuilder result = new StringBuilder();
            foreach (char currentChar in callSign)
            {
                // Replace letters as defined in the replacements dictionary
                if (char.IsLetter(currentChar) && replacements.ContainsKey(char.ToUpper(currentChar)))
                {
                    result.Append(replacements[char.ToUpper(currentChar)]);
                }
                else
                {
                    // Append non-letters as they are
                    result.Append(currentChar);
                }
            }

            return prefix + result.ToString(); // Combine prefix and processed callSign
        }



        // Update the timer panel with elapsed time for units on scene
        private void UpdateTimerPanel()
        {
            timerPanel.Controls.Clear();

            int xOffset = 5;
            int yOffset = 5;
            int maxHeight = 103;
            int labelHeight = 25;
            int labelWidth = 200;

            int maxLabelsPerColumn = maxHeight / labelHeight;
            int currentLabelCount = 0;

            foreach (var kvp in onSceneTimers)
            {
                string callSign = kvp.Key;
                TimeSpan elapsedTime = DateTime.Now - kvp.Value;

                if (currentLabelCount >= maxLabelsPerColumn)
                {
                    yOffset = 5;
                    xOffset += labelWidth;
                    currentLabelCount = 0;
                }

                Label label = new Label()
                {
                    Text = $"{callSign} status timer: {elapsedTime:mm\\:ss}",
                    ForeColor = Color.White,
                    Font = new Font("Impact", 12),
                    AutoSize = true,
                    Location = new Point(xOffset, yOffset)
                };

                timerPanel.Controls.Add(label);
                yOffset += labelHeight;
                currentLabelCount++;
            }
        }

        // Handle grid formatting
        private void unitGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && unitGridView.Columns[e.ColumnIndex].Name == "Department")
            {
                string department = e.Value?.ToString();
                switch (department)
                {
                    case "LSPD":
                        e.CellStyle.BackColor = Color.DarkBlue;
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.SelectionForeColor = Color.White;
                        break;
                    case "LCSO":
                        e.CellStyle.BackColor = Color.Brown;
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.SelectionForeColor = Color.White;
                        break;
                    case "SAHP":
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionForeColor = Color.Black;
                        break;
                    case "PBPD":
                        e.CellStyle.BackColor = Color.Gold ;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionForeColor = Color.Black;
                        break;
                    case "SADOT":
                        e.CellStyle.BackColor = Color.Orange;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionForeColor = Color.Black;
                        break;
                }
                
            }
        }

        // Handle cell value changes
        private void unitGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                string status = unitGridView.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Remove from activeGridView if status is inactive
                    if (status == "Unavailable" || status == "Busy" || status == "Off Duty")
                    {
                        RemovefromActiveGrid(callSign);
                    }
                    else if (status == "Available" || status == "En Route")
                    {
                        AddToActiveGridView(callSign);
                    }

                    // Handle "On Scene" status and timer
                    if (status == "On Scene")
                    {
                        if (!onSceneTimers.ContainsKey(callSign))
                        {
                            onSceneTimers[callSign] = DateTime.Now;
                        }

                        // Ensure the unit is added to activeGridView if missing
                        AddToActiveGridView(callSign);
                    }
                    else
                    {
                        // Remove timer if status changes away from "On Scene"
                        if (onSceneTimers.ContainsKey(callSign))
                        {
                            announcedUnits.Remove(callSign);
                            onSceneTimers.Remove(callSign);
                        }
                    }

                    // Refresh the activeGridView after changes
                    activeGridView.Refresh();
                }
            }
        }

        // Handle grid state changes
        private void unitGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (unitGridView.IsCurrentCellDirty)
            {
                unitGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void StyleUnitGridView()
        {
            // Set the background color for the grid
            unitGridView.BackgroundColor = Color.FromArgb(64, 64, 64);
            unitGridView.EnableHeadersVisualStyles = false;

            // Customize the header row appearance
            unitGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            unitGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Impact", 12);

            // Customize default row appearance
            unitGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.DefaultCellStyle.ForeColor = Color.White;
            unitGridView.DefaultCellStyle.Font = new Font("Impact", 12);
            unitGridView.RowTemplate.Height = 30;

            // Set alternating row color (the rows with index 1, 3, 5, etc. will have a different color)
            unitGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80); // Slightly lighter gray
            unitGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            // Customize row header appearance
            unitGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            // Set the grid to select rows only, not columns or headers
            unitGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            unitGridView.MultiSelect = false; // Only select one row at a time

            // Disable the highlight on column headers
            unitGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64); // Avoid blue highlight
            unitGridView.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;

            // Disable row resizing
            unitGridView.AllowUserToResizeRows = false;
            unitGridView.AllowUserToResizeColumns = false;
            unitGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

        }


        private void StyleActiveGridView()
        {
            activeGridView.BackgroundColor = Color.FromArgb(64, 64, 64);
            activeGridView.EnableHeadersVisualStyles = false;
            activeGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            activeGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            activeGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            activeGridView.DefaultCellStyle.ForeColor = Color.White;
            activeGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            activeGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            activeGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            activeGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            activeGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            activeGridView.MultiSelect = false;
            activeGridView.AllowUserToResizeRows = false;
            activeGridView.ReadOnly = true;
        }
    }
}
