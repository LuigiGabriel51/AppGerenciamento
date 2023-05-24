using Newtonsoft.Json.Linq;
using System;
using static System.Net.WebRequestMethods;
using System.Text;
using App_Gerenciamento.rest_services;

namespace App_Gerenciamento;

public partial class AppShell : Shell
{

	public AppShell()
	{
        validationLogin();
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        
    }
    private async void validationLogin()
    {
        TokenManager tokenManager = new TokenManager();
        List<string> token = tokenManager.GetTokens();

        if (token.Count > 0)
        {
            string firstToken = token[0];
            Console.WriteLine(firstToken);
            ApiChecker checker = new ApiChecker("https://1d6e-45-178-172-160.ngrok-free.app/Validation");
            var val = await checker.PostData(firstToken);
            if (val)
            {
                Console.WriteLine("token iniciado com sucesso");
                await Shell.Current.GoToAsync("///principal/agenda");
            }
            else
            {
                tokenManager.RemoveToken(firstToken);
            }
        }
        else
        {
            Console.WriteLine("Não há tokens disponíveis.");
        }
    }

    public class ApiChecker
    {
        private HttpClient httpClient;
        private string apiUrl;

        public ApiChecker(string apiUrl)
        {
            this.apiUrl = apiUrl;
            httpClient = new HttpClient();
        }

        public async Task<bool> PostData(string requestData)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", requestData);

                // Faça uma requisição GET para a API
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Verifique se a resposta foi bem-sucedida (código 200-299)
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return true; // Retorne o conteúdo da resposta da API
                }
                else
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Trate exceções de requisição ou outros erros
                Console.WriteLine($"Erro na requisição: {ex.Message}");
            }

            return false;
        }
    }
}
