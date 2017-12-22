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

namespace SharepointAssistant
{
    public partial class assistant : Form
    {
        /// <summary>
        /// keeps track of Duplicates
        /// </summary>
        private Dictionary<string,int> _fileCounts = new Dictionary<string,int>();
        /// <summary>
        /// stores the folders to be processed in order
        /// </summary>
        private Stack<SharePointDirectory> _folders = new Stack<SharePointDirectory>();
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
        /// <summary>
        /// Loads a folder using the path in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLoadFolder_Click(object sender, EventArgs e)
        {
            if (uxPath.Text.Trim().Equals(blankText))
            {
                MessageBox.Show("You must enter a path");
            }
            else
            {
                uxFolderList.Items.Add(uxPath.Text);
                uxSuffixList.Items.Add(uxSuffix.Text);
                _folders.Push(new SharePointDirectory(uxPath.Text,uxSuffix.Text));
                WriteToConsole(uxPath.Text + " was added to the folder list with suffix: " + uxSuffix.Text);

                uxPath.Clear();
                uxSuffix.Clear();
            }
        }
        /// <summary>
        /// represents what a unedited text box looks like
        /// </summary>
        string blankText;
        /// <summary>
        /// loads a folder from the folder browser dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFolder_Click(object sender, EventArgs e)
        {
            if (uxOpenFolderDialog.ShowDialog() == DialogResult.OK)
            {
                uxPath.Text = uxOpenFolderDialog.SelectedPath;
            }
        }
        /// <summary>
        /// Removes the last entered folder from the stack and the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxRemoveLast_Click(object sender, EventArgs e)
        {
            if(_folders.Count>0)
            {
                string remove = _folders.Pop().FolderDirectory;
                for (int n = uxFolderList.Items.Count - 1; n >= 0; --n)
                {
                    if (uxFolderList.Items[n].ToString().Contains(remove))
                    {
                        WriteToConsole(remove + " was removed from the folder list");
                        uxFolderList.Items.RemoveAt(n);
                        uxSuffixList.Items.RemoveAt(n);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Folders to Remove");
            }
        }
#endregion
        /// <summary>
        /// clears the list and the stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxClear_Click(object sender, EventArgs e)
        {
            _folders.Clear();
            uxFolderList.Items.Clear();
            uxPath.Clear();
            uxSuffixList.Items.Clear();
            WriteToConsole("Cleared");
        }
        /// <summary>
        /// runs the selected operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxRun_Click(object sender, EventArgs e)
        {
            if (_folders.Count > 0) {
                while (_folders.Count > 0)
                {
                    SharePointDirectory folder = _folders.Peek();
                    while (folder.FileCount > 0)
                    {
                        SharePointFile file = folder.PeekNextFile();
                        StringBuilder finalPath = new StringBuilder();
                        StringBuilder sb = new StringBuilder();
                        finalPath.Append(file.FileFolder+"\\");
                        sb.Append(file.FileName);
                        if (uxReformatDates.Checked)//reformat date tool
                        {

                        }
                        if (uxAddDates.Checked)//add date tool
                        {

                        }
                        if (uxAddSuffix.Checked)//suffix tool
                        {
                            if (file.HasSuffix)
                            {
                                sb.Append(" ");
                                sb.Append(folder.Suffix);
                            }
                        }
                        if (uxNumberDuplicates.Checked)//checks for duplicates
                        {
                            string fileNameWithExtension = file.FileName + file.Extention;
                            int count;
                            if (_fileCounts.ContainsKey(file.FileName + file.Extention))
                            {
                                count = _fileCounts[fileNameWithExtension] + 1;
                                _fileCounts[fileNameWithExtension] = count;
                                sb.Append(" (");
                                sb.Append(count - 1);
                                sb.Append(")");
                            }
                            else
                            {
                                _fileCounts.Add(fileNameWithExtension, 1);
                            }
                        }
                        //wraps up the file name
                        sb.Append(file.Extention);
                        finalPath.Append(sb.ToString());
                        File.Move(file.FilePath, finalPath.ToString());
                        WriteToConsole("Renamed " + file.FileName + " To " + sb.ToString());
                        sb.Clear();
                        folder.NextFile();
                    }
                    _folders.Pop();
                }
            }
            else
            {
                MessageBox.Show("nothing to process");
            }
        }

            
        /// <summary>
        /// writes the given string to the internal console and the Form console
        /// </summary>
        /// <param name="s"></param>
        public void WriteToConsole(string s)
        {
            uxConsole.Text += s + Environment.NewLine;
            Console.WriteLine(s);
        }

        private void uxAddSuffix_Click(object sender, EventArgs e)
        {
            uxSuffix.Visible = uxAddSuffix.Checked;
        }
    }
}
