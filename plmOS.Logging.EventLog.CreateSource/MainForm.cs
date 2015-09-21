using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plmOS.Logging.EventLog.CreateSource
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(this.sourceTextBox.Text))
                {
                    System.Diagnostics.EventLog.CreateEventSource(this.sourceTextBox.Text, this.locationTextBox.Text);
                }

                MessageBox.Show("Source created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
