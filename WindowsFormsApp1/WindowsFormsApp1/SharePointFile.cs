using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharepointAssistant
{
    class SharePointFile
    {
        private string _path="";
        private string _suffix="";
        public bool HasSuffix
        {
            get
            {
                return _suffix.Trim() == "";
            }
        }
        public string FilePath
        {
            get
            {
                return _path;
            }
        }
        public string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(_path);
            }
        }
        public string Extention
        {
            get
            {
               return Path.GetExtension(_path);
            }
        }
        public string FileFolder
        {
            get
            {
                return Path.GetDirectoryName(_path);
            }
        }

        public SharePointFile(string path, string suffix)
        {
            _path = path;
            _suffix = suffix;
        }
    }
}
