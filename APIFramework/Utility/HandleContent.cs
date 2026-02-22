using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace APIFramework.Utility
{
    public class HandleContent
    {
        public static T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }

        public static string GetFilePath(string name)
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directory != null)
            {
                string newPath = Path.Combine(directory.Replace("bin\\Debug", "Test Data"), name);

                return newPath;
            }
            else
            {
                throw new InvalidOperationException("Unable to determine the directory.");
            }
        }
    }
}
