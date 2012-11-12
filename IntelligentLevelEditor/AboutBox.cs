using System.Drawing;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            lblTitle.Text = Application.ProductName + @" v." + Application.ProductVersion;
        }
    }
}
