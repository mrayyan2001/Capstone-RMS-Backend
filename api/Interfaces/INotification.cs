using api.DTOs.Cart;
using api.DTOs.Notification;
using api.Models;

namespace api.Interfaces
{
    public interface INotification
    {
        public Task<List<GetAllNotificationDTO>> GetAllNotification(int userid);
    }
}
