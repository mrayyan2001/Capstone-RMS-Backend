using api.DTOs.Cart;
using api.DTOs.Notification;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class NotificationService : INotification
    {
        private readonly FoodtekDbContext _context;

        public NotificationService(FoodtekDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllNotificationDTO>> GetAllNotification(int userid)
        {
            if (!_context.Users.Any(x => x.Id == userid))
            {
                throw new Exception("UserId is invalid");
            }

            return await _context.Notifications.Where(x => x.UserId == userid)
                .Select(x => new GetAllNotificationDTO()
                {
                    NotificationId = x.Id,
                    TitelEn = x.TitleEn,
                    TitelAr = x.TitleAr,
                    ContentEn = x.DescriptionEn,
                    ContentAr = x.DescriptionAr,
                    Date = x.CreatedAt,
                    IsRead = x.IsRead
                }).ToListAsync();
        }
    }
}
