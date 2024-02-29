using AtvIndustria.Models;
using AtvIndustria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AtvIndustria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            AtualizarLista();
        }
        public void AtualizarLista()
        {
            String nome = "";
            if (txtNome.Text != null) nome = txtNome.Text;
            ServiceDBIndustria dBIndustria = new ServiceDBIndustria(App.DbPath);
            ListaFuncionarios.ItemsSource = dBIndustria.Localizar(nome);
        }

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void ListaFuncionarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModelIndustria industria = (ModelIndustria)ListaFuncionarios.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(industria));
        }
    }
}