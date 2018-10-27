
namespace EfCoreTutoApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class ConnStringHelpers
    {
        public static string ReplaceVarsFromConnectionString(string connectionString)
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;

            var varsToReplace = new Dictionary<string, string> {
                { "appdata", Path.Combine(appPath, "data")
                }
            };

            var result = connectionString;

            foreach (var entry in varsToReplace)
            {
                result = result.Replace($"%{entry.Key}%", entry.Value);
            }
            return result;
        }
    }
}
