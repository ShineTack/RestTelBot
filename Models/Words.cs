using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnglishQuizTelegramBot.Models
{
    [XmlRoot("Data")]
    public class Words
    {
        [XmlElement(ElementName = "Words", Type = typeof(List<Word>))]
        public List<Word> AllWords { get; set; }
    }
}