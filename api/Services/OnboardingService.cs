using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class OnboardingService : IOnboardingService
    {
        private readonly List<OnboardingPage> _pages = new List<OnboardingPage>
                {
                    new OnboardingPage
                    {
                        Id = 1,
                        ImgUrl = "images/onboarding1.png",
                        Title = "Welcome to Sahlah",
                        Description = "Enjoy a fast and smooth food delivery at your doorstep",
                        PageNumber = 1
                    },
                    new OnboardingPage
                    {
                        Id = 2,
                        ImgUrl = "images/onboarding2.png",
                        Title = "Get Delivery On Time",
                        Description = "Order your favorite food within the palm of your hand and the zone of your comfort",
                        PageNumber = 2
                    },
                    new OnboardingPage
                    {
                        Id = 3,
                        ImgUrl = "images/onboarding3.png",
                        Title = "Choose Your Food",
                        Description = "Order your favorite food within the palm of your hand and the zone of your comfort",
                        PageNumber = 3
                    },
                    new OnboardingPage
                    {
                        Id = 4,
                        ImgUrl = "images/onboarding4.png",
                        Title = "Turn On Your Location",
                        Description = "To continue, let your device turn on location, which uses Google’s location service",
                        PageNumber = 4
                    }
                };

        public List<OnboardingPage> GetAllPages()
            => _pages;

        public OnboardingPage? GetPageByNumber(int pageNumber)
            => _pages.SingleOrDefault(p => p.PageNumber == pageNumber);
    }
}