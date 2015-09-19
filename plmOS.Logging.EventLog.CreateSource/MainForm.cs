using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plmOS.Logging.CreateSource
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            plmOS.Logging.EventLog target = new plmOS.Logging.EventLog(this.locationTextBox.Text, this.sourceTextBox.Text);

            try
            {
                target.CreateSource();
                MessageBox.Show("Source created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
