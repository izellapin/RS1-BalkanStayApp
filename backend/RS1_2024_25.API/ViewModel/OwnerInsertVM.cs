namespace RS1_2024_25.API.ViewModel
{
    public class OwnerInsertVM
    {
        // Polja iz Account klase
        public string Username { get; set; }
        public string Password { get; set; } // Obavezno hashirajte prije spremanja
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Polja specifična za Owner
        public string Phone { get; set; }
        public int CityID { get; set; } // Obavezno validirati da li CityID postoji
        public int GenderID { get; set; } // Obavezno validirati da li GenderID postoji
        public string ImagePath { get; set; } // Changed from byte[] Image
    }


}
