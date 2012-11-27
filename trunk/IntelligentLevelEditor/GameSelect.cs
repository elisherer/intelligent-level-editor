using System;
using System.Windows.Forms;
using IntelligentLevelEditor.Games;

namespace IntelligentLevelEditor
{
    public partial class GameSelect : Form
    {
        private StudioManager.GameMode _game;

        public StudioManager.GameMode SelectedGame
        { 
            set { _game = value; }
            get { return _game; }
        }

        public GameSelect()
        {
            InitializeComponent();
        }

        private void btnPushmo_Click(object sender, EventArgs e)
        {
            _game = StudioManager.GameMode.Pushmo;
        }

        private void btnCrashmo_Click(object sender, EventArgs e)
        {
            _game = StudioManager.GameMode.Crashmo;
        }
    }
}
