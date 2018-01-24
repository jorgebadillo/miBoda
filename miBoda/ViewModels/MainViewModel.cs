﻿



namespace miBoda.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using miBoda.Services;
    using Xamarin.Forms;
    using miBoda.Models;
    using miBoda.Helpers;
    public class MainViewModel
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        string _result;
        ObservableCollection<Rate> _rates;
        Rate _sourceRate;
        Rate _targetRate;
        List<Rate> rates;
        string _status;
        #endregion
        #region Properties
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        public string Amount
        {
            get;
            set;
        }

        public ObservableCollection<Rate> Rates
        {
            get
            {
                return _rates;
            }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Rates)));
                }
            }
        }

        public Rate SourceRate
        {
            get
            {
                return _sourceRate;
            }
            set
            {
                if (_sourceRate != value)
                {
                    _sourceRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SourceRate)));
                }
            }
        }

        public Rate TargetRate
        {
            get
            {
                return _targetRate;
            }
            set
            {
                if (_targetRate != value)
                {
                    _targetRate = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TargetRate)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Result)));
                }
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand ChangeCommand
        {
            get
            {
                return new RelayCommand(Change);
            }
        }

        void Change()
        {
            var aux = SourceRate;
            SourceRate = TargetRate;
            TargetRate = aux;
            Convert();
        }

        public ICommand ConvertCommmand
        {
            get
            {
                return new RelayCommand(Convert);
            }
        }

        async void Convert()
        {
            if (string.IsNullOrEmpty(Amount))
            {
                await dialogService.ShowMessage(
                    Lenguages.Hola,
                    Lenguages.Hola);
                return;
            }

            decimal amount = 0;
            if (!decimal.TryParse(Amount, out amount))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a numeric value in amount.");
                return;
            }

            if (SourceRate == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must select a source rate.");
                return;
            }

            if (TargetRate == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must select a target rate.");
                return;
            }

            var amountConverted = amount /
                                  (decimal)SourceRate.TaxRate *
                                  (decimal)TargetRate.TaxRate;

            Result = string.Format(
                "{0} ${1:N2} = {2} ${3:N2}",
                SourceRate.Code,
                amount,
                TargetRate.Code,
                amountConverted);
        }
        #endregion
    }
}
