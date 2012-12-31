using System;
using System.Windows.Forms;
using IntelligentLevelEditor.Games;
using IntelligentLevelEditor.Games.Crashmo;
using IntelligentLevelEditor.Games.DenpaMen;
using IntelligentLevelEditor.Games.FreakyForms;
using IntelligentLevelEditor.Games.Pushmo;
using IntelligentLevelEditor.Games.Pyramids;
using IntelligentLevelEditor.Games.HexEditor;

namespace IntelligentLevelEditor
{
    public partial class GameSelect : Form
    {
        public enum GameMode
        {
            Pushmo,
            Crashmo,
            Pyramids,
            FreakyForms,
            DenpaMen,
            HexEditor,
            Unknown
        }

        public static GameMode DetectGame(byte[] data)
        {
            if (Pushmo.IsMatchingData(data))
                return GameMode.Pushmo;
            if (Crashmo.IsMatchingData(data))
                return GameMode.Crashmo;
            if (Pyramids.IsMatchingData(data))
                return GameMode.Pyramids;
            if (FreakyForms.IsMatchingData(data))
                return GameMode.FreakyForms;
            if (DenpaMen.IsMatchingData(data))
                return GameMode.DenpaMen;
            if (HexEditor.IsMatchingData(data))
                return GameMode.HexEditor;
            return GameMode.Unknown;
        }

        public static IStudio GetStudio(GameMode mode, StatusStrip strip)
        {
            switch (mode)
            {
                case GameMode.Pushmo:
                    return new PushmoStudio(strip) { Dock = DockStyle.Fill };
                case GameMode.Crashmo:
                    return new CrashmoStudio(strip) { Dock = DockStyle.Fill };
                case GameMode.Pyramids:
                    return new PyramidsStudio(strip) { Dock = DockStyle.Fill };
                case GameMode.FreakyForms:
                    return new FreakyFormsStudio { Dock = DockStyle.Fill };
                case GameMode.DenpaMen:
                    return new DenpaMenStudio { Dock = DockStyle.Fill };
                case GameMode.HexEditor:
                    return new HexEditor { Dock = DockStyle.Fill };
            }
            return null;
        }

        public GameMode SelectedGame { get; private set; }

        public GameSelect()
        {
            InitializeComponent();
        }

        private void btnPushmo_Click(object sender, EventArgs e)
        {
            SelectedGame = GameMode.Pushmo;
        }

        private void btnCrashmo_Click(object sender, EventArgs e)
        {
            SelectedGame = GameMode.Crashmo;
        }

        private void btnPyramids_Click(object sender, EventArgs e)
        {
            SelectedGame = GameMode.Pyramids;
        }

        private void btnFreakyForms_Click(object sender, EventArgs e)
        {
            SelectedGame = GameMode.FreakyForms;
        }

        private void btnDenpaMen_Click(object sender, EventArgs e)
        {
            SelectedGame = GameMode.DenpaMen;
        }
    }
}
