using api.DTOs.Address;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class AddressService : IAddress
    {
        private readonly FoodtekDbContext _context;

        public AddressService(FoodtekDbContext context)
        {
            _context = context;
        }
        public async Task<Address> AddNewAddress(AddNewAddressDTO dto)
        {
            var user=_context.Users.Where(x=>x.Id==dto.ClientId).SingleOrDefault();
            if (user != null)
                throw new ArgumentException("invalid user");
            var address = new Address
            {
              ClientId = dto.ClientId,
              AddressName=dto.AddressName,
              Hint = dto.Hint,
              Region = dto.Region,
              Province = dto.Province,
              Latitude = dto.Latitude,
              Longitude = dto.Longitude,
             }; 
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }
    }
}
