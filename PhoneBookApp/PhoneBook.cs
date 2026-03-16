using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mail;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace PhoneBookApp;

public class PhoneBook
{
    private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.json");
    private ObservableCollection<Contact> _contacts = new();
    public IReadOnlyList<Contact> Contacts => _contacts;

    public void SaveToFile()
    {
        var json = JsonSerializer.Serialize(Contacts);
        File.WriteAllText(_filePath, json);
    }
    
    public void LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return;

        var json = File.ReadAllText(_filePath);
        var contactsFromFile = JsonSerializer.Deserialize<ObservableCollection<Contact>>(json);

        if (contactsFromFile != null)
        {
            foreach (var contact in contactsFromFile)
            {
                _contacts.Add(contact);
            }
        }
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void DeleteContact(string number)
    {
        var contact = _contacts.FirstOrDefault(c => c.Number == number);

        if (contact == null)
            return;

        _contacts.Remove(contact);
    }
    
    public static bool EmailValidation(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
            return false;
            
        try
        {
            var parsedEmail = new MailAddress(email);
            return parsedEmail.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    public static bool NameValidation(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public static bool NumberValidation(string number)
    {
        return !string.IsNullOrWhiteSpace(number);
    }

    public static void SetValidationState(TextBox input, TextBlock errorText, bool isValid)
    {
        input.BorderBrush = isValid ? Brushes.Black : Brushes.Red;
        errorText.Visibility = isValid ? Visibility.Hidden : Visibility.Visible;
    }
}