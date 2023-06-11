using App_Gerenciamento.Models;
using App_Gerenciamento.ViewModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using System.Collections;

namespace App_Gerenciamento.Telas;

public partial class TelaSuporte : ContentPage
{
    bool bt1 = false;
    bool bt2 = false;

    public TelaSuporte()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        BindingContext = new PageSuporteVM();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        if(faq.IsVisible == true)
        {
            faq.IsVisible = false;
            btFaq.Text = "▲ FAQ";
        }
        else if(faq.IsVisible == false)
        {
            faq.IsVisible = true;
            btFaq.Text = "▼ FAQ";
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        if(duv.IsVisible == true)
        {
            duv.IsVisible = false;
            btDuv.Text = "▲ DÚVIDAS OU PROBLEMAS TÉCNICOS";

        }
        else if (duv.IsVisible == false)
        {
            duv.IsVisible = true;
            btDuv.Text = "▼ DÚVIDAS OU PROBLEMAS TÉCNICOS";
        }
    }
    
    [Obsolete]
    private void btCarta_Clicked(object sender, EventArgs e)
    {
        if(bt1 == false)
        {
            btCarta.BackgroundColor = Color.FromHex("#B5B5B5");
            btChatbot.BackgroundColor = Color.FromHex("#FFEC8B");
            bt1 = true;
            frameCarta.IsVisible = true;
            frameAD.IsVisible = false;
        }
        else
        {
            btCarta.BackgroundColor = Color.FromHex("#FFEC8B");
            bt1 = false;
            frameCarta.IsVisible = false;
        }
    }

    [Obsolete]
    private async void btCarta_Clicked2(object sender, EventArgs e)
    {
        if(bt2 == false)
        {
            frameCarta.IsVisible = false;
            frameAD.IsVisible = true;
            btChatbot.BackgroundColor = Color.FromHex("#B5B5B5");
            btCarta.BackgroundColor = Color.FromHex("#FFEC8B");
            bt2 = true;
            
        }
        else
        {
            btChatbot.BackgroundColor = Color.FromHex("#FFEC8B");
            bt2 = false;
            frameAD.IsVisible = false;
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        MensagemSuporte requisicao = new MensagemSuporte {
            nome = nome.Text,
            email = email.Text,
            tel = telefone.Text,
            cargo = cargo.SelectedItem.ToString(),
            motivo = motivo.SelectedItem.ToString(),
            mensagem = msg.Text
            
        };
        RestService rest = new RestService();
        await rest.SuporteTecnico(requisicao);
        await DisplayAlert("Suporte Tecnico", "Seu Formulário foi enviado para um email da Empresa, entraremos em contato para resolver o problema o mais rápido possível", "ok");
        nome.Text = null;
        email.Text = null;
        telefone.Text = null;
        cargo.SelectedItem = null;
        motivo.SelectedItem = null;
        msg.Text = null;
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        
        RestService rest = new RestService();
        await rest.ChatPost(Login.Nome, Mensagem.Text);
        Mensagem.Text = null;
        var msgs = await rest.ChatpGet();
        listMSG.ItemsSource = msgs;
        if (msgs.Count > 0)
        {
            var lastMsg = msgs[msgs.Count - 1];
            listMSG.ScrollTo(lastMsg, ScrollToPosition.End, false);
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        RestService rest = new RestService();
        var msgs = await rest.ChatpGet();
        listMSG.ItemsSource = msgs;
        if (msgs.Count > 0)
        {
            var lastMsg = msgs[msgs.Count - 1];
            listMSG.ScrollTo(lastMsg, ScrollToPosition.End, false);
        }
        if(BindingContext is PageSuporteVM view)
        {
            view.getFAQ.Execute(null);
        }
    }

    private void Mensagem_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (!string.IsNullOrWhiteSpace(Mensagem.Text))
        {
            enviar.TextColor = Color.FromArgb("#A020F0");
            enviar.IsEnabled = true;
        }
        else
        {
            enviar.TextColor = Color.FromArgb("#BEBEBE");
            enviar.IsEnabled = false;
        }
    }

}


public class MSGfaq
{
    public string pergunta { get; set; }
    public string resposta { get; set; }
}
public class MensagemSuporte
{
    public string nome { get; set; }
    public string email { get; set; }
    public string tel { get; set; }
    public string cargo { get; set; }
    public string motivo { get; set; }
    public string mensagem { get; set;}
}