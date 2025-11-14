using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Birds_WFA
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.SkyBlue; // Mimic Sky
            this.KeyDown += Game_KeyDown;
            this.FormClosed += (s, args) => MainWindow.NavigateToMenuHandler(s, args);

            InitializeComponents();
        }

        // Components

        Label haltedInfoLabel = new Label();
        FlowLayoutPanel rootPanel = new FlowLayoutPanel();
        private void InitializeComponents()
        {
            rootPanel.Parent = this;
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.FlowDirection = FlowDirection.TopDown;
            rootPanel.WrapContents = true;
            rootPanel.AutoScroll = false;

            haltedInfoLabel.Text = $"Game is halted. Press any key to continue and {Keys.Menu.ToString()} to halt again!";
            haltedInfoLabel.DataBindings.Add("Visible", Game.Instance, "IsHalted", true, DataSourceUpdateMode.OnPropertyChanged, true, "");
            haltedInfoLabel.Font = Globals.TitleFont;
            haltedInfoLabel.AutoSize = true;
            
            rootPanel.Controls.Add(haltedInfoLabel);

        }

        private void Game_KeyDown(object? sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Pause) // TODO: Change key to something else that's actually recognised
                Game.Instance.IsHalted = true; // Halt the game on Pause key

            if (Game.Instance.IsHalted)
                Game.Instance.IsHalted = false; // Unhalt on any key press
        }
    }
}
