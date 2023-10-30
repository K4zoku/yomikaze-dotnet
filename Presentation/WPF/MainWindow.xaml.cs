using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<byte[]> imageBytesList = new List<byte[]>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog op = new OpenFileDialog();
        op.Title = "Select pictures";
        op.Multiselect = true;
        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";

        if (op.ShowDialog() == true)
        {
            foreach (string fileName in op.FileNames)
            {
                // Read the selected image files as bytes and store them in the list
                byte[] imageBytes = File.ReadAllBytes(fileName);
                imageBytesList.Add(imageBytes);
            }

            // Update UI or perform any other necessary actions based on the selected images
        }
    }

    private async void btnUpLoad_Click(object sender, RoutedEventArgs e)
    {
        // Set your API endpoint URL
        string apiUrl = "https://localhost:7027/api/Upload";

        foreach (var imageBytes in imageBytesList)
        {
            using (HttpClient client = new HttpClient())
            {
                // Create a new instance of MultipartFormDataContent
                MultipartFormDataContent content = new MultipartFormDataContent();

                // Add the image content to the multipart form data
                content.Add(new ByteArrayContent(imageBytes), "file", "image.jpg");

                try
                {
                    // Send the POST request to the API endpoint using multipart form data
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Handle the API response if needed
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    // Image uploaded successfully
                    //    MessageBox.Show("Image uploaded successfully!");
                    //}
                    //else
                    //{
                    //    // Error occurred while uploading the image
                    //    MessageBox.Show("Error uploading image. Status code: " + response.StatusCode);
                    //}
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., network issues) during the upload process
                    MessageBox.Show("Error uploading image: " + ex.Message);
                }
            }
        }

        // Clear the list after uploading images (if you want to upload more images, you can skip this step)
        imageBytesList.Clear();
    }

}
