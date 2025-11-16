using System.ComponentModel;

namespace Flappy_Birds_WFA
{
    public partial class DebugConsole : Form
    {

        public static readonly DebugConsole Instance = new DebugConsole();

        private readonly RichTextBox rtbConsole = new RichTextBox();
        private readonly Button clearBtn = new Button();
        private readonly TextBox txtCommand = new TextBox();
        private readonly Button sendBtn = new Button();

        public event Action<string>? CommandEntered;

        public DebugConsole()
        {
            InitializeComponent();

            Text = "Debug Console";
            Size = new Size(600, 400);

            rtbConsole.Dock = DockStyle.Fill;
            rtbConsole.ReadOnly = true;
            rtbConsole.BackColor = Color.Black;
            rtbConsole.ForeColor = Color.LightGreen;
            rtbConsole.Font = new Font("Consolas", 10);
            rtbConsole.HideSelection = false;

            txtCommand.Dock = DockStyle.Fill;
            txtCommand.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendCommand();
                }
            };

            sendBtn.Text = "Send";
            sendBtn.Dock = DockStyle.Right;
            sendBtn.Width = 60;
            sendBtn.Click += (s, e) => SendCommand();

            clearBtn.Text = "Clear";
            clearBtn.Dock = DockStyle.Right;
            clearBtn.Width = 60;
            clearBtn.Click += (s, e) => ClearConsole();

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                Padding = new Padding(5)
            };

            bottomPanel.Controls.Add(txtCommand);
            bottomPanel.Controls.Add(sendBtn);
            bottomPanel.Controls.Add(clearBtn);
            Controls.Add(bottomPanel);

            Controls.Add(rtbConsole);
            Controls.Add(bottomPanel);

            FormClosing += (s, e) =>
            {
                e.Cancel = true;
                Hide();
            };
        }

        public void SendCommand()
        {
            var cmd = txtCommand.Text?.Trim();
            if (string.IsNullOrEmpty(cmd)) return;

            // Echo command in console and notify listeners
            AddLine($"> {cmd}");
            CommandEntered?.Invoke(cmd);
            txtCommand.Clear();
        }

        public void ClearConsole()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ClearConsole));
                return;
            }

            rtbConsole.Clear();
        }

        public void AddLine(string line)
        {
            if (rtbConsole.InvokeRequired)
            {
                rtbConsole.Invoke(new Action(() => AddLine(line)));
            }
            else
            {
                rtbConsole.AppendText(line + Environment.NewLine);
                rtbConsole.ScrollToCaret();
            }
        }
    }
}
