using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class assistant : Form
    {
        /// <summary>
        /// stores the folders to be processed in order
        /// </summary>
        private Stack<String> _folders = new Stack<string>();
        public assistant()
        {
            InitializeComponent();
            uxShowConsole.Text = ShowConsoleText[0];
            blankText = uxPath.Text;
        }
#region Show/Hide Console
        /// <summary>
        /// The strings to iterate through for the console button
        /// </summary>
        private String[] ShowConsoleText = { "Show Console", "Hide Console" };
        /// <summary>
        /// used to iterate through the strings
        /// </summary>
        private int ConsoleIndex = 0;
        /// <summary>
        /// Toggles the button text, resizeability and if the console is visible or not
        /// Default State is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxShowConsole_Click(object sender, EventArgs e)
        {
            if (ConsoleIndex == 0)//show console
            {
                uxShowConsole.Text = ShowConsoleText[1];
                ConsoleIndex = 1;
                this.MinimumSize = new Size(593, 323);
                uxConsole.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else//hide console
            {
                uxShowConsole.Text = ShowConsoleText[0];
                ConsoleIndex = 0;
                this.MinimumSize = new Size(593, 209);
                this.Size = new Size(593, 209);
                uxConsole.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }
        #endregion


#region Folder Loading
        private void uxLoadFolder_Click(object sender, EventArgs e)
        {
            uxFolderList.Items.Add(uxPath.Text);
            _folders.Push(uxPath.Text);
            uxPath.Clear();
        }
        string blankText;
        private void LoadFolder_Click(object sender, EventArgs e)
        {
            if (uxOpenFolderDialog.ShowDialog() == DialogResult.OK)
            {
                if (uxPath.Text.Trim().Equals(blankText))
                {
                    MessageBox.Show("You must enter a path");
                }
                else
                {
                    uxFolderList.Items.Add(uxOpenFolderDialog.SelectedPath);
                    _folders.Push(uxOpenFolderDialog.SelectedPath);
                }

            }
        }

        private void uxRemoveLast_Click(object sender, EventArgs e)
        {
            string remove =_folders.Pop();
            for (int n = uxFolderList.Items.Count - 1; n >= 0; --n)
            {
                if (uxFolderList.Items[n].ToString().Contains(remove))
                {
                    uxFolderList.Items.RemoveAt(n);
                }
            }
        }
#endregion
        private void uxClear_Click(object sender, EventArgs e)
        {
            _folders.Clear();
            uxFolderList.Items.Clear();
            uxPath.Clear();
        }

        private void uxRun_Click(object sender, EventArgs e)
        {

        }
    }
}
