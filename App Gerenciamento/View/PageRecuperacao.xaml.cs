using System.ComponentModel;
using App_Gerenciamento.Telas;
using System.Diagnostics;
using App_Gerenciamento.Models;

namespace App_Gerenciamento;

public partial class PageRecuperacao : ContentPage
{
    private bool _opcao1;
    private bool _opcao2;
    private string cpf = Login.Cpf;
    RestService restService = new RestService();

    public PageRecuperacao()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
	}

    public bool Opcao1
    {
        get => _opcao1;
        set
        {
            if (_opcao1 != value)
            {
                _opcao1 = value;
                OnPropertyChanged(nameof(Opcao1));
            }
        }
    }

    public bool Opcao2
    {
        get => _opcao2;
        set
        {
            if (_opcao2 != value)
            {
                _opcao2 = value;
                OnPropertyChanged(nameof(Opcao2));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void continuar_Clicked(object sender, EventArgs e)
    {
        Random random = new Random();
        string codigo = Convert.ToString(random.Next(100000, 1000000));

        if (op1.IsChecked == true)
        {   
            await restService.SendMsgEmail(InfoContato.email, codigo);
            int Codigo = Convert.ToInt32(codigo);
            int result = Convert.ToInt32(await DisplayPromptAsync("Digite o código que enviamos para o email"+InfoContato.email+".", "CÓDIGO: "));
            bool confirmado = true;
            while (result != Codigo && confirmado != true)
            {
                result = Convert.ToInt32(await DisplayPromptAsync("ERRO, código incorreto, Digite o código que enviamos para o numero" + InfoContato.email + ".", "CÓDIGO: "));
                confirmado = await DisplayAlert("Confirmação", "Deseja confirmar?", "Sim", "Não");
            }
            if (result == Codigo) await Shell.Current.GoToAsync("//TelaNVsenha");
        }
        if (op2.IsChecked == true)
        {
            //await restService.SendMsgTel(InfoContato.telefone);
            await restService.SendMsgTel(InfoContato.telefone, codigo);
            int Codigo = Convert.ToInt32(codigo);
            int result = Convert.ToInt32(await DisplayPromptAsync("Digite o código que enviamos para o numero" + InfoContato.email + ".", "CÓDIGO: "));
            while (result != Codigo) {
                result = Convert.ToInt32(await DisplayPromptAsync("ERRO, código incorreto, Digite o código que enviamos para o numero" + InfoContato.telefone + ".", "CÓDIGO: "));
            }
            if (result == Codigo) await Shell.Current.GoToAsync("//TelaNVsenha");
        }
        
    }
}