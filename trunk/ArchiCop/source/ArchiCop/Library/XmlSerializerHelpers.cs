
using System.Xml.Serialization;
using System.IO;

namespace ArchiCop.Library
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlSerializerHelpers
    {
        /// <summary>
        /// Reads the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static T Read<T>(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Dump File doesn't exist.", filename);
            }
            var deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(filename);
            var instance = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return instance;
        }

        /// <summary>
        /// Writes the specified list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="filename">The filename.</param>
        public static T Write<T>(this T instance, string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Dump File doesn't exist.", filename);
            }
            var serializer = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, instance);
            textWriter.Close();
            return instance;
        }


    }
}