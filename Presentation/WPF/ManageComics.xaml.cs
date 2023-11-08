using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using WPF;
using Yomikaze.Application.Data.Models.Common;

namespace Yomikaze.WPF;

/// <summary>
/// Interaction logic for Admin.xaml
/// </summary>
public partial class ManageComics : Window
{
    private readonly YomikazeClient _client;
    public ManageComics(YomikazeClient client)
    {
        InitializeComponent();
        _client = client;
    }

    private void btn_Genre(object sender, RoutedEventArgs e)
    {
        Admin2 objAdmin2 = new Admin2();
        objAdmin2.Show();
        Close();
    }

    private void btn_User(object sender, RoutedEventArgs e)
    {
        Admin1 objAdmin1 = new Admin1();
        objAdmin1.Show();
        Close();
    }

    private void bntLogOut(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("You Log Out!");
    }

    private void BtnDelete(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("You Delete!!!");
    }

    private async Task LoadData()
    {
        var comics = await _client.GetComics() ?? new();
        lvListComics.ItemsSource = comics;
    }

    private async void btnCreate_Click(object sender, RoutedEventArgs e)
    {

        var form = App.Services.GetRequiredService<FormComic>();
        var comic = new ComicModel();
        comic.Name = "TESt";
        form.DataContext = comic;
        form.ShowDialog();
        await LoadData();
    }

    private async void Window_ContentRendered(object sender, System.EventArgs e)
    {
        await LoadData();
    }
}

