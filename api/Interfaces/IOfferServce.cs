using api.DTOs.Offers;

namespace api.Interfaces
{
    public interface IOfferServce
    {
        public Task<OffersDTOs> GetActiveOffers();
    }
}
