using System.Windows;

namespace PhoneBookApp;

public partial class AddContactWindow : Window
{
    public Contact? NewContact { get; private set; }

    public AddContactWindow()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        bool isNameValid = PhoneBook.NameValidation(NameInput.Text.Trim());
        bool isNumberValid = PhoneBook.NumberValidation(NumberInput.Text.Trim());
        bool isEmailValid = PhoneBook.EmailValidation(EmailInput.Text.Trim());
        
        PhoneBook.SetValidationState(NameInput, NameErrorText, isNameValid);
        PhoneBook.SetValidationState(NumberInput, NumberErrorText, isNumberValid);
        PhoneBook.SetValidationState(EmailInput, EmailErrorText, isEmailValid);
        
        if (isNameValid && isNumberValid && isEmailValid)
        {
            NewContact = new Contact
            (
                name: NameInput.Text.Trim(),
                number: NumberInput.Text.Trim(),
                email: EmailInput.Text.Trim()
            );
            
            DialogResult = true;
            Close();
        }
    }
}