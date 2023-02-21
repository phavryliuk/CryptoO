using CryptoO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AlphaVantage.Net.Crypto;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using CryptoDataPoint = AlphaVantage.Net.Crypto.CryptoDataPoint;

namespace CryptoO.ViewModels
{
    public class CryptoDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly CryptoDataModel _model;
        private List<Models.CryptoDataPoint> _dataPoints;

        public List<Models.CryptoDataPoint> DataPoints
        {
            get { return _dataPoints; }
            set
            {
                _dataPoints = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataPoints"));
            }
        }

        public ICommand LoadDataCommand { get; private set; }

        public CryptoDataViewModel()
        {
            _model = new CryptoDataModel();
            LoadDataCommand = new RelayCommand(async () => await LoadData());
        }

        private async Task LoadData()
        {
            DataPoints = await _model.GetCryptoData("bitcoin", "usd", 14);
        }
    }

}
