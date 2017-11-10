using CodingDojo2.DataSim;
using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> modelItems = new List<ItemVm>();
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public ObservableCollection<string> ModeSelectionList { get; set; }

        private string curDate= System.DateTime.Now.ToLocalTime().ToShortDateString();
        
        public string CurDate
        {
            get { return curDate; }
            set { curDate = value; RaisePropertyChanged("CurDate"); }
        }

        private string curTime = System.DateTime.Now.ToLocalTime().ToShortTimeString();

        public string CurTime
        {
            get { return curTime; }
            set { curTime = value; RaisePropertyChanged("CurTime"); }
        }

        public MainViewModel()
        {
            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();
            ModeSelectionList = new ObservableCollection<string>();

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ModeSelectionList.Add(item);

            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new System.TimeSpan(0, 0, 3);
            timer.Tick += Timer_Tick;

            timer.Start();

            LoadData();
        }

        private void LoadData()
        {
            Simulator sim = new Simulator(modelItems);
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }

        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            CurDate = System.DateTime.Now.ToLocalTime().ToShortDateString();
            CurTime = System.DateTime.Now.ToLocalTime().ToShortTimeString();
    }
    }
}