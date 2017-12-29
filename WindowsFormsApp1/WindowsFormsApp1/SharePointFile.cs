using System.IO;
using System;
using System.Text;
using System.Linq;

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
        /// the date for the file
        /// </summary>
        private string _date = "";
        /// <summary>
        /// if there is a good date for this file name
        /// </summary>
        bool goodDate = false;
        /// <summary>
        /// returns if the file has a date
        /// </summary>
        public bool HasDate
        {
            get => goodDate;
        }
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
        public string Date
        {
            get => _date;
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
        /// <summary>
        /// gets the date from the built in metadata
        /// </summary>
        public void GetDate()
        {
            if (!goodDate)
            {
                goodDate = CheckForDate();
            }
            if (!goodDate)
            {
                StringBuilder date = new StringBuilder();
                DateTime lastWriteTime = File.GetLastWriteTime(_fName);
                date.Append(lastWriteTime.Year.ToString() + " ");
                if (lastWriteTime.Month >= 10)
                {
                    date.Append(lastWriteTime.Month);
                }
                else
                {
                    date.Append("0" + lastWriteTime.Month);
                }
                if (lastWriteTime.Day < 10)
                {
                    date.Append("0" + lastWriteTime.Day);
                }
                else
                {
                    _date += lastWriteTime.Day;
                }
            }
        }
        /// <summary>
        /// Gets the Date and name separated from the file name
        /// </summary>
        public void getDateName()
        {
            goodDate = CheckForDate();
            if (!goodDate)
            {
                bool isDigitPresent = _fName.Any(c => char.IsDigit(c));
                if (isDigitPresent)
                {
                    string[] nameParts = _fName.Split(new char[3] { ' ', '-', '_' });
                    StringBuilder name = new StringBuilder();

                    foreach (string numberCandidate in nameParts)
                    {
                        int n;
                        bool isNumeric = int.TryParse(numberCandidate, out n);
                        int length = numberCandidate.Length;
                        StringBuilder date = new StringBuilder();
                        if (isNumeric)
                        {
                            int year = Convert.ToInt32(numberCandidate.Substring(2, 2));
                            int month = Convert.ToInt32(numberCandidate.Substring(0, 2));
                            int day = 0;
                            if (length == 4)
                            {
                                day = File.GetLastWriteTime(_path).Day;
                                if (day <= 31 && day >= 1 && month <= 12 && month >= 1)
                                {
                                    if (year >= 80)
                                    {
                                        date.Append("19");
                                    }
                                    else if (year + 2000 <= DateTime.Now.Year)
                                    {
                                        date.Append("20");
                                    }
                                    else
                                    {
                                        name.Append(numberCandidate);
                                        break;
                                    }
                                    //dont forget to append 0 where needed for month and day
                                    date.Append(year + " " + month + day);//date found
                                    _date = date.ToString();
                                    goodDate = true;
                                }
                                else
                                {
                                    name.Append(numberCandidate);
                                }
                            }
                            else if (length == 6)
                            {
                                day = Convert.ToInt32(numberCandidate.Substring(4, 2));
                                if (day <= 31 && day >= 1 && month <= 12 && month >= 1)
                                {
                                    if (year >= 80)
                                    {
                                        date.Append("19");
                                    }
                                    else if (year + 2000 <= DateTime.Now.Year)
                                    {
                                        date.Append("20");
                                    }
                                    else
                                    {
                                        name.Append(numberCandidate);
                                        break;
                                    }
                                    //dont forget to append 0 where needed for month and day
                                    date.Append(year + " " + month + day);//date found
                                    _date = date.ToString();
                                    goodDate = true;
                                }
                                else
                                {
                                    name.Append(numberCandidate);
                                }
                            }
                            else
                            {
                                //not of a valid number of chars to be a date
                                name.Append(numberCandidate);
                            }
                        }// end is numeric
                        else
                        {
                            name.Append(numberCandidate);//not a valid number so add it to the name
                        }//end if else
                    }//end for each name parts
                }//end digit present
            }//end is good date
        }//end get date name

        /// <summary>
        /// checks for a date that is already in the file name
        /// </summary>
        /// <returns>if there is a date or not</returns>
        private bool CheckForDate()
        {
            if (_fName.Length >= 9)
            {
                string yearString = _fName.Substring(0, 4);
                string monthString = _fName.Substring(5, 2);
                string dayString = _fName.Substring(7, 2);
                int year;
                int day;
                int month;
                if (int.TryParse(yearString, out year) && int.TryParse(dayString, out day) && int.TryParse(monthString, out month))
                {
                    if ((day <= 31 && day >= 1 && month <= 12 && month >= 1) && (year >= 1980 && year <= DateTime.Now.Year))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
