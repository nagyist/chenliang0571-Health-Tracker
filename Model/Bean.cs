using Health_Tracker.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Health_Tracker.Model
{
    public class ItemBean : INotifyPropertyChanged
    {
        public int ID
        {
            get;
            set;
        }

        private int _categoryId;
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (value != _categoryId)
                {
                    _categoryId = value;
                    NotifyPropertyChanged("CategoryId");
                }
            }
        }

        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (value != _itemName)
                {
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        public DateTime StartTimestamp
        {
            get;
            set;
        }

        public DateTime EndTimestamp
        {
            get;
            set;
        }
        private string _strTimestamp;
        public string StrTimestamp
        {
            get
            {
                return _strTimestamp;
            }
            set
            {
                if (value != _strTimestamp)
                {
                    _strTimestamp = value;
                    NotifyPropertyChanged("StrTimestamp");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class CategoryBean : INotifyPropertyChanged
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("CategoryId");
                }
            }
        }

        private string _name;
        public string CategoryName
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("CategoryName");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}