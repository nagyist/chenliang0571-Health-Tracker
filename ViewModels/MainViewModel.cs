﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Health_Tracker.Model;
using System.Linq;


namespace Health_Tracker
{
    public class MainViewModel : INotifyPropertyChanged
    {
         // LINQ to SQL data context for the local database.
        private HealthTrackerDataContext healthTrackerDB;

        // Class constructor, create the data context object.
        public MainViewModel(string dbConnectionString)
        {
            healthTrackerDB = new HealthTrackerDataContext(dbConnectionString);
            this.CurrentItems = new ObservableCollection<ItemBean>();
            this.PreviousItems = new ObservableCollection<ItemBean>();
            this.CategoryItems = new ObservableCollection<CategoryBean>();
        }

        public MainViewModel()
        {}

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemBean> CurrentItems { get; private set; }
        public ObservableCollection<ItemBean> PreviousItems { get; private set; }
        public ObservableCollection<CategoryBean> CategoryItems { get; private set; }

        //private string _sampleProperty = "Sample Runtime Property Value";
        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding
        ///// </summary>
        ///// <returns></returns>
        //public string SampleProperty
        //{
        //    get
        //    {
        //        return _sampleProperty;
        //    }
        //    set
        //    {
        //        if (value != _sampleProperty)
        //        {
        //            _sampleProperty = value;
        //            NotifyPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void LoadCollectionsFromDatabase()
        {
            //Load items
            LoadItems();

            //Load Categories
            LoadCategories();
        }

        private void LoadItems()
        {
            this.CurrentItems.Clear();
            this.PreviousItems.Clear();
            DateTime now = DateTime.Now;
            DateTime current = new DateTime(now.Year, now.Month, now.Day);

            var varitems = from Items items in healthTrackerDB.items
                           join Categories categories in healthTrackerDB.categories on items.CategoryID equals categories.ID
                           where (items.IsActivity == true)
                           select new { items.ID, items.CategoryID, categories.Name, items.StartTime, items.EndTime };
            ItemBean ib = null;
            foreach (var item in varitems)
            {
                ib = new ItemBean()
                {
                    ID = item.ID,
                    CategoryId = item.CategoryID,
                    ItemName = item.Name,
                    StartTimestamp = item.StartTime,
                    EndTimestamp = item.EndTime
                };
                if (item.StartTime == item.EndTime)
                {
                    ib.StrTimestamp = item.StartTime + "";
                }
                else
                {
                    ib.StrTimestamp = item.StartTime + " - " + item.EndTime;
                }
                if (item.EndTime > current || item == null)
                {
                    this.CurrentItems.Add(ib);
                }
                else
                {
                    this.PreviousItems.Add(ib);
                }
            }
        }

        private void LoadCategories()
        {
            this.CategoryItems.Clear();
            var cates = from Categories categories in healthTrackerDB.categories
                        where (categories.IsActivity == true)
                        select categories;
            foreach (var c in cates)
            {
                this.CategoryItems.Add(new CategoryBean { ID = c.ID, CategoryName = c.Name });
            }
        }

        public void AddItem(Items newItem, CategoryBean categoryBean)
        {
            healthTrackerDB.items.InsertOnSubmit(newItem);
            healthTrackerDB.SubmitChanges();

            //update UI
            this.CurrentItems.Add(
                new ItemBean 
                {
                    ItemName = categoryBean.CategoryName, 
                    StartTimestamp = newItem.StartTime, 
                    EndTimestamp = newItem.EndTime, 
                    StrTimestamp = newItem.StartTime.ToLongDateString() 
                }
            );
        }

        public void AddCategory(Categories category)
        {
            healthTrackerDB.categories.InsertOnSubmit(category);
            healthTrackerDB.SubmitChanges();

            //update UI
            //this.CategoryItems.Add(
            //    new CategoryBean
            //    {
            //        CategoryName = category.Name
            //    }
            //);
            LoadCategories();
        }
    }
}