using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AtvIndustria.Models;
using AtvIndustria.Services;

namespace AtvIndustria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }
        public PageCadastrar(ModelIndustria industria)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = industria.Id.ToString();
            txtNome.Text = industria.Nome.ToString();
            txtSetor.Text = industria.Setor.ToString();
            txtTurno.Text = industria.Turno.ToString();
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelIndustria industria = new ModelIndustria();
                industria.Nome = txtNome.Text;
                industria.Setor = txtSetor.Text;
                industria.Turno = txtTurno.Text;
                ServiceDBIndustria dBIndustria = new ServiceDBIndustria(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dBIndustria.Inserir(industria);
                    DisplayAlert("Resultado", dBIndustria.StatusMessage, "OK");
                }
                else
                {
                    industria.Id = Convert.ToInt32(txtCodigo.Text);
                    dBIndustria.Alterar(industria);
                    DisplayAlert("Dados alterados com sucesso!", dBIndustria.StatusMessage, "OK");
                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Funcionário", "Deseja excluir esse funcionário?", "Sim", "Não");
            if (resp = true)
            {
                ServiceDBIndustria dBIndustria = new ServiceDBIndustria(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dBIndustria.Excluir(id);
                DisplayAlert("Funcionário excluído com sucesso", dBIndustria.StatusMessage, "OK");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}