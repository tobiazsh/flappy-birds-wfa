using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Birds_WFA
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static Game Instance = new Game();

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



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
