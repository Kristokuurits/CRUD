using Microsoft.Maui.Controls;

namespace MauiApp1.Views
{
    public partial class StartPage : ContentPage
    {
        private readonly DbService _dbService;

        public StartPage(DbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnStartPageTapped;
            stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void OnStartPageTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(_dbService));
        }
    }
}
