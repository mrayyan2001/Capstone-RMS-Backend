using api.DTOs.Offers;
using api.Models;

namespace api.Interfaces
{
    public interface IOfferServce
    {
        public Task<IEnumerable<OffersDTOs>> GetActiveOffers();
    }
}
