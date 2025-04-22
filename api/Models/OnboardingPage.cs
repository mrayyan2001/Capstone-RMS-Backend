using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class OnboardingPage
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Only English letters or digit are allowed.")]
        public string TitleEn { get; set; }

        [RegularExpression("^[\u0621-\u064A]+$", ErrorMessage = "Only Arabic letters are allowed.")]
        public string TitleAr { get; set; }

        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        [RegularExpression(@"\.(png|jpg|jpeg)$", ErrorMessage = "Only .png, .jpg, or .jpeg files are allowed.")]
        public string ImgUrl { get; set; }

        public int PageNumber { get; set; }

    }
        
}
