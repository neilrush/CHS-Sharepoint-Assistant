using System.IO;

namespace SharepointAssistant
{
    class SharePointFile
    {
        /// <summary>
        /// The path for this file
        /// </summary>
        private string _path="";
        /// <summary>
        /// The suffix for this file
        /// </summary>
        private string _suffix="";
        /// <summary>
        /// The Name without extention and no path
        /// </summary>
        private string _fName = "";
        /// <summary>
        /// returns if the file has a suffix to be added to the name
        /// </summary>
        public bool HasSuffix
        {
            get
            {
                return _suffix != "";
            }
        }
        /// <summary>
        /// returns the full file path
        /// </summary>
        public string FilePath
        {
            get
            {
                return _path;
            }
        }
        /// <summary>
        /// returns the file name without the path and extention
        /// </summary>
        public string FileName
        {
            get
            {
               return _fName;
            }
        }
        /// <summary>
        /// returns just the file extention
        /// </summary>
        public string Extention
        {
            get
            {
               return Path.GetExtension(_path);
            }
        }
        /// <summary>
        /// gives the folder the the file is in
        /// </summary>
        public string FileFolder
        {
            get
            {
                return Path.GetDirectoryName(_path);
            }
        }
        /// <summary>
        /// The Constructor for a SharePoint File
        /// </summary>
        /// <param name="path"></param>
        /// <param name="suffix"></param>
        public SharePointFile(string path, string suffix)
        {
            _path = path;
            _suffix = suffix;
            _fName=Path.GetFileNameWithoutExtension(_path);
        }
    }
}
