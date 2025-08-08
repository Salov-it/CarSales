using CarSales.Client.Commands;
using CarSales.Client.Models;
using CarSales.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarSales.Client.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiClient _apiClient;

        public MainViewModel()
        {
            _apiClient = new ApiClient();
            Reports = new ObservableCollection<MonthlySalesReportDto>();

            LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync());
            ExportExcelCommand = new RelayCommand(async _ => await ExportExcelAsync(), _ => Reports.Any());
        }

        // Вводимые пользователем значения
        private string _inputYear = DateTime.Now.Year.ToString();
        public string InputYear
        {
            get => _inputYear;
            set { _inputYear = value; OnPropertyChanged(); }
        }

        private string _inputModel;
        public string InputModel
        {
            get => _inputModel;
            set { _inputModel = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MonthlySalesReportDto> Reports { get; }

        public ICommand LoadDataCommand { get; }
        public ICommand ExportExcelCommand { get; }

        private async Task LoadDataAsync()
        {
            if (!int.TryParse(InputYear, out var year))
            {
                // Можно добавить MessageBox, если нужно
                return;
            }

            var data = await _apiClient.GetMonthlySalesAsync(year, InputModel);

            Reports.Clear();
            foreach (var item in data)
                Reports.Add(item);
        }

        private async Task ExportExcelAsync()
        {
            if (!int.TryParse(InputYear, out var year))
                return;

            var excelBytes = await _apiClient.GetMonthlySalesExcelAsync(year, InputModel);
            FileService.SaveFile(excelBytes, $"MonthlySales_{year}.xlsx");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
