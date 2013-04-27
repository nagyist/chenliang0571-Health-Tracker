using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace Health_Tracker.Model
{
    public class HealthTrackerDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public HealthTrackerDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Items> items;
        public Table<Categories> categories;
        public Table<Maps> map;
        public Table<Tags> tags;
        public Table<Schedules> schedules;
        public Table<Tasks> tasks;
        public Table<Description> description;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Items
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }
        [Column]
        public int CategoryID
        {
            get;
            set;
        }
        [Column]
        public DateTime StartTime
        {
            get;
            set;
        }
        [Column]
        public DateTime EndTime
        {
            get;
            set;
        }
        [Column(Storage = "_Comment", CanBeNull = true)]
        public string Comment
        {
            get;
            set;
        }
        [Column]
        public bool IsActivity
        {
            get;
            set;
        }
        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Categories
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public string Name
        {
            get;
            set;
        }

        [Column]
        public string DisplayName
        {
            get;
            set;
        }

        [Column]
        public string DescriptionIds
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Tags
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public string Name
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }


    /// <summary>
    /// describe category
    /// </summary>
    [Table]
    public class Description
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public string Name
        {
            get;
            set;
        }

        [Column]
        public int DescriptionType
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Maps
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public int ItemId
        {
            get;
            set;
        }

        [Column]
        public int TagId
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Tasks
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public string Name
        {
            get;
            set;
        }

        [Column]
        public int ScheduleId
        {
            get;
            set;
        }

        [Column]
        public DateTime NextRunTime
        {
            get;
            set;
        }

        [Column]
        public DateTime LastRunTime
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

    /// <summary>
    /// 
    /// </summary>
    [Table]
    public class Schedules
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID
        {
            get;
            set;
        }

        [Column]
        public string RecurrencePattern
        {
            get;
            set;
        }

        [Column]
        public int Every
        {
            get;
            set;
        }

        [Column]
        public string On
        {
            get;
            set;
        }

        [Column]
        public string Action
        {
            get;
            set;
        }

        [Column]
        public bool IsActivity
        {
            get;
            set;
        }

        [Column]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
    }

}
