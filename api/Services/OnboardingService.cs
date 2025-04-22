using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace api.Services
{
    public class OnboardingService : IOnboardingService
    {
        public List<OnboardingPage> GetAllPages()
            => _pages;

        private readonly List<OnboardingPage> _pages = new List<OnboardingPage>
        {
            new OnboardingPage
            {
                Id = 1,
                ImgUrl = "images/onboarding1.png",
                TitleEn = "Welcome to Sahlah",
                TitleAr = "مرحباً بك في سهلة",
                DescriptionEn = "Enjoy a fast and smooth food delivery at your doorstep",
                DescriptionAr = "استمتع بتوصيل طعام سريع وسلس إلى باب منزلك",
                PageNumber = 1
            },
            new OnboardingPage
            {
                Id = 2,
                ImgUrl = "images/onboarding2.png",
                TitleEn = "Get Delivery On Time",
                TitleAr = "احصل على التوصيل في الوقت المحدد",
                DescriptionEn = "Order your favorite food within the palm of your hand and the zone of your comfort",
                DescriptionAr = "اطلب طعامك المفضل بسهولة ومن مكان راحتك",
                PageNumber = 2
            },
            new OnboardingPage
            {
                Id = 3,
                ImgUrl = "images/onboarding3.png",
                TitleEn = "Choose Your Food",
                TitleAr = "اختر طعامك",
                DescriptionEn = "Order your favorite food within the palm of your hand and the zone of your comfort",
                DescriptionAr = "اطلب طعامك المفضل بسهولة ومن مكان راحتك",
                PageNumber = 3
            },
            new OnboardingPage
            {
                Id = 4,
                ImgUrl = "images/onboarding4.png",
                TitleEn = "Turn On Your Location",
                TitleAr = "قم بتشغيل الموقع",
                DescriptionEn = "To continue, let your device turn on location, which uses Google’s location service",
                DescriptionAr = "للمتابعة، اسمح لجهازك بتشغيل الموقع الذي يستخدم خدمة تحديد الموقع من Google",
                PageNumber = 4
            }
        };
        public OnboardingPage? GetPageByNumber(int pageNum)
        {
            if (pageNum <= 0)
            {
                throw new Exception("InValid Page number");

            }
            return _pages.SingleOrDefault(x => x.PageNumber == pageNum);
        }


    }
}
