using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Helpers;
using System.Numerics;
using ImageSeeder = RS1_2024_25.API.Data.Models.ImageSeeder;

namespace RS1_2024_25.API.Data
{
    public partial class ApplicationDbContextSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData
            (
                  new Gender { GenderID = 1, Name = "Male" },
                  new Gender { GenderID = 2, Name = "Female" }

            );

            modelBuilder.Entity<City>().HasData
             (
                new City { ID = 1, Name = "Sarajevo", CountryId = 1 },
                new City { ID = 2, Name = "Mostar", CountryId = 1 },
                new City { ID = 3, Name = "Tuzla", CountryId = 1 },
                new City { ID = 4, Name = "Zagreb", CountryId = 2 },
                new City { ID = 5, Name = "Rijeka", CountryId = 2 },
                new City { ID = 6, Name = "Pula", CountryId = 2 },
                new City { ID = 7, Name = "Beograd", CountryId = 3 },
                new City { ID = 8, Name = "Novi Sad", CountryId = 3 },
                new City { ID = 9, Name = "Skopje", CountryId = 4 },
                new City { ID = 10, Name = "Ohrid", CountryId = 4 },
                new City { ID = 11, Name = "Sofia", CountryId = 5 },
                new City { ID = 12, Name = "Varna", CountryId = 5 },
                new City { ID = 13, Name = "Budva", CountryId = 6 },
                new City { ID = 14, Name = "Bar", CountryId = 6 },
                new City { ID = 15, Name = "Kotor", CountryId = 6 },
                new City { ID = 16, Name = "Tirana", CountryId = 7 },
                new City { ID = 17, Name = "Vlorë", CountryId = 7 },
                new City { ID = 18, Name = "Durrës", CountryId = 7 },
                new City { ID = 19, Name = "Atena", CountryId = 8 },
                new City { ID = 20, Name = "Thessaloniki", CountryId = 8 },
                new City { ID = 21, Name = "Patras", CountryId = 8 },
                new City { ID = 22, Name = "Corfu", CountryId = 8 }

             );

            modelBuilder.Entity<Country>().HasData
            (
               new Country { ID = 1, Name = "Bosna i Hercegovina" },
               new Country { ID = 2, Name = "Hrvatska" },
               new Country { ID = 3, Name = "Srbija" },
               new Country { ID = 4, Name = "Makedonija" },
               new Country { ID = 5, Name = "Bugarska" },
               new Country { ID = 6, Name = "Crna Gora" },
               new Country { ID = 7, Name = "Albanija" },
               new Country { ID = 8, Name = "Grčka" }

            );


            modelBuilder.Entity<Administrator>().HasData
           (
                 new Administrator
                 {
                     AccountID = 1,
                     Username = "admin",
                     Password = "AdminPass",
                     FirstName = "Admin",
                     LastName = "User",
                     Email = "admin@example.com"
                 },
                 new Administrator
                 {
                     AccountID = 2,
                     Username = "superadmin",
                     Password = "SuperPass",
                     FirstName = "Super",
                     LastName = "Admin",
                     Email = "superadmin@example.com"
                 },
                 new Administrator
                 {
                     AccountID = 3,
                     Username = "izelrepuh",
                     Password = "SuperPass",
                     FirstName = "Izel",
                     LastName = "Repuh",
                     Email = "izelrepuh@example.com"
                 },
                new Administrator
                {
                    AccountID = 4,
                    Username = "amaromer",
                    Password = "SuperAmar",
                    FirstName = "Amar",
                    LastName = "Omer",
                    Email = "amaromer@example.com"
                }
           );


            modelBuilder.Entity<User>().HasData(
                  new User
                  {
                      AccountID = 5,
                      Username = "johndoe",
                      Email = "johndoe@example.com",
                      Password = "JohnPass",
                      FirstName = "John",
                      LastName = "Doe",
                      Phone = "+38761000111",
                      GenderID = 1,
                      CityID = 1,
                      CreatedAt = DateTime.UtcNow
                  },

                  new User
                  {
                      AccountID = 6,
                      Username = "janedoe",
                      Email = "janedoe@example.com",
                      Password = "JanePass",
                      FirstName = "Jane",
                      LastName = "Doe",
                      Phone = "+38761000222",
                      GenderID = 2,
                      CityID = 2,
                      CreatedAt = DateTime.UtcNow
                  },

                  new User
                  {
                      AccountID = 7,
                      Username = "xxxxx",
                      Email = "xxxx@example.com",
                      Password = "xxxxx",
                      FirstName = "Xkorisnik",
                      LastName = "PKorisnik",
                      Phone = "+38761000222",
                      GenderID = 1,
                      CityID = 3,
                      CreatedAt = DateTime.UtcNow
                  },

                  new User
                  {
                      AccountID = 8,
                      Username = "yyyy",
                      Email = "yyyy@example.com",
                      Password = "YYYXX",
                      FirstName = "YYKorisnik",
                      LastName = "YYPrezime",
                      Phone = "+38761000222",
                      GenderID = 2,
                      CityID = 4,
                      CreatedAt = DateTime.UtcNow
                  },
                   new User
                   {
                       AccountID = 12,
                       Username = "izellapin",
                       Email = "izellapin@gmail.com",
                       Password = "password",
                       FirstName = "Izel    ",
                       LastName = "Repuh",
                       Phone = "+38761000222",
                       GenderID = 2,
                       CityID = 4,
                       CreatedAt = DateTime.UtcNow
                   }
            );



            modelBuilder.Entity<Owner>().HasData
            (
                new Owner
                {
                    AccountID = 9,
                    FirstName = "Izel",
                    LastName = "Repuh",
                    Username = "Izel",
                    Email = "izel@gmail.com",
                    Password = "Izel",
                    Phone = "061-000-111",
                    GenderID = 2,
                    CityID = 1,
                    CreatedAt = DateTime.UtcNow

                },
                 new Owner
                 {
                     AccountID = 10,
                     FirstName = "Maida",
                     LastName = "Kovac",
                     Username = "Maida",
                     Email = "maida@gmail.com",
                     Password = "Maida",
                     Phone = "061-000-222",
                     GenderID = 2,
                     CityID = 2,
                     CreatedAt = DateTime.UtcNow,

                 },
                 new Owner
                 {
                     AccountID = 11,
                     FirstName = "Admin",
                     LastName = "Admin",
                     Username = "Admin",
                     Email = "owner@gmail.com",
                     Password = "Admin",
                     Phone = "061-000-333",
                     GenderID = 1,
                     CityID = 3,
                     CreatedAt = DateTime.UtcNow,

                 }


            );


            modelBuilder.Entity<Apartment>().HasData
            (
                // Sarajevo
                new Apartment { ApartmentId = 1, Name = "Apartment Marshal", Description = "A stylish and modern apartment in a prime location, perfect for travelers.", Adress = "Maršala Tita 25, Sarajevo", PricePerNight = 50, CityId = 1 },
                new Apartment { ApartmentId = 2, Name = "Old Town Retreat", Description = "Charming apartment near Baščaršija with traditional decor.", Adress = "Ferhadija 12, Sarajevo", PricePerNight = 65, CityId = 1 },
                new Apartment { ApartmentId = 3, Name = "City Panorama Apartment", Description = "Modern apartment with a beautiful city view.", Adress = "Zmaja od Bosne 15, Sarajevo", PricePerNight = 80, CityId = 1 },

                // Mostar
                new Apartment { ApartmentId = 4, Name = "Apartment Charm", Description = "A cozy and charming space with elegant decor.", Adress = "Rade Bitange 8, Mostar", PricePerNight = 70, CityId = 2 },
                new Apartment { ApartmentId = 5, Name = "Bridge View Apartment", Description = "Stunning apartment with a direct view of the Old Bridge.", Adress = "Maršala Tita 9, Mostar", PricePerNight = 90, CityId = 2 },
                new Apartment { ApartmentId = 6, Name = "Herzegovina Hideaway", Description = "Quiet and relaxing stay in the heart of Herzegovina.", Adress = "Gojka Vukovića 21, Mostar", PricePerNight = 75, CityId = 2 },

                // Tuzla
                new Apartment { ApartmentId = 7, Name = "Apartment Sun", Description = "Bright and spacious apartment with plenty of natural light.", Adress = "Slatina 10, Tuzla", PricePerNight = 50, CityId = 3 },
                new Apartment { ApartmentId = 8, Name = "Central Park Apartment", Description = "Apartment next to the famous Pannonian Lakes.", Adress = "Trg Slobode 5, Tuzla", PricePerNight = 60, CityId = 3 },
                new Apartment { ApartmentId = 9, Name = "Modern City Stay", Description = "Well-furnished and comfortable apartment for travelers.", Adress = "Aleja Alije Izetbegovića 16, Tuzla", PricePerNight = 55, CityId = 3 },

                // Zagreb
                new Apartment { ApartmentId = 10, Name = "Apartment Exclusive", Description = "A luxury apartment with high-end amenities.", Adress = "Ilica 42, Zagreb", PricePerNight = 150, CityId = 4 },
                new Apartment { ApartmentId = 11, Name = "Zagreb Skyline", Description = "Penthouse with a skyline view of Zagreb.", Adress = "Savska 77, Zagreb", PricePerNight = 180, CityId = 4 },
                new Apartment { ApartmentId = 12, Name = "Upper Town Retreat", Description = "Charming old town apartment near historical landmarks.", Adress = "Radićeva 6, Zagreb", PricePerNight = 140, CityId = 4 },

                // Rijeka
                new Apartment { ApartmentId = 13, Name = "Seaside Escape", Description = "A luxurious apartment right by the Adriatic Sea.", Adress = "Obala 45, Rijeka", PricePerNight = 120, CityId = 5 },
                new Apartment { ApartmentId = 14, Name = "Port View Apartment", Description = "Beautiful apartment overlooking the Rijeka harbor.", Adress = "Korzo 10, Rijeka", PricePerNight = 130, CityId = 5 },
                new Apartment { ApartmentId = 15, Name = "Historic Coastal Home", Description = "Stay in an elegant historic coastal apartment.", Adress = "Verdijeva 3, Rijeka", PricePerNight = 110, CityId = 5 },

                // Pula
                new Apartment { ApartmentId = 16, Name = "Mountain View Lodge", Description = "A cozy retreat with a breathtaking mountain view.", Adress = "Bjelašnica 12, Pula", PricePerNight = 80, CityId = 6 },
                new Apartment { ApartmentId = 17, Name = "Roman Amphitheater Stay", Description = "Apartment next to the famous Pula Arena.", Adress = "Flavijevska 1, Pula", PricePerNight = 95, CityId = 6 },
                new Apartment { ApartmentId = 18, Name = "Pula Beachfront Getaway", Description = "Enjoy the Adriatic coast in this beachfront apartment.", Adress = "Verudela 20, Pula", PricePerNight = 100, CityId = 6 },

                // Beograd
                new Apartment { ApartmentId = 19, Name = "Belgrade Central", Description = "Modern apartment in the city center.", Adress = "Knez Mihailova 28, Beograd", PricePerNight = 110, CityId = 7 },
                new Apartment { ApartmentId = 20, Name = "Riverside Apartment", Description = "Relaxing stay by the Sava River.", Adress = "Bulevar Zorana Đinđića 35, Beograd", PricePerNight = 120, CityId = 7 },
                new Apartment { ApartmentId = 21, Name = "Luxury Skadarlija Stay", Description = "A unique apartment in the famous bohemian quarter.", Adress = "Skadarska 9, Beograd", PricePerNight = 130, CityId = 7 },

                // Novi Sad
                new Apartment { ApartmentId = 22, Name = "Urban Loft", Description = "Modern loft in the heart of the city.", Adress = "Bulevar Oslobođenja 22, Novi Sad", PricePerNight = 95, CityId = 8 },
                new Apartment { ApartmentId = 23, Name = "Petrovaradin Fortress Stay", Description = "Stay next to the historical Petrovaradin Fortress.", Adress = "Podgradje 3, Novi Sad", PricePerNight = 85, CityId = 8 },
                new Apartment { ApartmentId = 24, Name = "River Danube Apartment", Description = "Relax by the beautiful Danube River.", Adress = "Kej žrtava racije 10, Novi Sad", PricePerNight = 90, CityId = 8 },

                // Skopje
                new Apartment { ApartmentId = 25, Name = "Historic Downtown Apartment", Description = "Stay in a beautifully restored historic building.", Adress = "Stari Grad 17, Skopje", PricePerNight = 70, CityId = 9 },
                new Apartment { ApartmentId = 26, Name = "Stone Bridge View", Description = "Perfect location next to the iconic Stone Bridge.", Adress = "Keј 13 Noemvri 5, Skopje", PricePerNight = 75, CityId = 9 },
                new Apartment { ApartmentId = 27, Name = "Luxury Skopje Suite", Description = "Spacious and elegant suite in the city center.", Adress = "Macedonia Square 1, Skopje", PricePerNight = 100, CityId = 9 },

                new Apartment { ApartmentId = 31, Name = "Ohrid Lake View", Description = "Stunning views of Lake Ohrid.", Adress = "Keј Marshal Tito 11, Ohrid", PricePerNight = 80, CityId = 10 },
                new Apartment { ApartmentId = 32, Name = "Old Town Retreat", Description = "Charming apartment in Ohrid's historic center.", Adress = "Car Samoil Street 5, Ohrid", PricePerNight = 70, CityId = 10 },
                new Apartment { ApartmentId = 33, Name = "Luxury Lakeside Suite", Description = "High-end suite with a private balcony and lake view.", Adress = "Macedonia Street 15, Ohrid", PricePerNight = 120, CityId = 10 },

                // Sofia (ID: 11)
                new Apartment { ApartmentId = 34, Name = "Sofia Panorama", Description = "Penthouse apartment in the heart of Sofia.", Adress = "Vitosha Blvd 15, Sofia", PricePerNight = 110, CityId = 11 },
                new Apartment { ApartmentId = 35, Name = "Sofia Central Loft", Description = "Modern loft in downtown Sofia.", Adress = "Aleksandar Stamboliyski Blvd 22, Sofia", PricePerNight = 95, CityId = 11 },
                new Apartment { ApartmentId = 36, Name = "Luxury Business Suite", Description = "Premium business-class apartment.", Adress = "Tsarigradsko Shose 30, Sofia", PricePerNight = 130, CityId = 11 },

                // Varna (ID: 12)
                new Apartment { ApartmentId = 37, Name = "Varna Sea View", Description = "Beachfront apartment in Varna.", Adress = "Golden Sands 5, Varna", PricePerNight = 100, CityId = 12 },
                new Apartment { ApartmentId = 38, Name = "Coastal Paradise", Description = "Luxury retreat near Varna Beach.", Adress = "Slivnitsa Blvd 8, Varna", PricePerNight = 110, CityId = 12 },
                new Apartment { ApartmentId = 39, Name = "Historic Varna Stay", Description = "Cozy apartment near Varna Cathedral.", Adress = "Opalchenska Street 12, Varna", PricePerNight = 90, CityId = 12 },

                // Budva (ID: 13) - samo 1 apartman
                new Apartment { ApartmentId = 40, Name = "Budva Riviera", Description = "Luxury stay near the Budva beaches.", Adress = "Slovenska Plaža 3, Budva", PricePerNight = 120, CityId = 13 },

                // Bar (ID: 14)
                new Apartment { ApartmentId = 41, Name = "Bar Harbor View", Description = "Modern apartment overlooking the Adriatic Sea.", Adress = "Marina Street 10, Bar", PricePerNight = 95, CityId = 14 },

                // Kotor (ID: 15)
                new Apartment { ApartmentId = 42, Name = "Bay of Kotor Retreat", Description = "Enjoy breathtaking bay views from this stunning apartment.", Adress = "Kotor Bay Road 10, Kotor", PricePerNight = 115, CityId = 15 },

                // Tirana (ID: 16)
                new Apartment { ApartmentId = 43, Name = "Tirana Central Loft", Description = "Modern loft in downtown Tirana.", Adress = "Blloku District 6, Tirana", PricePerNight = 90, CityId = 16 },

                // Vlorë (ID: 17)
                new Apartment { ApartmentId = 44, Name = "Vlorë Beachfront Getaway", Description = "Beautiful beachfront apartment.", Adress = "Rruga Dhimiter Konomi 8, Vlorë", PricePerNight = 110, CityId = 17 },

                // Durrës (ID: 18)
                new Apartment { ApartmentId = 45, Name = "Durrës Marina View", Description = "Luxury apartment with a stunning harbor view.", Adress = "Port Road 7, Durrës", PricePerNight = 120, CityId = 18 },

                // Thessaloniki (ID: 20)
                new Apartment { ApartmentId = 46, Name = "White Tower View", Description = "Amazing view of the iconic White Tower.", Adress = "Nikis Avenue 8, Thessaloniki", PricePerNight = 125, CityId = 20 },

                // Athens (ID: 19)
                new Apartment { ApartmentId = 47, Name = "Luxury Penthouse", Description = "Exclusive penthouse with panoramic views.", Adress = "Skyline Tower, Vasilissis Sofias Ave 10, Athens", PricePerNight = 250, CityId = 19 },

                // Patras (ID: 21)
                new Apartment { ApartmentId = 48, Name = "Patras City Center", Description = "Modern apartment in the heart of Patras.", Adress = "Agiou Nikolaou 10, Patras", PricePerNight = 110, CityId = 21 },

                // Corfu (ID: 22)
                new Apartment { ApartmentId = 49, Name = "Corfu Beachfront Escape", Description = "Luxury stay with direct beach access.", Adress = "Paleokastritsa Road 5, Corfu", PricePerNight = 140, CityId = 22 }
             );





            modelBuilder.Entity<Rule>().HasData
           (
               new Rule { RuleID = 1, RuleText = "Zabranjeno pusenje" },
               new Rule { RuleID = 2, RuleText = "Zabranjene zabave" },
               new Rule { RuleID = 3, RuleText = "Dozvoljeni ljubimci" },
               new Rule { RuleID = 4, RuleText = "Zabranjeno prekoracenje kapaciteta osoba" },
               new Rule { RuleID = 5, RuleText = "Zabranjeno NESTO" }
           );

            modelBuilder.Entity<ApartmentRule>().HasData
            (
                new ApartmentRule { ApartmentRuleID = 1, ApartmentId = 1, RuleID = 1 },
                new ApartmentRule { ApartmentRuleID = 2, ApartmentId = 2, RuleID = 2 },
                new ApartmentRule { ApartmentRuleID = 3, ApartmentId = 3, RuleID = 3 },
                new ApartmentRule { ApartmentRuleID = 4, ApartmentId = 1, RuleID = 4 },
                new ApartmentRule { ApartmentRuleID = 5, ApartmentId = 4, RuleID = 5 }
            );


            modelBuilder.Entity<Amenity>().HasData
              (
                  new Amenity { AmenityID = 1, AmenityText = "Besplatan parking" },
                  new Amenity { AmenityID = 2, AmenityText = "Klima uređaj" },
                  new Amenity { AmenityID = 3, AmenityText = "Veš mašina" },
                  new Amenity { AmenityID = 4, AmenityText = "Pogled s terase" },
                  new Amenity { AmenityID = 5, AmenityText = "Bazen" },
                  new Amenity { AmenityID = 6, AmenityText = "Sauna" }

              );

            modelBuilder.Entity<ApartmentAmenity>().HasData
            (
                new ApartmentAmenity { ApartmentAmenityID = 1, ApartmentId = 1, AmenityID = 1 },
                new ApartmentAmenity { ApartmentAmenityID = 2, ApartmentId = 2, AmenityID = 2 },
                new ApartmentAmenity { ApartmentAmenityID = 3, ApartmentId = 3, AmenityID = 3 },
                new ApartmentAmenity { ApartmentAmenityID = 4, ApartmentId = 4, AmenityID = 4 },
                new ApartmentAmenity { ApartmentAmenityID = 5, ApartmentId = 2, AmenityID = 5 }
            );


            modelBuilder.Entity<Toiletry>().HasData
             (
                 new Toiletry { ToiletryID = 1, Name = "Sapun" },
                 new Toiletry { ToiletryID = 2, Name = "Šampon" },
                 new Toiletry { ToiletryID = 3, Name = "Regenerator" },
                 new Toiletry { ToiletryID = 4, Name = "Fen" },
                 new Toiletry { ToiletryID = 5, Name = "Peškiri" }

             );

            modelBuilder.Entity<ApartmentToiletry>().HasData
            (
                new ApartmentToiletry { ApartmentToiletryID = 1, ApartmentId = 1, ToiletryID = 1 },
                new ApartmentToiletry { ApartmentToiletryID = 2, ApartmentId = 2, ToiletryID = 2 },
                new ApartmentToiletry { ApartmentToiletryID = 3, ApartmentId = 3, ToiletryID = 3 },
                new ApartmentToiletry { ApartmentToiletryID = 4, ApartmentId = 4, ToiletryID = 4 },
                new ApartmentToiletry { ApartmentToiletryID = 5, ApartmentId = 4, ToiletryID = 5 }


            );

            modelBuilder.Entity<ApartmentImage>().HasData
            (
                new ApartmentImage { ApartmentImageID = 1, ApartmentId = 1, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 2, ApartmentId = 2, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 3, ApartmentId = 3, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 4, ApartmentId = 4, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 5, ApartmentId = 5, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 6, ApartmentId = 6, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 7, ApartmentId = 7, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 8, ApartmentId = 8, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 9, ApartmentId = 9, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 10, ApartmentId = 10, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 11, ApartmentId = 11, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 12, ApartmentId = 12, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 13, ApartmentId = 13, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 14, ApartmentId = 14, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 15, ApartmentId = 15, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 16, ApartmentId = 16, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 17, ApartmentId = 17, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 18, ApartmentId = 18, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 19, ApartmentId = 19, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 20, ApartmentId = 20, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 21, ApartmentId = 21, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 22, ApartmentId = 22, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 23, ApartmentId = 23, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 24, ApartmentId = 24, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 25, ApartmentId = 25, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 26, ApartmentId = 26, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 27, ApartmentId = 27, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 28, ApartmentId = 31, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 29, ApartmentId = 32, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 30, ApartmentId = 33, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 31, ApartmentId = 34, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 32, ApartmentId = 35, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 33, ApartmentId = 36, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 34, ApartmentId = 37, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 35, ApartmentId = 38, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 36, ApartmentId = 39, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 37, ApartmentId = 40, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 38, ApartmentId = 41, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 39, ApartmentId = 42, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 40, ApartmentId = 43, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 41, ApartmentId = 44, ImageID = 1 },
                new ApartmentImage { ApartmentImageID = 42, ApartmentId = 45, ImageID = 2 },
                new ApartmentImage { ApartmentImageID = 43, ApartmentId = 46, ImageID = 3 },
                new ApartmentImage { ApartmentImageID = 44, ApartmentId = 47, ImageID = 4 },
                new ApartmentImage { ApartmentImageID = 45, ApartmentId = 48, ImageID = 5 },
                new ApartmentImage { ApartmentImageID = 46, ApartmentId = 49, ImageID = 1 }
            );





            modelBuilder.Entity<Image>().HasData(
            new Image
            {
                ImageID = 1,
                ImagePath = ImageSeeder.GetImagePath("/images/image2.jpg") // ❌ Wrong type (byte[])
            },
            new Image
            {
                ImageID = 2,
                ImagePath = ImageSeeder.GetImagePath("/images/room1.jpg") 
            },
            new Image
            {
                ImageID = 3,
                ImagePath = ImageSeeder.GetImagePath("/images/toilet2.jpg")
            },

             new Image
             {
                 ImageID = 4,
                 ImagePath = ImageSeeder.GetImagePath("/images/room1.jpg")
             },
            new Image
            {
                ImageID = 5,
                ImagePath = ImageSeeder.GetImagePath("/images/image2.jpg") 
            },
            new Image
            {
                ImageID = 6,
                ImagePath = ImageSeeder.GetImagePath("/images/male.png") 
            },
            new Image
            {
                ImageID = 7,
                ImagePath = ImageSeeder.GetImagePath("/images/female.png") 
            }
        );

            modelBuilder.Entity<UserImage>().HasData(
                new UserImage { UserImageID = 1, AccountID = 5, ImageID = 7 }, // For John Doe
                new UserImage { UserImageID = 2, AccountID = 6, ImageID = 7 }, // For Jane Doe
                new UserImage { UserImageID = 3, AccountID = 7, ImageID = 6 }, // For Xkorisnik
                new UserImage { UserImageID = 4, AccountID = 8, ImageID = 6 },  // For YYKorisnik
                new UserImage { UserImageID = 5, AccountID = 12, ImageID = 6 }  // For YYKorisnik
            );

            modelBuilder.Entity<OwnerImage>().HasData(
                new OwnerImage { OwnerImageID = 1, AccountID = 9, ImageID = 6 }, // For Izel
                new OwnerImage { OwnerImageID = 2, AccountID = 10, ImageID = 6 }, // For Maida
                new OwnerImage { OwnerImageID = 3, AccountID = 11, ImageID = 7 }  // For Admin Owner
            );



         


        }
    }
}

