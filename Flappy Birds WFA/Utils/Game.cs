using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA.Utils
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static Game Instance = new Game();

        // Fields
        // Components
        Floor floor1, floor2;

        // Constants
        private const int scroll = 2; // Floor movement speed


        public Game()
        {
            floor1 = new Floor()
                .SetBounds(Globals.GameWindowWidth, Floor.TEXTURE.Size.Height);

            floor2 = new Floor()
                .SetBounds(Globals.GameWindowWidth, Floor.TEXTURE.Size.Height);
        }

        public void Initialize(Form parent)
        {
            floor1
                .SetBounds(parent.ClientSize.Width, Floor.TEXTURE.Size.Height)
                .SetPosition(0, parent.ClientSize.Height - floor1.Height);

            floor2
                .SetBounds(parent.ClientSize.Width, Floor.TEXTURE.Size.Height)
                .SetPosition(floor1.Width, parent.ClientSize.Height - floor2.Height);
        }

        // Private Fields

        private int _score;
        private bool _isHalted = true; // Make game not immediately start

        // Properties
        public int Score
        {
            get => _score;
            set
            {
                if (_score == value) return;

                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool IsHalted
        {
            get => _isHalted;
            set
            {
                if (_isHalted == value) return;

                _isHalted = value;
                OnPropertyChanged(nameof(IsHalted));
            }
        }

        public void GameLoop(Form sender, PaintEventArgs e)
        {
            if (!IsHalted)
            {
                UpdateState(sender); // Allow update state only if not halted
                sender.Invalidate(); // Inform about update
            }

            Render(e); // Always render regardless of halted state
        }

        public void Render(PaintEventArgs e)
        {
            floor1.Draw(e);
            floor2.Draw(e);
        }

        public void UpdateState(Form sender)
        {
            floor1.SetPosition(floor1.X - scroll, floor1.Y);
            floor2.SetPosition(floor2.X - scroll, floor2.Y);

            if (floor1.X + floor1.Width <= 0)
            {
                floor1.SetPosition(floor2.X + floor2.Width, floor1.Y);
            }

            if (floor2.X + floor2.Width <= 0)
            {
                floor2.SetPosition(floor1.X + floor1.Width, floor2.Y);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
