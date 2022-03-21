using System.Xml.Serialization;

namespace EnglishQuizTelegramBot.Models
{
    [XmlRoot("Member")]
    public class Member
    {
        [XmlElement("Id")]
        public long Id { get; set; }
        
        [XmlElement("Username")]
        public string Username { get; set; }
        
        [XmlElement("ChatId")]
        public long ChatId { get; set; }
    }
}