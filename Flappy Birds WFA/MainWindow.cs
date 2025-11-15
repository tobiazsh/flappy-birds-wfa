using Flappy_Birds_WFA.Utils;

namespace Flappy_Birds_WFA
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.BackColor = Color.SkyBlue;
            this.KeyPreview = true;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
            this.Text = "Flappy Bird WFA";
            this.KeyDown += MainWindow_KeyDown;
            this.FormClosed += (s, ev) => Terminate();
        }

        // Controls
        TableLayoutPanel menuTable = new TableLayoutPanel();

        Label highscoreLabel = new Label();
        Label title = new Label();

        Button playButton = new Button();

        BindingSource achievementsBinding = new BindingSource();


        private void InitializeControls()
        {
            // Create table menu
            menuTable.Parent = this;
            menuTable.Dock = DockStyle.Fill;
            menuTable.ColumnCount = 1;
            menuTable.RowCount = 3;

            // Define table rows and columns
            menuTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Title
            menuTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Highscore
            menuTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // Buttons
            menuTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Create title label
            title.Text = "Flappy Birds - WFA Edition";
            title.AutoSize = true;
            title.Dock = DockStyle.Fill;
            title.Font = Globals.TitleFont;
            title.TextAlign = ContentAlignment.MiddleCenter;
            menuTable.Controls.Add(title, 0, 0);

            // Setup BindingSource
            achievementsBinding.DataSource = Achievements.Instance;
            achievementsBinding.ResetBindings(false);

            // Create highscore label
            var highscoreBinding = new Binding("Text", Achievements.Instance, "Highscore", true, DataSourceUpdateMode.OnPropertyChanged);

            highscoreBinding.Format += (sender, e) =>
            {
                e.Value = $"Highscore: {e.Value}";
            };

            highscoreLabel.DataBindings.Add(highscoreBinding);

            highscoreLabel.Font = Globals.TitleFont;
            highscoreLabel.AutoSize = true;
            highscoreLabel.Dock = DockStyle.Fill;
            highscoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            menuTable.Controls.Add(highscoreLabel, 0, 1);

            // Create play button
            playButton.Text = "Play";
            playButton.Font = Globals.DefaultFont;
            playButton.AutoSize = true;
            playButton.Padding = new Padding(20);
            playButton.Anchor = AnchorStyles.None;
            playButton.Click += (sender, e) => StartGame();
            menuTable.Controls.Add(playButton, 0, 2);
        }

        // OnClicks

        // Other

        private void StartGame()
        {
            this.Hide();
            new GameWindow().Show();
        }

        // Events

        private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.D && e.Control)
            {
                new DebugMenu().Show(this);
            }
        }

        public static void Terminate()
        {
            Application.Exit();
        }

        /// <summary>
        /// Is supposed to be implemented in a FormClosedEvent to navigate back to menu from other forms when they're closed.
        /// </summary>
        /// <param name="sender">Sending Form1</param>
        /// <param name="e">Handler</param>
        public static void NavigateToMenuHandler(object? sender, FormClosedEventArgs e)
        {
            var closingForm = sender as Form;
            if (closingForm == null) return;

            // Get all open forms EXCEPT the one that just closed
            var otherForms = Application.OpenForms
                .OfType<Form>()
                .Where(f => f != closingForm && !f.IsDisposed && f is not MainWindow)
                .ToArray();

            // Close all others
            foreach (var form in otherForms)
            {
                form.Close();
            }

            // Show main menu (create only if not already open)
            var menu = Application.OpenForms.OfType<MainWindow>().FirstOrDefault()
                       ?? new MainWindow();

            menu.Show();
            menu.BringToFront();
        }
    }
}
