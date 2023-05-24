using App_Gerenciamento.rest_services;
using System.ComponentModel;
using System.Diagnostics;

namespace App_Gerenciamento.Telas
{
    public partial class TelaAgenda : ContentPage
    {
        RestService restService = new RestService();
        public TelaAgenda()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();        
            BindingContext = new MyViewModel();
            InicializaAgenda();
        }
        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Ldata.Text = e.NewDate.ToString("dd/MM/yyyy");
            Console.WriteLine(Ldata.Text);
            var compromissos = await restService.RequestAgenda(Ldata.Text);
            if (compromissos != null && compromissos.Count > 0)
            {
                listAgenda.ItemsSource = compromissos;
                infoData.Text = "Selecione um compromisso para visualizar mais detalhes";
                eventos.IsVisible = true;
            }
            else
            {
                listAgenda.ItemsSource = null;
                infoData.Text = "Não há compromissos marcados para essa data";
                eventos.IsVisible = false;
            }
        }

        public async void InicializaAgenda()
        {
            string dData;
            dData = DateTime.Now.ToString("dd/MM/yyyy");
            var compromissos = await restService.RequestAgenda(dData);
            if (compromissos != null)
            {
                listAgenda.ItemsSource = compromissos;
            }
        }

        private void listAgenda_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Servicos objetoSelecionado = (Servicos)e.SelectedItem;

                // Obtém as propriedades do objeto selecionado
                string nome = objetoSelecionado.Nome;
                string horario = objetoSelecionado.horario;
                string dia = objetoSelecionado.dia;
                string descricao = objetoSelecionado.descricao;

                string message = "\n Atividade: " + nome+ "\n \n Horário da Atividade: " + horario + "\n \n Data da Atividade: " + dia + "\n \n*Obs: " + descricao + "";
                DisplayAlert("Atividades", message, "OK");
            }
            listAgenda.SelectedItem = null;
        }
        

    }

    public class MyViewModel : INotifyPropertyChanged
    {
        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MyViewModel()
        {
            Data = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}