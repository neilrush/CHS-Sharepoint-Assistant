using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace SharepointAssistant
{
    class FileCodePair
    {
        /// <summary>
        /// stores the md5 code for the file
        /// </summary>
        private string _code;
        /// <summary>
        /// stores the filename associated with the md5
        /// </summary>
        private string _fileName;
        /// <summary>
        /// getter for the md5
        /// </summary>
        public string Code
        {
            get => _code;
        }
        /// <summary>
        /// getter for the _fileName
        /// </summary>
        public string FileName
        {
            get => _fileName;
        }
        /// <summary>
        /// constructor for the FileMd5 pair
        /// </summary>
        /// <param name="FileName"></param>
        public FileCodePair(string FileName)
        {
            _fileName = FileName;
            _code = CalculateMD5(FileName);
        }
        /// <summary>
        /// compares this file with the given file path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool CompareFiles(string filename)
        {
            return CalculateMD5(filename) == _code;
        }
        /// <summary>
        /// compares this file with the given fileCodePair
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CompareFiles(FileCodePair file)
        {
            return file.Code == _code;
        }
        /// <summary>
        /// equals operator for FileCodePair
        /// </summary>
        /// <param name="x">the first FileCodePair</param>
        /// <param name="y">the second FileCodePair</param>
        /// <returns>if they are equal</returns>
        public static bool operator ==(FileCodePair x, FileCodePair y)
        {
            if (Equals(x, null))
            {
                return Equals(y, null);
            }
            else if (Equals(y, null))
            {
                return false;
            }
            if (x.Code != y.Code)
            {
                return false;
            }
            else
            return true;
        }
        /// <summary>
        /// checks if the two FileCodePairs are inequal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(FileCodePair x, FileCodePair y)
        {
            return !(x == y);
        }
        /// <summary>
        /// equals function for FileCodePair
        /// </summary>
        /// <param name="obj">the object to check equality of</param>
        /// <returns>if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is FileCodePair)
            {
                return this == (FileCodePair)obj;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return _code.GetHashCode();
        }
        /// <summary>
        /// returns the md5 as a string
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
