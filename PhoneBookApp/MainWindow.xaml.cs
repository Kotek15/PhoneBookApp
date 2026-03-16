using System.Windows;
using System.Windows.Controls;


namespace PhoneBookApp;

public partial class MainWindow : Window
{
    private readonly PhoneBook _phoneBook = new();

    public MainWindow()
    {
        InitializeComponent();
        _phoneBook.LoadFromFile();
        ContactsListBox.ItemsSource = _phoneBook.Contacts;
    }

    private void AddContactButton_Click(object sender, RoutedEventArgs e)
    {
        var window = new AddContactWindow();

        if (window.ShowDialog() == true)
        {
            _phoneBook.AddContact(window.NewContact);
            ContactsListBox.Items.Refresh();
            _phoneBook.SaveToFile();
        }
    }

    private void DeleteContactButton_Click(object sender, RoutedEventArgs e)
    {
        if (ContactsListBox.SelectedItem == null)
            return;
        
        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this contact?", 
            "Delete Contact",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        
        if (result == MessageBoxResult.Yes)
        {
            var contact = (Contact)ContactsListBox.SelectedItem;
            _phoneBook.DeleteContact(contact.Number);
            ContactsListBox.Items.Refresh();
            _phoneBook.SaveToFile();
        }
    }

    private void EditContactButton_Click(object sender, RoutedEventArgs e)
    {
        var contact = (Contact)ContactsListBox.SelectedItem;
        var window = new EditContactWindow(contact);
        
        if (window.ShowDialog() == true)
        {
            ContactsListBox.Items.Refresh();
            ContactsListBox.Items.Refresh();
            _phoneBook.SaveToFile();
        }
    }

    private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
    {
        var textToSearch = SearchTextBox.Text.Trim();

        if (string.IsNullOrEmpty(textToSearch))
        {
            ContactsListBox.ItemsSource = _phoneBook.Contacts;
            return;
        }
        
        var filteredContacts = _phoneBook.Contacts.Where(c =>
            c.Name.Contains(textToSearch) ||
            c.Number.Contains(textToSearch) ||
            c.Email.Contains(textToSearch));

        ContactsListBox.ItemsSource = filteredContacts.ToList();
    }
    
    private void RefreshList()
    {
        ContactsListBox.ItemsSource = _phoneBook.Contacts;
    }
}