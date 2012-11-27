using System.Windows.Forms;
using IntelligentLevelEditor.Games.Crashmo;
using IntelligentLevelEditor.Games.Pushmo;

namespace IntelligentLevelEditor.Games
{
    public static class StudioManager
    {
        public enum GameMode
        {
            Pushmo,
            Crashmo,
            Pyramids,
            Unknown
        }

        public static GameMode DetectGame(byte[] data)
        {
            if (Pushmo.Pushmo.IsMatchingData(data))
                return GameMode.Pushmo;
            if (Crashmo.Crashmo.IsMatchingData(data))
                return GameMode.Crashmo;
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
            }
            return null;
        }
    }
}
