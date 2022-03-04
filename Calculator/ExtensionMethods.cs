using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// 擴充方法
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// DEEP CLONE
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="copyObject">要複製的物件</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T copyObject)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, copyObject);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
