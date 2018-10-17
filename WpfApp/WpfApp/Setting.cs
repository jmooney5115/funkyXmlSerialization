/*     Licensed under the Apache License, Version 2.0
    
http://www.apache.org/licenses/LICENSE-2.0
*/
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace WpfApp
{
    [XmlRoot(ElementName = "Setting")]
    public class Setting
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
        
        public override string ToString()
        {
            return $"Name: {Name}, Value: {Value}";
        }
    }

    [XmlRoot(ElementName = "Row")]
    public class Row
    {
        [XmlElement(ElementName = "Setting")]
        public List<Setting> Setting { get; set; }
    }

    [XmlRoot(ElementName = "Page")]
    public class Page
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Row")]
        public List<Row> Row { get; set; }
    }

    [XmlRoot(ElementName = "Pages")]
    public class Pages
    {
        [XmlElement(ElementName = "Page")]
        public Page Page { get; set; }
    }

    [XmlRoot(ElementName = "Processor")]
    public class Processor
    {
        [XmlElement(ElementName = "Pages")]
        public Pages Pages { get; set; }
    }

    [XmlRoot(ElementName = "Object")]
    public class Object
    {
        [XmlElement(ElementName = "Processor")]
        public Processor Processor { get; set; }
    }
}