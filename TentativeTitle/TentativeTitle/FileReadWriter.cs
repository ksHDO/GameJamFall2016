using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TentativeTitle.Maps;

namespace TentativeTitle
{
    public class FileReadWriter
    {
        /// <summary>
        /// Writes a specified object into a Json file of the designated path.
        /// Please check if the path exists.
        /// Unless using CreateDirectory(), use the other overload that takes in (object, string, string, Formatting)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fullPath"></param>
        /// <param name="format"></param>
        public static void WriteToJSON(object value, string fullPath, Formatting format = Formatting.Indented)
        {
            string json = JsonConvert.SerializeObject(value, format);
            File.WriteAllText(fullPath, json);
        }

        /// <summary>
        /// Writes a specified object into a Json file of the designated path.
        /// Will check that the path already exists before creating the file.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public static void WriteToJSON(object value, string path, string filename, Formatting format = Formatting.Indented)
        {
            CreateDirectory(path);
            string fullPath = path + filename;
            WriteToJSON(value, fullPath, format);
        }

        public static T ReadFromJSON<T>(string fullPath)
        {
            string json = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ReadFromJSON<T>(string path, string filename)
        {
            CreateDirectory(path);
            string fullPath = path + filename;
            return ReadFromJSON<T>(fullPath);
        }

        public static void WriteToBinary(object value, string fullPath, bool append = false)
        {
            using (Stream stream = File.Open(fullPath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, value);
            }
        }

        public static void WriteToBinary(object value, string path, string filename, bool append = false)
        {
            CreateDirectory(path);
            string fullPath = path + filename;
            WriteToBinary(value, fullPath, append);
        }

        public static T ReadFromBinary<T>(string fullPath)
        {
            using (Stream stream = File.Open(fullPath, FileMode.Open))
            {
                if (stream != null)
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
                else
                {
                    return default(T);
                }

            }
        }
        public static T ReadFromBinary<T>(string path, string filename)
        {
            CreateDirectory(path);
            string fullPath = path + filename;
            return ReadFromBinary<T>(fullPath);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
        public static bool FileExists(string path, string fileName)
        {
            return File.Exists(path + fileName);
        }


        public static string[] RetrieveFiles(string path, string fileType = "")
        {
            string[] files = Directory.GetFiles(path);
            int filesLength = files.Length;
            bool[] filesEndsWith = new bool[filesLength];
            string[] output;
            int outputSize = 0;
            int currentIndex = 0;

            for (int i = 0; i < filesLength; i++)
                if (filesEndsWith[i] = files[i].EndsWith(fileType))
                    outputSize++;

            output = new string[outputSize];
            for (int i = 0; i < filesLength; i++)
                if (filesEndsWith[i])
                    output[currentIndex++] = files[i];
            return output;
        }

        public static T[] RetrieveFilesFromBinary<T>(string path, string fileType)
        {
            string[] files = RetrieveFiles(path, fileType);
            int filesLength = files.Length;
            T[] output = new T[filesLength];

            for (int i = 0; i < filesLength; i++)
            {
                output[i] = ReadFromBinary<T>(files[i]);
            }
            return output;
        }

        static T[] RetrieveFilesFromJSON<T>(string path, string fileType)
        {
            string[] files = RetrieveFiles(path, fileType);
            int filesLength = files.Length;
            T[] output = new T[filesLength];

            for (int i = 0; i < filesLength; i++)
            {
                output[i] = ReadFromJSON<T>(files[i]);
            }
            return output;
        }

    }
}
