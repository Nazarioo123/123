using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Maui.Controls;

namespace Fitness_Diary.ContentPages
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DB.FirestoreService firebaseService;

        public RegisterPage()
        {
            InitializeComponent();
            firebaseService = new DB.FirestoreService();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;
            string username = UsernameEntry.Text;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(username))
            {
                StatusLabel.Text = "Please fill out all fields.";
                StatusLabel.TextColor = Colors.Red;
                return;
            }

            if (password != confirmPassword)
            {
                StatusLabel.Text = "Passwords do not match.";
                StatusLabel.TextColor = Colors.Red;
                return;
            }

            if (password.Length < 6 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                StatusLabel.Text = "Password must be at least 6 characters long and contain letters and numbers.";
                StatusLabel.TextColor = Colors.Red;
                return;
            }

            try
            {
                if (await firebaseService.DoesUserExist(username))
                {
                    StatusLabel.Text = "User already exists.";
                    StatusLabel.TextColor = Colors.Red;
                    return;
                }

                byte[] salt;
                string hashedPassword = PasswordHelper.HashPassword(password, out salt);

                await firebaseService.RegisterUser(username, hashedPassword, salt);

                StatusLabel.Text = "Registration successful!";
                StatusLabel.TextColor = Colors.Green;

                await Shell.Current.GoToAsync("//ProfilPage");
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Error: {ex.Message}";
                StatusLabel.TextColor = Colors.Red;
            }
        }

        private void OnCheckPasswordClicked(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                StatusLabel.Text = "Enter password and confirmation.";
                StatusLabel.TextColor = Colors.Red;
                return;
            }

            StatusLabel.Text = password == confirmPassword
                ? "Passwords match."
                : "Passwords do not match.";
            StatusLabel.TextColor = password == confirmPassword ? Colors.Green : Colors.Red;
        }
    }
}
