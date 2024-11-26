using Fitness_Diary.ContentPages; 

namespace Fitness_Diary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new ProfilPage();
        }
    }
}
