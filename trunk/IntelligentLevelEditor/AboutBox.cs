﻿using System.Drawing;
using System.Windows.Forms;

namespace IntelligentLevelEditor
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Localization.ApplyToContainer(this, "AboutBox");
            lblTitle.Text = Application.ProductName + @" v." + Application.ProductVersion;
        }
    }
}