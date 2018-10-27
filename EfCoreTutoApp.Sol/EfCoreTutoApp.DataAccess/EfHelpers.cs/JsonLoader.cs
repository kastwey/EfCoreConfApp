
namespace EfCoreTutoApp.DataAccess.EfHelperscs
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class JsonLoader
    {
        private const string JsonDataDirectory = "SeedJsonData\\";

        public static IEnumerable<T> LoadJson<T>(string fileSearchString) where T : class
        {
            var filePath = GetJsonFilePath(fileSearchString);
            var jsonDecoded = JsonConvert.DeserializeObject<ICollection<T>>(File.ReadAllText(filePath));

            return jsonDecoded.ToList();
        }

        private static string GetJsonFilePath(string searchPattern)
        {
            var jsonDataPath = Path.Combine(Directory.GetCurrentDirectory(), JsonDataDirectory);
            var fileList = Directory.GetFiles(jsonDataPath, searchPattern);

            if (fileList.Length == 0)
                throw new FileNotFoundException($"Could not find a file with the search name of {searchPattern} in directory {jsonDataPath}");

            return fileList.OrderBy(x => x).Last();
        }
    }
}
