﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ivory.Soap
{
    /// <summary>Represents the SOAP content placeholder.</summary>
    [Serializable]
    [XmlRoot(Namespace = "")]
    public class SoapContent<TContent> : List<TContent>, IXmlSerializable
    {
        /// <inheritdoc/>
        public XmlSchema GetSchema() => null;

        /// <inheritdoc/>
        public void ReadXml(XmlReader reader)
        {
            Guard.NotNull(reader, nameof(reader));
            var serializer = new XmlSerializer(typeof(TContent), string.Empty);

            while (reader.Read())
            {
                var item = (TContent)serializer.Deserialize(reader);
                Add(item);
            }
        }

        /// <inheritdoc/>
        public void WriteXml(XmlWriter writer)
        {
            Guard.NotNull(writer, nameof(writer));
            var serializer = new XmlSerializer(typeof(TContent), string.Empty);

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            foreach (var item in this)
            {
                serializer.Serialize(writer, item, ns);
            }
        }
    }
}