using Fitness_Diary.ContentPages;


namespace Fitness_Diary
{
    

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ProfilPage", typeof(ProfilPage));
        }

    }
}
