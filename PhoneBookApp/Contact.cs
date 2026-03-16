using System.Text.Json.Serialization;

namespace PhoneBookApp;

public class Contact
{
    public Contact() { }
    
    public Contact(string name, string number, string email)
    {
        Name = name;
        Number = number;
        Email = email;
    }
    
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
}