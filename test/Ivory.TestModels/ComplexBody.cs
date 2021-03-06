﻿using System;
using System.Xml.Serialization;

namespace Ivory.TestModels
{
    [Serializable]
    [XmlRoot("Complex", Namespace = "http://ivory.net/compex")]
    public class ComplexBody
    {
        public int Answer { get; set; } = 42;

        [XmlAttribute("mood", Namespace = "http://ivory.net/compex-mood")]
        public string Mood { get; set; }
    }
}
