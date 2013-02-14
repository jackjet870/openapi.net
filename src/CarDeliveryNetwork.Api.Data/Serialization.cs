﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using CarDeliveryNetwork.Types;


namespace CarDeliveryNetwork.Api.Data
{
    class Serialization
    {
        public static string Serialize(object o, MessageFormat format)
        {
            XmlObjectSerializer serializer = format == MessageFormat.Json
                ? new DataContractJsonSerializer(o.GetType()) as XmlObjectSerializer
                : new DataContractSerializer(o.GetType()) as XmlObjectSerializer;

            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                serializer.WriteObject(stream, o);
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public static T Deserialise<T>(string serialisedObject, MessageFormat format)
        {
            XmlObjectSerializer serializer = format == MessageFormat.Json
                ? new DataContractJsonSerializer(typeof(T)) as XmlObjectSerializer
                : new DataContractSerializer(typeof(T)) as XmlObjectSerializer;

            using (var stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(serialisedObject)))
                return (T)serializer.ReadObject(stream);
        }
    }
}