# CHS-Sharepoint-Assistant
Tool for renaming files in bulk. Purpose built for CHS data storage standards to move to sharepoint.

add dates:
    for adding dates to files that have no date. skips if valid date is detected. format: MMDD YYYY
    
reformat dates:
    reformate dates that are in the following format MMDDYY, MMYY - gets missing day from modified date (close enough

add suffix: 
    adds suffix to the end of all files in folder input suffix before adding folder to list

number duplicate names: 
    compares files in all folders that are adding for processing to make sure there are no duplicate names. adds (#) to the end 

delete copy files:
    checks files using md5 hashes in order to clean out any duplicates from duplicated efforts from past filing 
