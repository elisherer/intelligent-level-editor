using System.Windows.Forms;

namespace IntelligentLevelEditor.Games.Pyramids
{
    public partial class MakeQRCardForm : Form
    {
        public MakeQRCardForm()
        {
            InitializeComponent();
            txtCreator.Text = Properties.Settings.Default.DefaultCreator;
        }

        public static DialogResult ShowDialog(ref string name, ref string creator)
        {
            var frm = new MakeQRCardForm();
            var ret = frm.ShowDialog();
            if (ret==DialogResult.OK)
            {
                name = frm.txtName.Text;
                creator = frm.txtCreator.Text;
            }
            return ret;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.DefaultCreator = txtCreator.Text;
            Properties.Settings.Default.Save();
        }
    }
}
