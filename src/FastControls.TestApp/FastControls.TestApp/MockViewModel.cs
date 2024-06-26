﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using FastGrid.FastGrid;

namespace TestRadComboBox
{
    public enum SimpleEnum { Me, You, He, She }

    public class DummyData : INotifyPropertyChanged{
        private int operatorRecordId;
        private string operatorReportLabel = "";
        private string password = "";
        private string username = "";
        private string department;
        private string city;
        private int vehicleId;
        private DateTime time_ = DateTime.Now;
        private IReadOnlyList<DummyData> _children = new List<DummyData>();
        private bool _editableBool;
        private SimpleEnum _enumy = SimpleEnum.Me;
        private DateTime _date = DateTime.Now;

        public SimpleEnum Enumy {
            get => _enumy;
            set {
                if (value == _enumy) {
                    return;
                }

                _enumy = value;
                OnPropertyChanged();
            }
        }

        // allow tree-like structure
        public IReadOnlyList<DummyData> Children {
            get => _children;
            set {
                if (Equals(value, _children)) return;
                _children = value;
                OnPropertyChanged();
            }
        }

        public string OperatorReportLabel {
            get => operatorReportLabel;
            set {
                if (value == operatorReportLabel) return;
                operatorReportLabel = value;
                OnPropertyChanged();
            }
        }

        public int OperatorRecordId {
            get => operatorRecordId;
            set {
                if (value == operatorRecordId) return;
                operatorRecordId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BgColor));
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public string Username
        {
            get => username;
            set
            {
                if (value == username) return;
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (value == password) return;
                password = value;
                OnPropertyChanged();
            }
        }

        public string Department
        {
            get => department;
            set
            {
                if (value == department) return;
                department = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => city;
            set
            {
                if (value == city) return;
                city = value;
                OnPropertyChanged();
            }
        }

        public DateTime Time {
            get => time_;
            set {
                if (value.Equals(time_)) return;
                time_ = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date {
            get => _date;
            set {
                if (value.Equals(_date)) {
                    return;
                }

                _date = value;
                OnPropertyChanged();
            }
        }

        public string TimeString => Time.ToString("HH:mm");

        public Brush BgColor {
            get {
                switch (OperatorRecordId % 4) {
                case 0: return BrushCache.Inst.GetByColor(Colors.CornflowerBlue);
                case 1: return BrushCache.Inst.GetByColor(Colors.DodgerBlue);
                case 2: return BrushCache.Inst.GetByColor(Colors.LightSeaGreen);
                case 3: return BrushCache.Inst.GetByColor(Colors.DarkSlateBlue);
                default:
                    Debug.Assert(false);
                    return BrushCache.Inst.GetByColor(Colors.DarkSlateBlue);
                }
            }
        }

        public int VehicleId {
            get => vehicleId;
            set {
                if (value == vehicleId) return;
                vehicleId = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive => (OperatorRecordId % 10) == 0;

        public bool EditableBool {
            get => _editableBool;
            set {
                if (value == _editableBool) {
                    return;
                }

                _editableBool = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }


    public class MockViewModel
    {

        public MockViewModel() {
        }

        public IEnumerable<DummyData> GetDummyByCount(int count, string prefix = "", int offset = 0) {
            var time = DateTime.Now;
            var date = DateTime.Now;
            var incrementTimeSecs = 60;
            for (int i = offset; i < count + offset; ++i) {
                yield return new DummyData {
                    OperatorReportLabel = $"Operator {prefix} {i}" , 
                    OperatorRecordId = i,
                    VehicleId = i ,
                    Username = $"User {prefix} {i}",
                    Password = $"Pass {prefix} {i}",
                    Department = $"Dep {prefix} {i}",
                    City = $"City {prefix} {i}",
                    Time = time, 
                    Date = date,
                    EditableBool = (i % 2) == 0,
                };
                time = time.AddSeconds(incrementTimeSecs);
                date = date.AddDays(-1);
            }

        }

        public IEnumerable<DummyData> GetDummyByCountForTestingFilter(int count, int offset = 0) {
            var time = DateTime.Now;
            var incrementTimeSecs = 10;
            for (int i = offset; i < count + offset; ++i) {
                yield return new DummyData {
                    OperatorReportLabel = $"Operator {i % 250}" , 
                    OperatorRecordId = i % 97,
                    VehicleId = i ,
                    Username = $"User {i % 29}",
                    Password = $"Pass {i % 37}",
                    Department = $"Dep {i % 61}",
                    City = $"City {i % 23}",
                    Time = time,
                };
                time = time.AddSeconds(incrementTimeSecs);
            }


        }

    }



}
