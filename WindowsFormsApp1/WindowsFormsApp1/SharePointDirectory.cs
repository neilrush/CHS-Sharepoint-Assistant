using System.Collections.Generic;
using System.IO;

namespace SharepointAssistant
{
    class SharePointDirectory
    {
        /// <summary>
        /// The path to the folder
        /// </summary>
        private string _directory;
        /// <summary>
        /// The suffix associated with the files in the folder
        /// </summary>
        private string _suffix;
        /// <summary>
        /// the files that are in the folder
        /// </summary>
        private Queue<SharePointFile> _files = new Queue<SharePointFile>();
        /// <summary>
        /// gets the directory of this folder
        /// </summary>
        public string FolderDirectory
        {
            get
            {
                return _directory;
            }
        }
        /// <summary>
        /// getter for the suffix associated with the folder
        /// </summary>
        public string Suffix
        {
            get
            {
                return _suffix;
            }
        }
        public int FileCount
        {
            get
            {
                return _files.Count;
            }
        }
        /// <summary>
        /// constructor for Directory
        /// </summary>
        /// <param name="Directory"></param>
        /// <param name="Suffix"></param>
        public SharePointDirectory(string Directory, string Suffix)
        {
            this._directory = Directory;
            this._suffix = Suffix;
            GetAllFiles(Directory);
        }

        /// <summary>
        /// recursively gets all the files in the specified folder and adds them to the file queue
        /// </summary>
        /// <param name="folder"></param>
        private void GetAllFiles(string folder)
        {
            if (folder == null)
            {
                return;
            }
            else
            {
                foreach (string file in Directory.GetFiles(folder))
                {
                    _files.Enqueue(new SharePointFile(file,_suffix));
                }
                foreach (string directory in Directory.GetDirectories(folder))
                {
                    GetAllFiles(directory);
                }
            }
        }
        /// <summary>
        /// removes and returns the next file from the queue
        /// </summary>
        /// <returns></returns>
        public SharePointFile NextFile()
        {
            return _files.Dequeue();
        }
        /// <summary>
        /// peeks at the next file
        /// </summary>
        /// <returns></returns>
        public SharePointFile PeekNextFile()
        {
            return _files.Peek();
        }
    }
}
