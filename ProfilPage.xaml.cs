using System;
using System.Threading.Tasks;

namespace Fitness_Diary.ContentPages
{
    public partial class ProfilPage : ContentPage
    {
        private readonly DB.FirestoreService firestoreService;

        public ProfilPage()
        {
            InitializeComponent();
            firestoreService = new DB.FirestoreService();
        }

        private async void OnSendButton(object sender, EventArgs e)
        {
            try
            {
                string username = "Adiwas";
                string postContent = "test";

                await firestoreService.AddPostAsync(username, postContent);

                await DisplayAlert("Success", "Post sent successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error sending post: {ex.Message}", "OK");
            }
        }
    }
}
