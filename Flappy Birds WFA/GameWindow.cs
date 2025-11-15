using Flappy_Birds_WFA.Utils;
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
            this.Height = Globals.GameWindowHeight;
            this.Width = Globals.GameWindowWidth;
            this.DoubleBuffered = true; // Reduce flickering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Prevent resizing
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            Game.Instance.Initialize(this); // Initialize Game Instance with this window

            InitializeComponents();
        }

        // Components

        Label haltedInfoLabel = new Label();
        private void InitializeComponents()
        {
            haltedInfoLabel.Text = $"Game is halted. Press any key to continue and {Keys.Pause.ToString()} to halt again!";
            haltedInfoLabel.DataBindings.Add("Visible", Game.Instance, "IsHalted", true, DataSourceUpdateMode.OnPropertyChanged, true, "");
            haltedInfoLabel.Font = Globals.TitleFont;
            haltedInfoLabel.AutoSize = true;
            haltedInfoLabel.Parent = this;
        }

        private void Game_KeyDown(object? sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Pause)
            {
                Game.Instance.IsHalted = true; // Halt the game on Pause key
                return;
            }

            if (!Game.Instance.IsHalted && args.KeyCode == Keys.Space)
            {
                Game.Instance.Jump();
            }

            if (Game.Instance.IsHalted)
            {
                Game.Instance.IsHalted = false; // Unhalt on any key press
                return;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Game.Instance.GameLoop(this, e);
            base.OnPaint(e);
        }
    }
}
