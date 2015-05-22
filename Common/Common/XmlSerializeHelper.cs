using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// XML序列化和反序列化
    /// </summary>
    public class XmlSerializeHelper
    {
        public static string Serialize(object model, Encoding encode)
        {
            using (var sm = new System.IO.MemoryStream())
            {
                XmlSerializer ser = new XmlSerializer(model.GetType());
                ser.Serialize(sm, model);
                return encode.GetString(sm.GetBuffer());
            }
        }

        public static TEntity Deserialize<TEntity>(string xml, Encoding encode) where TEntity : class
        {
            using (var sm = new System.IO.MemoryStream())
            {
                var writer = new System.IO.StreamWriter(sm, encode);
                writer.Write(xml);
                writer.Flush();
                sm.Seek(0, System.IO.SeekOrigin.Begin);
                XmlSerializer ser = new XmlSerializer(typeof(TEntity));
                return ser.Deserialize(sm) as TEntity;
            }
        }
    }
}
