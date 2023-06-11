using App_Gerenciamento.Telas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gerenciamento.ViewModel
{
    public class PageSuporteVM: ObservableObject
    {

        private List<MSGfaq> _listfaq;
        public List<MSGfaq> ListFaq
        {
            get { return _listfaq; }
            set { SetProperty(ref _listfaq, value); }
        }

        public IAsyncRelayCommand getFAQ;
        public PageSuporteVM()
        {
            getFAQ = new AsyncRelayCommand(getfaq);
        }
        private async Task getfaq()
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

            for (int i = 0; i < 3; i++)
            {
                MSGfaq faq = new MSGfaq
                {
                    pergunta = perguntas[i],
                    resposta = respostas[i]
                };

                listFAQ.Add(faq);
            }
            ListFaq = listFAQ;
        }
    }
}
