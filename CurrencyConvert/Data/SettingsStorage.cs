using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CurrencyConvert.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CurrencyConvert.Data
{
    class SettingsStorage
    {
        // AttemptRetrieveData value are stored in here
        public SettingsStorageDto settings;
        public SettingsStorageDto AttemptRetrieveData()
        {
            // read file

            // convert that file's string to .json

            // return the .json parsed as the SettingsStorageDto

            return null;
        }

        public SettingsStorageDto GetStoredValues {
            // if settings is null get the values and return the settings, else just return the settings and not try to read the file again
            get
            {
                return null;
            }
        }

        public void SaveSettings(SettingsStorageDto settings)
        {
            // save the SettingsStorageDto as a JSON file
        }

        public bool FilterException(Exception exception)
        {
            return exception is FileNotFoundException || exception is UnauthorizedAccessException ||
                   exception is DirectoryNotFoundException;
        }
    }
}
