using api.DTOs.Payment;
using api.Models;

namespace api.Interfaces
{
    public interface IPayment
    {
        Task<PaymentMethod?> AddNewPayment(AddNewPaymentDTO dto);

    }
}
