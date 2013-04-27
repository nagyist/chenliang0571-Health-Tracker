using System;
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
using Microsoft.Phone.Tasks;
using System.Xml.Linq;


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
                           select new { items.ID, items.CategoryID, categories.DisplayName, items.StartTime, items.EndTime };
            ItemBean ib = null;
            foreach (var item in varitems)
            {
                ib = new ItemBean()
                {
                    ID = item.ID,
                    CategoryId = item.CategoryID,
                    ItemName = item.DisplayName,
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

                //add to items
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
                this.CategoryItems.Add(new CategoryBean { ID = c.ID, CategoryName = c.Name, DisplayName = c.DisplayName });
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
                    ID = newItem.ID,
                    CategoryId = newItem.CategoryID,
                    ItemName = categoryBean.DisplayName,
                    StartTimestamp = newItem.StartTime,
                    EndTimestamp = newItem.EndTime,
                    StrTimestamp = newItem.StartTime.ToLongDateString()
                }
            );
        }

        public void DeleteItem(int itemId)
        {
            var item = from Items items in healthTrackerDB.items
                         where (items.ID == itemId)
                         select items;
            foreach (Items i in item)
            {
                healthTrackerDB.items.DeleteOnSubmit(i);
                healthTrackerDB.SubmitChanges();

                //update UI
                RemoveCurrentItem(itemId);
                break;
            }
        }

        private void RemoveCurrentItem(int itemId)
        {
            foreach (ItemBean i in this.CurrentItems)
            {
                if (itemId == i.ID)
                {
                    this.CurrentItems.Remove(i);
                    break;
                }
            }
        }


        public void AddCategory(Categories category)
        {
            healthTrackerDB.categories.InsertOnSubmit(category);
            healthTrackerDB.SubmitChanges();

            //update UI
            LoadCategories();
        }

        public void DeleteCategory(int categoryId)
        {
            var categoryQury = from Categories category in healthTrackerDB.categories
                               where (category.ID == categoryId)
                               select category;
            foreach (Categories c in categoryQury)
            {
                healthTrackerDB.categories.DeleteOnSubmit(c);
                healthTrackerDB.SubmitChanges();

                //update UI
                LoadCategories();
                break;
            }
        }

        public void UpdateCategory(Categories category)
        {
            healthTrackerDB.SubmitChanges();

            //update UI
            LoadCategories();
        }

        public void Export2XML()
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            DateTime now = DateTime.Now;
            emailComposeTask.Subject = "Health Tracker Backup @ " + now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
            emailComposeTask.Body = Appbar.GenerateXML(healthTrackerDB);
            emailComposeTask.Show();

            //MessageBox.Show(GenerateXML());
        }

    }
}