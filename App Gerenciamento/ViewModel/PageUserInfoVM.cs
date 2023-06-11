using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Gerenciamento.Models;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_Gerenciamento.ViewModel
{
    public class PageUserInfoVM: ObservableObject
    {
        private string _Nome;
        private string _Cpf;
        private string _Email;
        private string _DataN;
        private string _Numero;
        private string _Sexo;
        private string _Cargo;

        public string Nome
        {
            get { return _Nome; }
            set { SetProperty(ref _Nome, value); }
        }

        public string Cpf
        {
            get { return _Cpf; }
            set { SetProperty(ref _Cpf, value); }
        }
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }
        public string DataN
        {
            get { return _DataN; }
            set { SetProperty(ref _DataN, value); }
        }
        public string Numero
        {
            get { return _Numero; }
            set { SetProperty(ref _Numero, value); }
        }
        public string Sexo
        {
            get { return _Sexo; }
            set { SetProperty(ref _Sexo, value); }
        }
        public string Cargo
        {
            get { return _Cargo; }
            set { SetProperty(ref _Cargo, value); }
        }

        public IAsyncRelayCommand getInfoUser;

        public PageUserInfoVM()
        {
            getInfoUser = new AsyncRelayCommand(getUser);
        }
        private async Task getUser()
        {
            List<string> user = new List<string>();
            user.Add(Login.Nome);
            user.Add(Login.Cpf);
            user.Add(Login.Email);
            user.Add(Login.DataN);
            user.Add(Login.Numero);
            user.Add(Login.Sexo);
            user.Add(Login.Cargo);
            Nome = user[0];
            Cpf = user[1];
            Email = user[2];
            DataN = user[3];
            Numero = user[4];
            Sexo = user[5];
            Cargo = user[6];
        }
    }
    
}
