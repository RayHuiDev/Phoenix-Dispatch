﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        // COLUMNS
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


        // INITIALIZERS
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
            callSign.TextChanged += callSign_TextChanged;
            name.KeyDown += TextBox_KeyDown;
            unitGridView.CellFormatting += unitGridView_CellFormatting;
            unitGridView.CellValueChanged += unitGridView_CellValueChanged;
            unitGridView.CurrentCellDirtyStateChanged += unitGridView_CurrentCellDirtyStateChanged;
            unitGridView.CellClick += unitGridView_CellClick;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            pictureBox2.MouseDown += PictureBox_MouseDown;
            pictureBox2.MouseMove += PictureBox_MouseMove;
            pictureBox2.MouseUp += PictureBox_MouseUp;
            pictureBox2.Paint += PictureBox_Paint;

            SetInitialMapPosition(new Point(2941, 6311));

        }
        // DEPARTMENT DROPDOWN PREFIX
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem?.ToString();
            label4.AutoSize = false;
            label4.Size = new Size(30, 28);

            if (selectedValue == "LSPD")
            {
                label4.Text = "L-";
            }
            else if (selectedValue == "PBPD")
            {
                label4.Text = "P-";
            }
            else if (selectedValue == "LCSO")
            {
                label4.Text = "C-";
            }
            else if (selectedValue == "SAHP")
            {
                label4.Text = "H-";
            }
            else if (selectedValue == "SAMS")
            {
                label4.Text = "E-";
            }
            else if (selectedValue == "SADOT")
            {
                label4.Text = "D-";
            }
        }



        //TEXT FIELD FOR NOTES AND LOCATION
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
                System.Windows.Forms.TextBox inputBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 200 };

                System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 350, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
            }
        }



        //FORMS
        private int selectedRowIndex;

        private void UnionCallSign()
        {
            selectedRowIndex = unitGridView.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                string callSign = unitGridView.Rows[selectedRowIndex].Cells["CallSign"].Value?.ToString();
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
                            unitGridView.Rows[selectedRowIndex].Cells["UnionCallSign"].Value = selectedUnionCallSign;
                            unit.UnionCallSign = selectedUnionCallSign; // Update the units list
                            unitGridView.ClearSelection();
                            unitGridView.Refresh();
                        }
                    }
                }

            }
        }
        private void Notes()
        {
            selectedRowIndex = unitGridView.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                string callSign = unitGridView.Rows[selectedRowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Prompt the user to enter the notes
                    string notes = Prompt.ShowDialog("Enter the notes", "Notes");

                    // Update the "Notes" field with the entered text
                    unitGridView.Rows[selectedRowIndex].Cells["Notes"].Value = notes;
                    unit.Notes = notes; // Update the units list
                    unitGridView.ClearSelection();
                    unitGridView.Refresh();

                }
            }
        }


        private Dictionary<string, Point> postalCoordinates = new Dictionary<string, Point>
        {
            { "5022", new Point(2922, 4764) },
            {"8090", new Point(2086, 6300) },
            {"8013", new Point(2053, 6255) },
        };

        private List<Point> currentMarkers = new List<Point>();

        private bool isDragging = false;
        private Point lastMousePosition;
        private Point imageOffset = new Point(0, 0); // Image position inside the PictureBox

        // Paint event to display the image with the offset and zoom
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                e.Graphics.Clear(pictureBox2.BackColor);

                // Calculate the scaled image size based on the zoom factor
                int scaledWidth = pictureBox2.Image.Width;
                int scaledHeight = pictureBox2.Image.Height;
                // Draw the image using the new scaled size and offset
                e.Graphics.DrawImage(pictureBox2.Image, imageOffset.X, imageOffset.Y, scaledWidth, scaledHeight);
            }
        }

        // Mouse Down - Start dragging
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.Location;
        }

        // Mouse Move - Update offset during dragging
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Calculate movement
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;

                // Update image offset to move the image
                imageOffset.X += deltaX;
                imageOffset.Y += deltaY;

                // Clamp the offset to prevent the image from going outside the PictureBox bounds
                int maxOffsetX = 0;
                int maxOffsetY = 0;

                // Ensure the image does not go outside the left/top boundaries
                if (imageOffset.X > maxOffsetX)
                    imageOffset.X = maxOffsetX;
                if (imageOffset.Y > maxOffsetY)
                    imageOffset.Y = maxOffsetY;

                // Ensure the image does not go outside the right/bottom boundaries
                if (imageOffset.X < pictureBox2.Width - pictureBox2.Image.Width)
                    imageOffset.X = pictureBox2.Width - pictureBox2.Image.Width;
                if (imageOffset.Y < pictureBox2.Height - pictureBox2.Image.Height)
                    imageOffset.Y = pictureBox2.Height - pictureBox2.Image.Height;

                lastMousePosition = e.Location;

                // Redraw image with the new position
                pictureBox2.Invalidate();
            }
        }

        // Mouse Up - Stop dragging
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // Move the map to center on the selected location
        private void CenterMapOnLocation(Point location)
        {
            if (pictureBox2.Image == null) return;

            int formCenterX = pictureBox2.Width / 2;
            int formCenterY = pictureBox2.Height / 2;

            // Adjust image offset so that the location appears at the center, considering zoom
            imageOffset.X = formCenterX - location.X;
            imageOffset.Y = formCenterY - location.Y;

            // Clamp the offset to prevent the image from going outside the PictureBox bounds
            int maxOffsetX = 0;
            int maxOffsetY = 0;

            // Ensure the image does not go outside the left/top boundaries
            if (imageOffset.X > maxOffsetX)
                imageOffset.X = maxOffsetX;
            if (imageOffset.Y > maxOffsetY)
                imageOffset.Y = maxOffsetY;

            // Ensure the image does not go outside the right/bottom boundaries
            if (imageOffset.X < pictureBox2.Width - pictureBox2.Image.Width)
                imageOffset.X = pictureBox2.Width - pictureBox2.Image.Width;
            if (imageOffset.Y < pictureBox2.Height - pictureBox2.Image.Height)
                imageOffset.Y = pictureBox2.Height - pictureBox2.Image.Height;

            // Refresh to apply changes
            pictureBox2.Invalidate();
        }


        private void SetInitialMapPosition(Point initialLocation)
        {
            if (pictureBox2.Image == null) return;

            int formCenterX = pictureBox2.Width / 2;
            int formCenterY = pictureBox2.Height / 2;

            imageOffset.X = formCenterX - initialLocation.X;
            imageOffset.Y = formCenterY - initialLocation.Y;

            // Redraw to apply changes
            pictureBox2.Invalidate();
        }

        private void Locations()
        {
            selectedRowIndex = unitGridView.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                string callSign = unitGridView.Rows[selectedRowIndex].Cells["CallSign"].Value?.ToString();
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return;

                if (!string.IsNullOrEmpty(callSign))
                {
                    // Prompt the user to enter the location
                    string location = Prompt.ShowDialog("Enter the location", "Location");

                    // Remove the old marker associated with the previous location (if it exists)
                    if (unit.Location != "Unknown" && postalCoordinates.ContainsKey(unit.Location))
                    {
                        currentMarkers.RemoveAll(m => m.Equals(postalCoordinates[unit.Location]));
                    }

                    // If the location exists in the postal coordinates, move the marker
                    if (postalCoordinates.TryGetValue(location, out Point mapCoordinates))
                    {
                        // Add the new marker location
                        currentMarkers.Add(mapCoordinates);

                        // Update the DataGridView and unit list
                        unitGridView.Rows[selectedRowIndex].Cells["Location"].Value = location;
                        unit.Location = location;

                        // Center map on new location and refresh markers
                        DrawMarkersOnMap();
                        CenterMapOnLocation(mapCoordinates);
                    }
                    else
                    {
                        // If the location is cleared or invalid, remove the marker
                        if (!string.IsNullOrEmpty(unit.Location) && postalCoordinates.ContainsKey(unit.Location))
                        {
                            currentMarkers.RemoveAll(m => m.Equals(postalCoordinates[unit.Location]));
                        }

                        // Clear the image and redraw without the marker
                        pictureBox2.Image = new Bitmap(pictureBox2.Image);
                        pictureBox2.Refresh();

                        // Clear location and refresh grid
                        unitGridView.Rows[selectedRowIndex].Cells["Location"].Value = location;
                        unit.Location = location;
                    }

                    unitGridView.ClearSelection();
                    unitGridView.Refresh();
                }
            }
        }

        private void DrawMarkersOnMap()
        {
            if (pictureBox2.Image == null) return;

            // Create a copy of the image to draw on
            Bitmap tempMap = new Bitmap(pictureBox2.Image);

            using (Graphics g = Graphics.FromImage(tempMap))
            {
                // Draw each marker based on currentMarkers
                foreach (var markerLocation in currentMarkers)
                {
                    Brush markerBrush = new SolidBrush(Color.Red);
                    g.FillEllipse(markerBrush, markerLocation.X - 5, markerLocation.Y - 5, 10, 10); // Draw a small red dot
                }
            }

            // Update the PictureBox with the modified image
            pictureBox2.Image = tempMap;
            pictureBox2.Refresh();
        }



        private void Activity()
        {
            selectedRowIndex = unitGridView.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                string callSign = unitGridView.Rows[selectedRowIndex].Cells["CallSign"].Value?.ToString();
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
                            unitGridView.Rows[selectedRowIndex].Cells["Activity"].Value = selectedActivity;
                            unit.Activity = selectedActivity; // Update the units list
                            unitGridView.ClearSelection();
                            unitGridView.Refresh();
                        }
                    }
                }
            }
        }
        private void Status()
        {
            selectedRowIndex = unitGridView.CurrentCell.RowIndex;
            if (selectedRowIndex >= 0)
            {
                // Get the call sign of the selected row
                string callSign = unitGridView.Rows[selectedRowIndex].Cells["CallSign"].Value?.ToString();

                // Find the unit in the list using the call sign
                var unit = units.FirstOrDefault(u => u.CallSign == callSign);
                if (unit == null) return; // If no matching unit, return

                // Open the StatusForm to select a new status
                using (StatusForm statusForm = new StatusForm())
                {
                    if (statusForm.ShowDialog() == DialogResult.OK)
                    {
                        var selectedStatus = statusForm.SelectedStatus;
                        // Update the grid and the unit's status
                        unitGridView.Rows[selectedRowIndex].Cells["Status"].Value = selectedStatus;
                        unit.Status = selectedStatus; // Update the units list
                        unitGridView.ClearSelection();
                        unitGridView.Refresh();
                    }
                }
            }
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)
            {
                Status(); 
            }
            if (e.KeyCode == Keys.NumPad1)
            {
                Locations(); 
            }
            if (e.KeyCode == Keys.NumPad2)
            {
                Notes(); 
            }
            if (e.KeyCode == Keys.NumPad3)
            {
                Activity();
            }
            if (e.KeyCode == Keys.NumPad4)
            {
                UnionCallSign();
            }
        }


        private void unitGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 3:
                    Status();
                    break;
                case 4:
                    Locations();
                    break;
                case 5:
                    Notes();
                    break;
                case 6:
                    Activity();
                    break;
                case 7:
                    UnionCallSign();
                    break; 
            }
        }



        private void callSign_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = callSign.SelectionStart;
            callSign.Text = callSign.Text.ToUpper();
            callSign.SelectionStart = cursorPosition;
        }

        private void CapitalizeWords()
        {
            string input = name.Text;
            string capitalizedText = string.Join(" ", input.Split(' ')
                                                      .Select(word =>
                                                          word.Length > 0 ?
                                                          char.ToUpper(word[0]) + word.Substring(1).ToLower() : word));
            name.Text = capitalizedText;
        }
        private void UpdateInput()
        {
            string input = callSign.Text;

            string digits = new string(input.Where(char.IsDigit).ToArray());
            string letters = new string(input.Where(char.IsLetter).ToArray());

            digits = digits.PadLeft(3, '0');

            string formattedInput = digits + letters;
            callSign.Text = formattedInput.ToUpper();
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            UpdateInput();
            CapitalizeWords();
            string CallSign = callSign.Text.Trim();
            string nameText = name.Text.Trim();
            string department = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(CallSign))
            {
                MessageBox.Show("Call Sign is required!");
                return;
            }else if(string.IsNullOrEmpty(nameText))
            {
                MessageBox.Show("Name is required!");
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

            units.Add(newUnit);
            unitGridView.Rows.Add(newUnit.CallSign, newUnit.Name, newUnit.Department, newUnit.Status, newUnit.Location, newUnit.Notes, newUnit.Activity, newUnit.UnionCallSign);
            callSign.Text = "";
            name.Text = "";

            AddToActiveGridView(callprefix + CallSign);
            

            unitGridView.Sort(unitGridView.Columns["CallSign"], System.ComponentModel.ListSortDirection.Ascending);
            unitGridView.ClearSelection();
            unitGridView.Refresh();
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

                    foreach (DataGridViewRow row in unitGridView.Rows)
                    {
                        if (row.Cells["CallSign"].Value?.ToString() == callSignToRemove)
                        {
                            unitGridView.Rows.Remove(row);
                            announcedUnits.Remove(callSignToRemove);
                            onSceneTimers.Remove(callSignToRemove);
                            break;
                        }
                    }
                    RemovefromActiveGrid(callSignToRemove);
                    unitGridView.ClearSelection();
                    unitGridView.Refresh();
                }
            }
        }


        // Handle Enter key to add unit
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateInput();
                e.SuppressKeyPress = true;
                btnAddUnit_Click(sender, e);
                callSign.Focus();
            }
        }
        //For Department Selection
        private void StyleComboBox()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }




        // ADD TO ACTIVE
        private void AddToActiveGridView(string callSign)
        {
            if (!activeGridView.Rows.Cast<DataGridViewRow>().Any(row => row.Cells["CallSign"].Value?.ToString() == callSign))
            {
                activeGridView.Rows.Add(callSign);
                activeGridView.Sort(activeGridView.Columns["CallSign"], System.ComponentModel.ListSortDirection.Ascending);
            }
            activeGridView.Refresh();
        }
        // REMOVE FROM ACTIVE
        private void RemovefromActiveGrid(string callSignToRemove)
        {
            var activeRowToRemove = activeGridView.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => row.Cells["CallSign"].Value?.ToString() == callSignToRemove);
            if (activeRowToRemove != null)
                activeGridView.Rows.Remove(activeRowToRemove);

            activeGridView.Refresh();
        }






        // INITIALIZE ACTIVE GRID
        private void InitializeActiveGridView()
        {
            activeGridView.Columns.Add("CallSign", "Call Sign");
            activeGridView.Columns["CallSign"].SortMode = DataGridViewColumnSortMode.NotSortable;
            activeGridView.AllowUserToAddRows = false;
            activeGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            activeGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            activeGridView.ColumnHeadersVisible = false;
            activeGridView.RowHeadersVisible = false;
            activeGridView.DefaultCellStyle.Font = new Font("Segoe UI", 12);

            foreach (DataGridViewColumn column in activeGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // INITIALIZE UNIT GRID
        private void InitializeUnitGridView()
        {
            if (unitGridView.Columns.Count == 0) 
            {
                unitGridView.Columns.Add("CallSign","CallSign");
                unitGridView.Columns.Add("Name", "Name");
                unitGridView.Columns.Add("Department", "Department");
                unitGridView.Columns.Add("Status","Status");
                unitGridView.Columns.Add("Location", "Location");
                unitGridView.Columns.Add("Notes", "Notes");
                unitGridView.Columns.Add("Activity", "Activity");
                unitGridView.Columns.Add("UnionCallSign", "Union");

                var btnRemove = new DataGridViewButtonColumn
                {
                    Name = "Remove",
                    HeaderText = "",
                    Text = "Delete",
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







        // TIMER & VOICE
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private HashSet<string> announcedUnits = new HashSet<string>();

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimerPanel();

            foreach (var kvp in onSceneTimers)
            {
                string callSign = kvp.Key;
                TimeSpan elapsedTime = DateTime.Now - kvp.Value;

                if (elapsedTime >= TimeSpan.FromSeconds(120) && !announcedUnits.Contains(callSign))
                {
                    string text = $"Check Status for {callSign}";
                    string processedText = ReplaceLettersInCallsign(text);
                    synthesizer.SpeakAsync(processedText);
                    announcedUnits.Add(callSign);
                }
            }
        }

        static string ReplaceLettersInCallsign(string text)
        {
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
                { 'K', "Kilo" }
            };
            int index = text.LastIndexOf("for ");
            if (index == -1 || index + 3 >= text.Length)
            {
                return text;
            }

            string prefix = text.Substring(0, index + 3);
            string callSign = text.Substring(index + 3);

            StringBuilder result = new StringBuilder();
            foreach (char currentChar in callSign)
            {
                if (char.IsLetter(currentChar) && replacements.ContainsKey(char.ToUpper(currentChar)))
                {
                    result.Append(replacements[char.ToUpper(currentChar)]);
                }
                else
                {
                    result.Append(currentChar);
                }
            }

            return prefix + result.ToString();
        }
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
                    Font = new Font("Segoe UI", 12),
                    AutoSize = true,
                    Location = new Point(xOffset, yOffset)
                };

                timerPanel.Controls.Add(label);
                yOffset += labelHeight;
                currentLabelCount++;
            }
        }



        // DEPARTMENT COLORS & ACTIVITY COLORS
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
            if (e.RowIndex >= 0 && unitGridView.Columns[e.ColumnIndex].Name == "Status")
            {
                string department = e.Value?.ToString();
                switch (department)
                {
                    case "Available":
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionForeColor = Color.Transparent;
                    break;
                }
            }
        }



        // ADD AND REMOVE FROM ACTIVE GRID AND SCENE TIMER
        private void unitGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string callSign = unitGridView.Rows[e.RowIndex].Cells["CallSign"].Value?.ToString();
                string status = unitGridView.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

                if (!string.IsNullOrEmpty(callSign))
                {
                    if (status == "Unavailable" || status == "Busy" || status == "Off Duty" || status == "Dispatch")
                    {
                        RemovefromActiveGrid(callSign);
                    }
                    else if (status == "Available" || status == "En Route" || status == "Pursuit")
                    {
                        AddToActiveGridView(callSign);
                    }
                    if (status == "On Scene")
                    {
                        if (!onSceneTimers.ContainsKey(callSign))
                        {
                            onSceneTimers[callSign] = DateTime.Now;
                        }
                        AddToActiveGridView(callSign);
                    }
                    else
                    {
                        if (onSceneTimers.ContainsKey(callSign))
                        {
                            announcedUnits.Remove(callSign);
                            onSceneTimers.Remove(callSign);
                        }
                    }
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




        //HANDLES THE STYLE OF THE FORMS
        private void StyleUnitGridView()
        {
            unitGridView.BackgroundColor = Color.FromArgb(64, 64, 64);
            unitGridView.EnableHeadersVisualStyles = false;
            unitGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            unitGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12);
            unitGridView.ColumnHeadersHeight = 40;
            unitGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.DefaultCellStyle.ForeColor = Color.White;
            unitGridView.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            unitGridView.RowTemplate.Height = 30;
            unitGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            unitGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            unitGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            unitGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            unitGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            unitGridView.MultiSelect = false;
            unitGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 64, 64);
            unitGridView.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void callSign_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
