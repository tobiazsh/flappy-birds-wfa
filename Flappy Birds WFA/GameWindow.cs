using Flappy_Birds_WFA.Utils;

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
            this.Text = "Flappy Birds";

            Game.Instance.Initialize(this); // Initialize Game Instance with this window

            InitializeComponents();
        }

        // Components

        Label haltedInfoLabel = new Label();
        Label gameOverLabel = new Label();
        private void InitializeComponents()
        {
            haltedInfoLabel.Text = $"Game is halted. Press any key to continue and {Keys.Pause.ToString()} to halt again!";
            haltedInfoLabel.DataBindings.Add("Visible", Game.Instance, "IsHalted", true, DataSourceUpdateMode.OnPropertyChanged, true, "");
            haltedInfoLabel.Font = Globals.TitleFont;
            haltedInfoLabel.AutoSize = true;
            haltedInfoLabel.Parent = this;

            // Game over label
            var scoreBinding = new Binding("Text", Game.Instance, "Score", true, DataSourceUpdateMode.OnPropertyChanged, 0);

            scoreBinding.Format += (sender, e) =>
            {
                e.Value = $"Game Over! Your Score: {e.Value}. Press any key to restart.";
            };
            
            scoreBinding.BindingComplete += (s, e) =>
            {
                if (e.BindingCompleteState == BindingCompleteState.Success)
                    RecenterGameOverLabel();
            };

            gameOverLabel.DataBindings.Add(scoreBinding);

            var visibilityBinding = new Binding("Visible", Game.Instance, "IsGameOver", true, DataSourceUpdateMode.OnPropertyChanged, false, "");
            
            visibilityBinding.BindingComplete += (s, e) =>
            {
                if (e.BindingCompleteState == BindingCompleteState.Success)
                {
                    if (gameOverLabel.Visible)
                        RecenterGameOverLabel();
                }
            };

            gameOverLabel.DataBindings.Add(visibilityBinding);
            gameOverLabel.Font = Globals.TitleFont;
            gameOverLabel.AutoSize = true;
            gameOverLabel.Location = new Point((this.ClientSize.Width - gameOverLabel.Width) / 2, (this.ClientSize.Height - gameOverLabel.Height) / 2);
            gameOverLabel.Parent = this;
        }

        private void RecenterGameOverLabel()
        {
            Console.WriteLine("Recentering Game Over Label");

            gameOverLabel.PerformLayout();
            int x = Math.Max(0, (this.ClientSize.Width - gameOverLabel.Width) / 2);
            int y = Math.Max(0, (this.ClientSize.Height - gameOverLabel.Height) / 2);
            gameOverLabel.Location = new Point(x, y);
            gameOverLabel.BringToFront();
            this.Invalidate();
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

            if (Game.Instance.IsHalted && !Game.Instance.IsGameOver)
            {
                Game.Instance.IsHalted = false; // Unhalt on any key press
                return;
            }

            if (Game.Instance.IsGameOver)
            {
                Game.Instance.Reset();
                Game.Instance.Initialize(this);
                this.Invalidate(); // Redraw
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Game.Instance.GameLoop(this, e);
            base.OnPaint(e);
        }
    }
}
