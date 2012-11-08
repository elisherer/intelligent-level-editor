namespace IntelligentLevelEditor.Games
{
    interface IStudio
    {
        void NewData();
        void LoadData(byte[] data);
        byte[] SaveData();
    }
}
