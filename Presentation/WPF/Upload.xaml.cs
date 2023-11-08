using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using WPF;
using Yomikaze.Application.Data.Models.Response;

namespace Yomikaze.WPF;
/// <summary>
/// Interaction logic for Upload.xaml
/// </summary>
public partial class Upload : Window
{
    private readonly Storyboard storyboard = new();
    private readonly HttpClient _httpClient;
    public Upload(HttpClient httpClient)
    {
        InitializeComponent();
        _httpClient = httpClient;
        DoubleAnimation opacityAnimation = new()
        {
            From = 1,
            To = 0.4,
            Duration = new Duration(TimeSpan.FromSeconds(0.5)),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever
        };
        Storyboard.SetTarget(opacityAnimation, ImageDrop);
        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
        storyboard.Children.Add(opacityAnimation);
    }

    private void ImageDrop_Drop(object sender, DragEventArgs e)
    {
        storyboard.Stop();
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddToList(files);
        }
    }


    private void ImageDrop_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop) && sender != e.Source)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (FilterFiles(files))
            {
                // Add effect on dragging
                e.Effects = DragDropEffects.Copy;

                storyboard.Begin();
                return;
            }

        }
        e.Effects = DragDropEffects.None;
    }

    private static bool FilterFiles(string[] files)
    {
        return Array.TrueForAll(files, file => file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".webp"));
    }

    private void ImageDrop_DragLeave(object sender, DragEventArgs e)
    {
        storyboard.Stop();
    }

    private void ImageDrop_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.webp";
        openFileDialog.Multiselect = true;
        var result = openFileDialog.ShowDialog() ?? false;
        if (!result) return;
        var files = openFileDialog.FileNames;
        AddToList(files);
    }

    private void AddToList(string[] files)
    {
        foreach (var file in files)
        {
            UploadProgressList.Items.Add(new UploadProgress { FileName = Path.GetFileName(file), Progress = 0, FilePath = file });
        }
    }

    private void UploadToServer()
    {
        foreach (var item in UploadProgressList.Items)
        {
            var upload = (UploadProgress)item;
            var request = new HttpRequestMessage(HttpMethod.Post, "/API/V1/Images/Upload");
            var formData = new MultipartFormDataContent
            {
                { new StreamContent(File.OpenRead(upload.FilePath)), "file", upload.FileName}
            };
            request.Content = formData;
            _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ContinueWith(async responseTask =>
                {
                    HttpResponseMessage response = await responseTask;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var uploadResult = JsonConvert.DeserializeObject<ResponseModel<ImageUploadResponse>>(result);
                        if (result is not null)
                        {
                            upload.Progress = 100;
                            upload.Result = $"{App.API_SERVER}{uploadResult.Data?.Url}";
                            Results.AppendText(upload.Result + "\n");
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    public class UploadProgress
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Progress { get; set; }
        public string Result { get; set; }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        UploadToServer();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        UploadProgressList.Items.Clear();
        Results.Text = string.Empty;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(Results.Text);
        MessageBox.Show("Copied results to clipboard");
    }
}
