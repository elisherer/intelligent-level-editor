using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class GameSelect : Form
    {
        private string _game = string.Empty;

        public string SelectedGame { 
            set { _game = value; }
            get { return _game; }
        }

        public GameSelect()
        {
            InitializeComponent();
        }

        private void btnPushmo_Click(object sender, EventArgs e)
        {
            _game = "Pushmo";
        }

        private void btnCrashmo_Click(object sender, EventArgs e)
        {
            _game = "Crashmo";
        }
    }
}
