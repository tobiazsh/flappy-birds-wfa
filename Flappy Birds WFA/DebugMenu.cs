using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flappy_Birds_WFA.Utils;
using Microsoft.VisualBasic;

namespace Flappy_Birds_WFA
{
    public partial class DebugMenu : Form
    {
        public DebugMenu()
        {
            InitializeComponent();
            this.Text = "DEBUG MENU";
        }

        private void DebugMenu_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InitializeControls();
        }

        private FlowLayoutPanel rootPanel = new FlowLayoutPanel();

        private Button SetHighscoreButton = new Button();
        private void InitializeControls()
        {
            rootPanel.Parent = this;
            
            SetHighscoreButton.Parent = rootPanel;
            SetHighscoreButton.Click += SetHighscoreButton_Click;
            SetHighscoreButton.AutoSize = true;
            SetHighscoreButton.Text = "Set Highscore";
            rootPanel.Controls.Add(SetHighscoreButton);
        }

        private void SetHighscoreButton_Click(object? sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter new highscore:", "Set Highscore", "0");

            if (int.TryParse(input, out int newHighscore))
                Achievements.Instance.Highscore = newHighscore;
            else
                MessageBox.Show("Invalid input. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
