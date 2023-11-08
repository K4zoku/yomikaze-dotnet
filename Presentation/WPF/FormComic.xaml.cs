using System.Windows;
using Yomikaze.Application.Data.Models.Common;

namespace Yomikaze.WPF;

/// <summary>
/// Interaction logic for ListComic.xaml
/// </summary>
public partial class FormComic : Window
{
    private readonly YomikazeClient _client;
    public FormComic(YomikazeClient client)
    {
        InitializeComponent();
        _client = client;
    }

    private void btnSave(object sender, RoutedEventArgs e)
    {
        if (!(DataContext is ComicModel model)) return;


    }
}
