using api.Data.interfaces;
using api.DTOs.Offers;
using api.Interfaces;
using api.Models;
using AutoMapper;

namespace api.Services
{
    public class OfferService:IOfferServce
    {

        private readonly IBaseRepo<Offer> _repo;
        private readonly IMapper _mapper;

        public OfferService(IBaseRepo<Offer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OffersDTOs>> GetActiveOffers()
        {
            IEnumerable<Offer> result = await _repo.FindAllAsync(x=>x.IsActive==true);
            return _mapper.Map<IEnumerable<OffersDTOs>>(result);

        }

    }
}
