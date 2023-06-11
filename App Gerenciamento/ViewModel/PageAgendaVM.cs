using App_Gerenciamento.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gerenciamento.ViewModel
{
    public class PageAgendaVM: ObservableObject
    {
        private string _agenda;
        public string Agenda { 
            get { return _agenda; }
            set {SetProperty(ref _agenda, value); }
        }  

        private string _date;
        public string Data
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private List<Servicos> _servicos;
        public List<Servicos> Servicos
        {
            get { return _servicos; }
            set { SetProperty(ref _servicos, value); }
        }

        public IAsyncRelayCommand Inicialize;
        public PageAgendaVM()
        {
            Data = DateTime.Now.ToString("dd/MM/yyyy");
            Inicialize = new AsyncRelayCommand(InicializaAgenda);
        }

        public async Task InicializaAgenda()
        {
            RestService rs = new RestService();
            string dData;
            dData = DateTime.Now.ToString("dd/MM/yyyy");
            var compromissos = await rs.RequestAgenda(dData);
            if (compromissos != null)
            {
                Servicos = compromissos;
            }
        }
    }
}
