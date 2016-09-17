using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LiveRC.Common
{
    public class RaceClock : INotifyPropertyChanged
    {
        long _elapsedTime;
        long _remainingTime;
        long _length;

        TimeSpan _raceTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public TimeSpan RaceTime
        {
            get { return _raceTime; }
            set
            {

                if (_raceTime != value)
                {
                    _raceTime = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("RaceTime"));
                    }
                }
            }
        }

        public long ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {

                if (_elapsedTime != value)
                {
                    _elapsedTime = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("ElapsedTime"));
                    }
                }
            }
        }
        public long Length
        {
            get { return _length; }
            set
            {

                if (_length != value)
                {
                    _length = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Length"));
                    }
                }
            }
        }

        public long RemainingTime
        {
            get { return _remainingTime; }
            set
            {

                if (_remainingTime != value)
                {
                    _remainingTime = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("RemainingTime"));
                    }
                }
            }
        }
    }

    public class RaceData : INotifyPropertyChanged
    {
        
        long _Tick;
        string _RaceTitle;
        int _RaceNumber;
        int _RoundNumber;
        string _Status;
        string _FastestLapDriverName;
        int _FastestLapOnLap;
        TimeSpan _FastestLapTime;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string FastestLapDriverName
        {
            get { return _FastestLapDriverName; }
            set
            {

                if (_FastestLapDriverName != value)
                {
                    _FastestLapDriverName = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("FastestLapDriverName"));
                    }
                }
            }
        }

        public int FastestLapOnLap
        {
            get { return _FastestLapOnLap; }
            set
            {

                if (_FastestLapOnLap != value)
                {
                    _FastestLapOnLap = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("FastestLapOnLap"));
                    }
                }
            }
        }


        public TimeSpan FastestLapTime
        {
            get { return _FastestLapTime; }
            set
            {

                if (_FastestLapTime != value)
                {
                    _FastestLapTime = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("FastestLapTime"));
                    }
                }
            }
        }


        public long Tick
        {
            get { return _Tick; }
            set
            {

                if (_Tick != value)
                {
                    _Tick = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Tick"));
                    }
                }
            }
        }
        
        public string RaceTitle
        {
            get { return _RaceTitle; }
            set {

                if (_RaceTitle != value)
                {
                    _RaceTitle = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("RaceTitle"));
                    }
                }
            }
        }
        public int RaceNumber
        {
            get { return _RaceNumber; }
            set
            {

                if (_RaceNumber != value)
                {
                    _RaceNumber = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("RaceNumber"));
                    }
                }
            }
        }
        public int RoundNumber
        {
            get { return _RoundNumber; }
            set
            {

                if (_RoundNumber != value)
                {
                    _RoundNumber = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("RoundNumber"));
                    }
                }
            }
        }
        public string Status
        {
            get { return _Status; }
            set
            {

                if (_Status != value)
                {
                    _Status = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Status"));
                    }
                }
            }
        }

    }


    public class DriverData : INotifyPropertyChanged
    {
        private long _tick;
        private int _position;
        private int _number;
        private int _laps;
        private string _time;
        private string _difference;
        private string _name;
        private string _fastest;
        private string _average;

        public event PropertyChangedEventHandler PropertyChanged;
        public long Tick
        {
            get { return _tick; }
            set
            {

                if (_tick != value)
                {
                    _tick = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Tick"));
                    }
                }
            }
        }
        public int Laps
        {
            get { return _laps; }
            set
            {

                if (_laps != value)
                {
                    _laps = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Laps"));
                    }
                }
            }
        }
        public int Position
        {
            get { return _position; }
            set
            {

                if (_position != value)
                {
                    _position = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Position"));
                    }
                }
            }
        }

        public int Number
        {
            get { return _number; }
            set
            {

                if (_number != value)
                {
                    _number = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Number"));
                    }
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {

                if (_name != value)
                {
                    _name = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }
        public string Time
        {
            get { return _time; }
            set
            {

                if (_time != value)
                {
                    _time = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Time"));
                    }
                }
            }
        }

        public string Difference
        {
            get { return _difference; }
            set
            {

                if (_difference != value)
                {
                    _difference = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Difference"));
                    }
                }
            }
        }

        public string Fastest
        {
            get { return _fastest; }
            set
            {

                if (_fastest != value)
                {
                    _fastest = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Fastest"));
                    }
                }
            }
        }
        public string Average
        {
            get { return _average; }
            set
            {

                if (_average != value)
                {
                    _average = value;

                    PropertyChangedEventHandler temp = PropertyChanged;
                    if (temp != null)
                    {
                        temp(this, new PropertyChangedEventArgs("Average"));
                    }
                }
            }
        }

    }

}
