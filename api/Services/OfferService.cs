using api.Data.interfaces;
using api.DTOs.Offers;
using api.Models;
using AutoMapper;

namespace api.Services
{
    public class OfferService
    {

        private readonly IBaseRepo<Offer> _repo;
        private readonly IMapper _mapper;

        public OfferService(IBaseRepo<Offer> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<OffersDTOs> GetActiveOffers()
        {
           var result= await _repo.FindAllAsync(x=>x.IsActive==true);
            return _mapper.Map<OffersDTOs>(result);

        }

    }
}
