using App_Gerenciamento.rest_services;
using System.Windows.Input;

namespace App_Gerenciamento.Telas;

public partial class TelaNVsenha : ContentPage
{
    RestService restService = new RestService();
    public TelaNVsenha()
	{
        InitializeComponent();
    }
    
    private async void Button_Clicked(object sender, EventArgs e)
    {
		if(novaSenha1.Text == novaSenha2.Text)
		{
			bool verification = await restService.AlterSenha(Login.Cpf, novaSenha2.Text);
            if (verification)
            {
                await DisplayAlert("Nova Senha", "sua senha foi alterada com sucesso", "ok");
                await Shell.Current.GoToAsync("//login");
            }
            else res.Text = "senha nao foi alterada";
        }else res.Text = "As senhas n�o coincidem!";
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }
}