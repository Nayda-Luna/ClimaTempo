using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaTempo.ViewModels
{
    public partial class PrevisaoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cidade;

        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private string condicao;

        [ObservableProperty]
        private string data;

        [ObservableProperty]
        private double max;

        [ObservableProperty]
        private double min;

        [ObservableProperty]
        private string indiceuv;

        [ObservableProperty]
        private DateTime atualizado_em;

        [ObservableProperty]
        private string condicao_desc;

        [ObservableProperty]
        private List<Clima> proximosDias;



        private Previsao previsao;

        private Previsao proxPrevisao;



        public ICommand BuscarPrevisaoCommand { get; }
        public ICommand BuscarCidadesCommand { get; }
        public PrevisaoViewModel()
        {
            BuscarPrevisaoCommand = new Command<int>(BuscarPrevisao);
            BuscarCidadesCommand = new Command(BuscarCidades);
            IsPrevisaoVisible = false;

        }
        public async void BuscarPrevisao(int Id)
        {

            previsao = await new PrevisaoService().GetPrevisaoById(Id);
            proxPrevisao = await new PrevisaoService().GetPrevisaoById(244,3);
             
           // Cidade = previsao.Cidade;
           // Estado = previsao.Estado;
            Max = previsao.clima[0].Max.ToString();
            Min = previsao.clima[0].Min.ToString();
            IsPrevisaoVisible = true;

           // ProximosDias = proxPrevisao.clima;

        }
        public async void BuscarCidades()
        {
            IsPrevisaoVisible = false;

            Cidade_list = new List<Cidade>();
            Cidade_list = await new CidadeService().GetCidadesByName(Cidade_pesquisada);

        }
    }
}