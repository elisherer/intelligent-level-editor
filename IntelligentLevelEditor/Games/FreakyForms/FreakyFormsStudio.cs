using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using IntelligentLevelEditor.Utils;
using com.google.zxing.common;

namespace IntelligentLevelEditor.Games.FreakyForms
{
    public partial class FreakyFormsStudio : UserControl, IStudio
    {
        private readonly FreakyForms _level = new FreakyForms();

        public FreakyFormsStudio()
        {
            InitializeComponent();
        }

        public void NewData()
        {
            _level.New();
        }

        public void LoadData(byte[] data)
        {
            var ms = new MemoryStream(data);
            _level.Read(ms);
            ms.Close();
 
            textName.Text = _level.GetName();
            textAuthor.Text = _level.GetAuthorName();
            textCatchphrase.Text = _level.GetCatchphrase();

            var chunks = _level.GetChunks();
            listviewElements.Items.Clear();
            for (var i=0 ; i<chunks.Count; i++)
            {
                var item = listviewElements.Items.Add(i.ToString("X2"));
                item.SubItems.Add(chunks[i].Type.ToString());
                item.SubItems.Add(MarshalUtil.ByteArrayToString(chunks[i].Data));
            }
        }

        public byte[] SaveData()
        {
            var ms = new MemoryStream();
            _level.Write(ms);
            return ms.ToArray();
        }

        public Image MakeQrCard(ByteMatrix qrMatrix)
        {
            return null;
        }
    }
}
