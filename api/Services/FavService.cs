using api.Data.interfaces;
using api.DTOs.Bookmark;
using api.Interfaces;
using api.Models;
using AutoMapper;

namespace api.Services
{
    public class FavService : IfavServices
    {

        private readonly IBaseRepo<Bookmark> _repo;
        private readonly IBaseRepo<Client> _clientRepo;
        private readonly IBaseRepo<Item> _itemRepo;

        private readonly IMapper _mapper;


        public FavService(IBaseRepo<Bookmark> repo, IBaseRepo<Client> clientRepo, IBaseRepo<Item> itemRepo, IMapper mapper)
        {
            _repo = repo;
            _clientRepo = clientRepo;
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        public async Task<List<FavItemDTOs>?> GetFavItem(int userId)
        {

            var favItems = await _repo.FindAllAsync(x=>x.ClientId == userId && x.IsActive==true,new[]{"Item"});
            if (favItems == null )
            {
                return null;
            }

            return _mapper.Map<List<FavItemDTOs>>(favItems);
        }
        public async Task<int> AddItemToFav(AddItemToFavDTO input)
        {
            //if(!await _clientRepo.IsExistsELementAsync(input.ClientId))
            //{
            //    throw new Exception("client doesn't Exists");
            //}
            //if (!await _itemRepo.IsExistsELementAsync(input.ItemId))
            //{
            //    throw new Exception("Item doesn't Exists");
            //}
            Bookmark? item=await _repo.FindAsync((x => x.ClientId == input.ClientId && x.ItemId==input.ItemId));
            if (item!=null)
            {
                if (item.IsActive == true)
                    throw new Exception("The item is already marked as a favorite.");
                await _repo.ReactivateElementAsync(item.Id);
                return item.Id;
            }
            var newItemFav = await _repo.AddElementAsync(_mapper.Map<Bookmark>(input));
            return newItemFav.Id;
        }

        public async Task<int> RemoveItemFromFav(int id)
        {
           bool inactiveItemInFav=await _repo.SoftDeleteElementAsync(id);
            if (!inactiveItemInFav)
                await _repo.HardDeleteElementAsync(id);
            return id;

        }
    }
}
