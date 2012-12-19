using System.Drawing;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class AboutBox : Form
    {
        public AboutBox(Image icon)
        {
            InitializeComponent();
            lblTitle.Text = Application.ProductName + @" v." + Application.ProductVersion;
            picIcon.Image = icon;
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = (LinkLabel)sender;
            System.Diagnostics.Process.Start(link.Text);
        }
    }
}
