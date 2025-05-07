namespace api.DTOs.Payment
{
    public class AddNewPaymentDTO
    {
        public int ClientId { get; set; }

        public string CardNumber { get; set; } = null!;

        public string CardHolderName { get; set; } = null!;

        public string ExpiryDate { get; set; } = null!;

        public string Cvv { get; set; } = null!;

       
    }
}
