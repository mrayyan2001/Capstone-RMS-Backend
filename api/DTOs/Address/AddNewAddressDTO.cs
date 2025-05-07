namespace api.DTOs.Address
{
    public class AddNewAddressDTO
    {
        public int ClientId { get; set; }
        public string AddressName { get; set; } = null!;

        public string? Hint { get; set; }

        public string Region { get; set; } = null!;

        public string Province { get; set; } = null!;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        
    }
}
