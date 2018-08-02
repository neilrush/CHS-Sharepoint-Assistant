using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SharepointAssistant
{
    public partial class assistant : Form
    {
        /// <summary>
        /// keeps track of Duplicates
        /// </summary>
        private Dictionary<string, int> _fileCounts = new Dictionary<string, int>();
        /// <summary>
        /// stores files with a FileCodePair()
        /// </summary>
        private Dictionary<FileCodePair, string> _fileIDs = new Dictionary<FileCodePair, string>();
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
            if (uxPath.Text.Trim().Equals(blankText)||!Directory.Exists(uxPath.Text))
            {
                MessageBox.Show("invalid path");
            }
            else
            {
                uxFolderList.Items.Add(uxPath.Text);
                if (uxAddSuffix.Checked) {
                uxSuffixList.Items.Add(uxSuffix.Text);
                    _folders.Push(new SharePointDirectory(uxPath.Text, uxSuffix.Text));
                    WriteToConsole(uxPath.Text + " was added to the folder list with suffix: " + uxSuffix.Text);
                }
                else
                {
                    uxSuffixList.Items.Add("");
                    _folders.Push(new SharePointDirectory(uxPath.Text, ""));
                    WriteToConsole(uxPath.Text + " was added to the folder list");
                }
               
                

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
            if (_folders.Count > 0)
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
            uxSuffix.Clear();
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
            try
            {
                if (_folders.Count > 0)
                {
                    Dictionary<string, int> deletedFiles = new Dictionary<string, int>();
                    if (uxDeleteCopies.Checked)
                    {
                        foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\DeletedSharePointFiles\\"))
                        {
                            deletedFiles.Add(Path.GetFileName(s), 1);
                        }
                    }
                    string exePath = Directory.GetCurrentDirectory();
                    while (_folders.Count > 0)
                    {
                        SharePointDirectory folder = _folders.Peek();
                        while (folder.FileCount > 0)
                        {

                            SharePointFile file = folder.PeekNextFile();
                            try
                            {
                                if (uxDeleteCopies.Checked)
                                {
                                    FileCodePair pair = new FileCodePair(file.FilePath);
                                    if (_fileIDs.ContainsKey(pair))
                                    {
                                        string count = "";
                                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DeletedSharePointFiles\\");
                                        if (deletedFiles.ContainsKey(pair.FileName))
                                        {
                                            count = (deletedFiles[Path.GetFileName(pair.FileName)] + 1).ToString();
                                            deletedFiles[Path.GetFileName(pair.FileName)] = Convert.ToInt32(count);
                                            count = " (" + (Convert.ToInt32(count) - 1) + ")";
                                        }
                                        else
                                        {
                                            _fileCounts.Add(pair.FileName, 1);
                                        }
                                        move(file.FilePath, Directory.GetCurrentDirectory() + "\\DeletedSharePointFiles\\" + file.FileName + count + file.Extention);
                                        WriteToConsole("Deleted " + file.FileName + file.Extention);
                                    }
                                    else
                                    {
                                        _fileIDs.Add(pair, file.FilePath);
                                    }
                                }//uses md5 checksums looking for duplicate files
                                else
                                {
                                    StringBuilder finalPath = new StringBuilder();
                                    StringBuilder fileNameBuilder = new StringBuilder();
                                    finalPath.Append(file.FileFolder + "\\");
                                    if (uxReformatDates.Checked)//reformat date tool
                                    {
                                        file.getDateName();
                                        if (file.HasDate)
                                        {
                                            fileNameBuilder.Append(file.Date);
                                        }

                                    }
                                    if (uxAddDates.Checked)//add date tool
                                    {
                                        if (!file.HasDate)
                                        {
                                            fileNameBuilder.Append(file.getModifiedDate() + " ");
                                        }
                                    }
                                    fileNameBuilder.Append(file.FileName);

                                    if (uxAddSuffix.Checked)//suffix tool
                                    {
                                        if (file.HasSuffix&&!(((file.FileName).ToLower()).Contains(folder.Suffix.ToLower())))
                                        {
                                            fileNameBuilder.Append(" ");
                                            fileNameBuilder.Append(folder.Suffix);
                                        }
                                    }
                                    if (uxNumberDuplicates.Checked)//checks for duplicates
                                    {
                                        string fileNameWithExtension = (file.FileName + file.Extention).ToLower();
                                        int count;
                                        if (_fileCounts.ContainsKey(fileNameWithExtension))
                                        {
                                            count = _fileCounts[fileNameWithExtension] + 1;
                                            _fileCounts[fileNameWithExtension] = count;
                                            fileNameBuilder.Append(" (");
                                            fileNameBuilder.Append(count - 1);
                                            fileNameBuilder.Append(")");
                                        }
                                        else
                                        {
                                            _fileCounts.Add(fileNameWithExtension, 1);
                                        }
                                    }
                                    //wraps up the file name remember to pass over files that did not change name
                                    fileNameBuilder.Append(file.Extention);
                                    finalPath.Append(fileNameBuilder.ToString());
                                    move(file.FilePath, finalPath.ToString());
                                    WriteToConsole("Renamed " + file.FileName + " To " + fileNameBuilder.ToString());
                                    fileNameBuilder.Clear();
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unable to rename " + file.FilePath + "\n" + ex.ToString());
                                WriteToConsole("Unable to rename " + file.FilePath + "\n" + ex.ToString());
                            }
                            folder.NextFile();
                        }
                        _folders.Pop();
                    }
                    _fileCounts.Clear(); //make sure everything is cleared out after processing files
                    _fileIDs.Clear();
                    _folders.Clear();
                    uxFolderList.Items.Clear();
                    uxPath.Clear();
                    uxSuffixList.Items.Clear();
                    uxSuffix.Clear();
                    WriteToConsole("Done");
                    if (ConsoleIndex == 0)
                    {
                        MessageBox.Show("Done");
                    }
                }
                else
                {
                    MessageBox.Show("nothing to process");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        /// <summary>
        /// shows/hides the suffix box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxAddSuffix_Click(object sender, EventArgs e)
        {
            uxSuffix.Visible = uxAddSuffix.Checked;
        }
        /// <summary>
        /// automatically adds a suffix based on the folder name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxPath_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// moves the file from the given location to the new location 
        /// makes sure to write to console and logfile
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        private void move(string oldFile, string newFile)
        {
            try
            {
                File.Move(oldFile, newFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void uxPath_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string folder = uxPath.Text;
                String dirname = new DirectoryInfo(folder).Name;
                uxSuffix.Text = dirname;
            }
            catch (Exception)
            {
                //just skip
            }
        }

    }
}
