using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using App_Gerenciamento.Models;
using App_Gerenciamento.ViewModel;

namespace App_Gerenciamento.Telas;

public partial class TelaUser : ContentPage
{
    RestService restService = new RestService();
    public TelaUser()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        BindingContext = new PageUserInfoVM();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//atualizaDados");
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//novaSenha");
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        bool logout =  await DisplayAlert("Alerta", "VOC� DESEJA FAZER LOGOUT?", "SIM", "N�O");

        if (logout == true) {
            var loginPage = new LoginPage();
            img.Source = "perfil.png";
            Login.Cpf = null;
            TokenManager tokenManager = new TokenManager();
            List<string> token = tokenManager.GetTokens();
            if (token.Count > 0)
            {
                string firstToken = token[0];
                Console.WriteLine(firstToken);
                tokenManager.RemoveToken(firstToken);
            }
            await Shell.Current.GoToAsync("//login");
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PageUserInfoVM viewmodel)
        {
            await UpdateImage();
            viewmodel.getInfoUser.Execute(null);
        }
    }

    public async Task UpdateImage()
    {
        const string Url = "https://1db5-2804-5e7c-f831-8d00-2def-8216-48bf-f843.ngrok-free.app/sendImagePerfil";
        try
        {
            modeloLogin pessoa = new modeloLogin
            {
                id = Login.UserID,
            };
            string jsonString = JsonConvert.SerializeObject(pessoa);
            StringContent dadosLogin = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                var client = new HttpClient();
                response = await client.PostAsync(Url, dadosLogin);

                // Verifica se a resposta tem sucesso
                response.EnsureSuccessStatusCode();

                // L� a imagem como um array de bytes
                var imageBytes = await response.Content.ReadAsByteArrayAsync();

                // Cria um objeto StreamImageSource a partir do array de bytes da imagem
                var imagem = new StreamImageSource();
                imagem.Stream = (token) => Task.FromResult((System.IO.Stream)new System.IO.MemoryStream(imageBytes));

                Console.WriteLine(@"\t Requisi��o bem sucedida" + imagem.Stream);
                // Exibe a imagem em uma ImageView
                img.Source = imagem;
            }
            catch (Exception)
            {
                Console.WriteLine(@"\t Requisi��o falhou.");
                // Trata erros de requisi��o HTTP ou outros erros
                await DisplayAlert("Erro", "N�o foi poss�vel exibir sua imagem de perfil.", "OK");
            }
        }
        catch (Exception)
        {
            // Trata erros de serializa��o ou outros erros
            await DisplayAlert("Erro", "N�o foi poss�vel exibir sua imagem de perfil.", "OK");
        }
    }

}