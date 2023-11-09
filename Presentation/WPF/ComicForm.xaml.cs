using System;
using System.Windows;
using Yomikaze.Application.Data.Models.Request;

namespace Yomikaze.WPF;

/// <summary>
/// Interaction logic for ListComic.xaml
/// </summary>
public partial class ComicForm : Window
{
    private readonly YomikazeClient _client;
    public ComicForm(YomikazeClient client)
    {
        InitializeComponent();
        _client = client;
    }

    private async void btnSave(object sender, RoutedEventArgs e)
    {
        if (DataContext is not ComicRequestModel model) return;
        try
        {
            await _client.CreateComic(model);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
            return;
        }
        MessageBox.Show("Create success!");
        Close();
    }
}
