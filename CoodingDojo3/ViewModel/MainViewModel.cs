using CodingDojo2.DataSimulation;
using CodingDojo2.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CoodingDojo3.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> ModellItem = new List<ItemVm>();
        private ObservableCollection<ItemVm> SensorList { get; set; }
        private ObservableCollection<ItemVm> ActorList { get; set; }

        public RelayCommand SensorAddBtnClickCmd { get; set; }
        public RelayCommand SensorDelBttnCmd { get; set; }
        public RelayCommand ActuatorAddBttnClickCmd { get; set; }
        public RelayCommand ActuatorDelBtnClickCmd { get; set; }
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();

        public ObservableCollection<string> ModeSelectionList { get; private set; }

        public string CurrentTime {
            get { return CurrentTime; }
            set { CurrentTime = value; RaisePropertyChanged(); }
        }

        public string CurrentDate
        {
            get { return CurrentDate; }
            set { CurrentDate= value; RaisePropertyChanged(); }
        }
        public MainViewModel()
        {
            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();
            ModeSelectionList = new ObservableCollection<string>();

            //Fill ModeSelectionList
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ModeSelectionList.Add(item);

            }

            //for time /date update
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 24);
            timer.Tick += UpdateTime;

            if (!IsInDesignMode)
            {
                //load Data
                LoadData();

                //start timer for date/time update
                timer.Start();
            }

        }

        private void LoadData()
        {
            Simulator sim = new Simulator(ModellItem);
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }

        }



        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
    }
}
    
