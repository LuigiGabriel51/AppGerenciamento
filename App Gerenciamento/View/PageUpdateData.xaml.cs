using App_Gerenciamento.Models;

namespace App_Gerenciamento.Telas;

public partial class PageUpdateData : ContentPage
{
    string E;
    string T;
    string S;
    string C;

    RestService RS = new RestService();
    public PageUpdateData()
	{
		InitializeComponent();
		
	}

    private async void Atualiza(object sender, EventArgs e)
    {
        {
            T = telefone.Text;
            E = email.Text;
            bool confirm = await DisplayAlert("CONFIRMAR", "Seus dados serão atualizado, tem certeza que quer continuar?", "Sim", "Nãp");
            if (confirm)
            {
                bool atual = await RS.UpdateData(Login.Cpf, E, T, S, C);
                if (atual)
                {
                    await DisplayAlert("AVISO", "Seus dados foram atualizados com sucesso", "ok");
                }
                else
                {
                    await DisplayAlert("AVISO", "Seus dados não foram atualizados, verifique se você digitou nomes diferentes", "ok");
                }
            }
        }
	

    }

    private void cargoSelect(object sender, EventArgs e)
    {
        var carg = (sender as Picker).SelectedItem.ToString();
        C = carg;
    }

    private void generoSelect(object sender, EventArgs e)
    {
        var sex = (sender as Picker).SelectedItem.ToString();
        S = sex;
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///principal/perfil");
    }
}