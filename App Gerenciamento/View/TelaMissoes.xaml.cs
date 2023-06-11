using App_Gerenciamento.Models;
using App_Gerenciamento.ViewModel;
using Microsoft.Maui.Animations;
using System.Collections.Generic;

namespace App_Gerenciamento.Telas;

public partial class TelaMissoes : ContentPage
{
	RestService restService = new RestService();

    public TelaMissoes()
	{
        
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        BindingContext = new PageMissoesVM();
	}

    private void p_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (e.SelectedItem == null)
            return;

        Projetos objetoSelecionado = (Projetos)e.SelectedItem;
        string Nome = objetoSelecionado.Nome;
        List<Fases> fases = objetoSelecionado.fases;
        F.BindingContext = new { nome = Nome };
        Fases.BindingContext = new
        {
            fases = fases
        };

        F.IsVisible = true;
        bt.IsVisible = true;
        p_list.SelectedItem = null;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        F.IsVisible = false;
        bt.IsVisible = false;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if(e.NewTextValue != string.Empty)
            {
                p_list.ItemsSource = null;
                string searchTerm = e.NewTextValue.ToUpper(); // Obtenha o termo de pesquisa digitado na SearchBar

                List<Projetos> projetos = await restService.requestProjetos(); // Obtenha a lista completa de projetos

                // Filtrar os projetos com base no termo de pesquisa
                List<Projetos> resultados = projetos.Where(p => p.Nome.Contains(searchTerm)).ToList();
                resultados.ForEach(projeto =>
                {
                    Console.WriteLine(projeto.Nome);
                });
                // Atualizar a fonte de dados da ListView com os resultados da pesquisa
                p_list.ItemsSource = resultados;
            }
            else
            {
                List<Projetos> projetos = await restService.requestProjetos();
                p_list.ItemsSource = projetos;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if(BindingContext is PageMissoesVM viewmodel)
        {
            viewmodel.getProjetos.Execute(null);
        }
    }
}