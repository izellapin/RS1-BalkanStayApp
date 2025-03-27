namespace RS1_2024_25.API.ViewModel
{
    public class UserUpdateVM
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int GenderID { get; set; }
        public int CityID { get; set; }
        public string ImagePath { get; set; }
    }
}
