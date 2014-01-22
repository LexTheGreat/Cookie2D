/* File Description
 * Original Works/Author: Thomas Slusny
 * Other Contributors: None
 * Author Website: http://indiearmory.com
 * License: MIT
*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CookieLib.Content
{
    /// <summary>
    /// Provides simple way of loading and saving binary files
    /// </summary>
    public static class SimpleBinary
    {
        /// <summary>
        /// Writes object to binary format in a simple way
        /// </summary>
        public static bool Write(string path, object obj)
        {
            
                FileStream stream = new FileStream(path, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Close();
                return true; 
        }

        /// <summary>
        /// Gets object from binary file format in a simple way
        /// </summary>
        public static object Read(string path)
        {
            
                FileStream stream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = formatter.Deserialize(stream);
                stream.Close();
                return obj;
        }
    }
}
