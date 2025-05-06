using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Notification
{
    public class GetAllNotificationDTO
    {
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public string TitelAr { get; set; }
        public string TitelEn { get; set; }

        public string ContentEn { get; set; }
        public string ContentAr { get; set; }

        public DateTime? Date { get; set; }
        public bool? IsRead { get; set; }


    }
}
