using api.DTOs.Address;
using api.Models;

namespace api.Interfaces
{
    public interface IAddress
    {
        Task<Address> AddNewAddress(AddNewAddressDTO dto);
    }
}
