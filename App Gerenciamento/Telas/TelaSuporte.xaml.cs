using App_Gerenciamento.rest_services;
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
        FAQlist.ItemsSource = getFAQ();
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
    public List<MSGfaq> getFAQ()
    {
        List<MSGfaq> listFAQ = new List<MSGfaq>();

        List<string> perguntas = new List<string>
        {
        " ➢ Como posso adicionar um novo evento na semana?",
        " ➢ Não estou conseguindo visualizar meus projetos em execução, o que devo fazer? Onde posso ver o progresso atual do meu projeto?",
        " ➢ Como posso ficar em alerta quanto a metas importantes do meu projeto? Não estou conseguindo marcar como concluída uma determinada etapa do meu projeto, o que devo fazer?",
        " ➢ Ainda estou com dúvidas ou encontrei um problema no aplicativo, qual o próximo passo?"
    };
        List<string> respostas = new List<string>
        {
        "Vá para a área agenda e clique em Adicionar.",
        "Tente verificar se você está conectado a Internet, se não resolver, tente entrar em contato com um atendente.",
        "Clique na opção 'DÚVIDAS OU PROBLEMAS TÉCNICOS' e fale com um atendente.",
        " Se você possui alguma dúvida e ela não pôde ser respondida pela FAQ, recomendamos que utilize nossa aba de Dúvidas ou Problemas Técnicos , onde poderá entrar em contato conosco por meio de um e-mail ou falando diretamente com um de nossos atendentes"

        };




        
        for(int i = 0; i < 3; i++) {
            MSGfaq faq = new MSGfaq
            {
                pergunta = perguntas[i],
                resposta = respostas[i]
            };
            
            listFAQ.Add(faq);
                }
        return listFAQ;
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