using api.DTOs.Cart;
using api.DTOs.Payment;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class PaymentService : IPayment
    {
        private readonly FoodtekDbContext _context;

        public PaymentService(FoodtekDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentMethod?> AddNewPayment(AddNewPaymentDTO dto)
        {
            var user = _context.Users.Where(x => x.Id == dto.ClientId).SingleOrDefault();
            if(user==null)
                throw new ArgumentException("User Not Found");
            

            var payment = new PaymentMethod()
            {
                ClientId = dto.ClientId,
                CardNumber = dto.CardNumber,
                CardHolderName = dto.CardHolderName,
                ExpiryDate=dto.ExpiryDate,
                Cvc=dto.Cvv,
            };
            await _context.PaymentMethods.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;

        }
    }
}
