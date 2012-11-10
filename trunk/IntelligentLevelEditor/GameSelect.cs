using System;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class GameSelect : Form
    {
        private FormEditor.GameMode _game;

        public FormEditor.GameMode SelectedGame { 
            set { _game = value; }
            get { return _game; }
        }

        public GameSelect()
        {
            InitializeComponent();
        }

        private void btnPushmo_Click(object sender, EventArgs e)
        {
            _game = FormEditor.GameMode.Pushmo;
        }

        private void btnCrashmo_Click(object sender, EventArgs e)
        {
            _game = FormEditor.GameMode.Crashmo;
        }
    }
}
