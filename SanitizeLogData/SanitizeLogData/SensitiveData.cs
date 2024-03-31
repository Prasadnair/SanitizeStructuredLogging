using Destructurama.Attributed;

namespace SanitizeLogData
{
    public class SensitiveData
    {

        [NotLogged]
        public string Password { get; set; }
        public string Username { get; set; }
        public Address Address { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }       
        public string ZipCode { get; set; }
    }
        

       
}
