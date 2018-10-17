using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApp
{
    public static class SerializerHelper
    {
        private static XmlWriterSettings GetXmlWriterSettings()
        {
            return new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = new UTF8Encoding(false),
                Indent = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
        }

        /// <summary>
        /// Serialize an object to a string
        /// </summary>
        /// <param name="obj">Object to be serialized</param>
        /// <returns>XML string of the serialized object</returns>
        public static string SerializeObject(object obj)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(obj.GetType());

            using (XmlWriter writer = XmlWriter.Create(sb, GetXmlWriterSettings()))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(writer, obj, ns);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Write all the updated xml data back out to a file
        /// </summary>
        /// <param name="obj">Object to be serialized</param>
        /// <param name="filePath">File to save the XML to</param>
        public static void SerializeObject(object obj, string filePath)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (XmlWriter writer = XmlWriter.Create(filePath, GetXmlWriterSettings()))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object
        /// Deserialize an object given a file path.
        /// </summary>
        /// <typeparam name="T">Type of the object we are deserializing</typeparam>
        /// <param name="xmlFilename">Name of the XML file to deserialize</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string filePath)
        {
            var returnObject = default(T);
            if (string.IsNullOrEmpty(filePath)) return default(T);

            try
            {
                var xmlStream = new StreamReader(filePath);
                var serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                //Log.Error("Failed to deserialize from file. File: " + filePath + ". Ex: " + ex.ToString());
            }
            return returnObject;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object
        /// Deserialize an object given a string of xml data.
        /// </summary>
        /// <typeparam name="T">Type of the object we are deserializing</typeparam>
        /// <param name="xmlFilename">XML data to deserialize</param>
        /// <returns></returns>
        public static T DeserializeObjectFromString<T>(string data)
        {
            var returnObject = default(T);
            if (string.IsNullOrEmpty(data)) return default(T);

            try
            {
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));

                var serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
               // Log.Error("Failed to deserialize from string. Ex: " + ex.ToString());
            }
            return returnObject;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/10518372/how-to-deserialize-xml-to-object
        /// Deserialize an object given a stream of xml data.
        /// </summary>
        /// <typeparam name="T">Type of the object we are deserializing</typeparam>
        /// <param name="stream">Stream to deserialize</param>
        /// <returns></returns>
        public static T DeserializeObjectFromStream<T>(Stream stream)
        {
            var returnObject = default(T);
            if (stream is null) return default(T);

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
              //  Log.Error("Failed to deserialize from stream. Ex: " + ex.ToString());
            }
            return returnObject;
        }

        /// <summary>
        /// Deserialize many files in a single directory and return the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static List<T> DeserializeDirectoryObjects<T>(string directoryPath)
        {
            var returnList = new List<T>();

            if (!Directory.Exists(directoryPath))
                return returnList;

            foreach (string file in Directory.GetFiles(directoryPath))
            {
                var module = SerializerHelper.DeserializeObject<T>(file);
                returnList.Add(module);
            }

            return returnList;
        }
    }
}
