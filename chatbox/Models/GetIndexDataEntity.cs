using System.ComponentModel.DataAnnotations;

namespace ChatWebApp.Models
{
    public class GetIndexDataEntity
    {
      
       [Key] public int ChatId { get; set; }
        public string Sender { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
    }
}
