using System.Windows;

namespace PhoneBookApp;

public partial class EditContactWindow : Window
{
    private Contact _contact;
    
    public EditContactWindow(Contact contact)
    {
        InitializeComponent();
        _contact = contact;

        NameEditInput.Text = contact.Name;
        NumbeEditInput.Text = contact.Number;
        EmailEditInput.Text = contact.Email;
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        bool isNameValid = PhoneBook.NameValidation(NameEditInput.Text.Trim());
        bool isNumberValid = PhoneBook.NumberValidation(NumbeEditInput.Text.Trim());
        bool isEmailValid = PhoneBook.EmailValidation(EmailEditInput.Text.Trim());
        
        PhoneBook.SetValidationState(NameEditInput, NameEditErrorText, isNameValid);
        PhoneBook.SetValidationState(NumbeEditInput, NumberEditErrorText, isNumberValid);
        PhoneBook.SetValidationState(EmailEditInput, EmailEditErrorText, isEmailValid);
        
        if (isNameValid && isNumberValid && isEmailValid)
        {
            _contact.Name = NameEditInput.Text.Trim();
            _contact.Number = NumbeEditInput.Text.Trim();
            _contact.Email = EmailEditInput.Text.Trim();
            
            DialogResult = true;
            Close();
        }
    }
}