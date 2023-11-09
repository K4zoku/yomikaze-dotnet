using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;

namespace Yomikaze.WPF;

/// <summary>
/// Interaction logic for Login.xaml
/// </summary>
public partial class Login : Window
{
    private readonly ManageComics _adminWindow;
    private readonly HttpClient _client;

    public Login(ManageComics adminWindow, HttpClient client)
    {
        InitializeComponent();
        _adminWindow = adminWindow;
        _client = client;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        button.IsEnabled = false;
        Cursor = Cursors.Wait;

        await ApiLogin(TxtUsername.Text, TxtPassword.Password);

        button.IsEnabled = true;
        Cursor = Cursors.Arrow;
    }

    private async Task ApiLogin(string username, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/API/V1/Authentication/SignIn");
        var data = new SignInModel { Username = username, Password = password };
        var json = JsonConvert.SerializeObject(data);
        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        var responseJson = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseModel<TokenModel>>(responseJson);

        if (!result.Success)
        {
            MessageBox.Show($"Sign In failed: {result.Message}");
            return;
        }
        var token = result.Data?.Token;
        if (token == null)
        {
            MessageBox.Show("Error when getting token");
            return;
        }
        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var roles = jwt.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        if (!roles.Contains("Administrator"))
        {
            MessageBox.Show("You are not an admin");
            return;
        }
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        _adminWindow.Show();
        Close();
    }
}
