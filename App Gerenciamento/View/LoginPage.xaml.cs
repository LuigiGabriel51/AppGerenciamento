using App_Gerenciamento.Models;
using Newtonsoft.Json;
using System.Text;

namespace App_Gerenciamento.Telas;

public partial class LoginPage : ContentPage
{
    RestService restService = new RestService();
    public LoginPage()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    
    private async void EnterClicked(object sender, EventArgs e)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {

            bool verify = await restService.SaveLogin(cpf.Text, senha.Text);
            if (verify) {
                cpf.Text = string.Empty;
                senha.Text = string.Empty;

                TokenManager tokenManager = new TokenManager();
                tokenManager.AddToken(Login.AcessToken);

                await Shell.Current.GoToAsync("///principal/agenda"); 
            }
            else res.Text = "Credenciais Inválidas";

            //            await Navigation.PushAsync(new AppShell());
        }
        else
        {
            await DisplayAlert("Alerta", "Você precisa de Internet para acessar o Aplicativo Gerenciamento", "OK");
        }

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (cpf.Text != null)
        {
            Login.Cpf = cpf.Text;
            int very = Convert.ToInt32(await restService.SaveContato(cpf.Text));
            if (very == 200) { await Navigation.PushAsync(new App_Gerenciamento.PageRecuperacao()); }
            else res.Text = "CPF inválido";
        }
        else res.Text = "digite seu cpf para recuperação de senha";

    }

    private void cpf_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (e.NewTextValue.Length == 11)
        {
            esqueceu.TextColor = Color.FromArgb("#7D26CD");
            entrar.TextColor = Color.FromArgb("#000000");
            esqueceu.IsEnabled = true;
            entrar.IsEnabled = true;
        }
        else
        {
            esqueceu.TextColor = Color.FromArgb("#9C9C9C");
            entrar.TextColor = Color.FromArgb("#9C9C9C");
            esqueceu.IsEnabled = false;
            entrar.IsEnabled = false;
        }

    }
}