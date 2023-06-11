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
    public class PageMissoesVM : ObservableObject
    {
        private List<Projetos> _projetos;
        public List<Projetos> Projetos
        {
            get { return _projetos; }
            set { SetProperty(ref _projetos, value); }
        }

        public IAsyncRelayCommand getProjetos;

        public PageMissoesVM()
        {
            getProjetos = new AsyncRelayCommand(getprojetos);
        }

        private async Task getprojetos()
        {
            RestService rs = new RestService();
            Projetos = await rs.requestProjetos();
        }
    }
}
