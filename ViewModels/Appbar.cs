using Health_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Health_Tracker
{
    public static class Appbar
    {
        public static string GenerateXML(HealthTrackerDataContext healthTrackerDB)
        {
            string xml = "<HealthTracker>";

            //Tags Table
            var q_tag = from Tags tags in healthTrackerDB.tags
                        select tags;
            XDocument xdoc_tag = new XDocument(new XElement("Tags", from tags in q_tag
                                                                    select (
                                                                    new XElement("Tag",
                                                                        new XAttribute("ID", tags.ID),
                                                                        new XAttribute("Name", tags.Name),
                                                                        new XAttribute("IsActivity", tags.IsActivity),
                                                                        new XAttribute("UpdateTime", tags.UpdateTime)))));
            xml += xdoc_tag.ToString();

            //Categories Table
            var q_category = from Categories categories in healthTrackerDB.categories
                             select categories;
            XDocument xdoc_category = new XDocument(new XElement("Categories", from categories in q_category
                                                                               select (
                                                                               new XElement("Category",
                                                                                   new XAttribute("ID", categories.ID),
                                                                                   new XAttribute("Name", categories.Name),
                                                                                   new XAttribute("DescriptionIds", categories.DescriptionIds),
                                                                                   new XAttribute("DisplayName", categories.DisplayName),
                                                                                   new XAttribute("IsActivity", categories.IsActivity),
                                                                                   new XAttribute("UpdateTime", categories.UpdateTime)))));
            xml += xdoc_category.ToString();

            //Description table
            var q_description = from Description desc in healthTrackerDB.description
                                select desc;
            XDocument xdoc_desc = new XDocument(new XElement("Descriptions", from desc in q_description
                                                                             select (
                                                                             new XElement("Description",
                                                                                 new XAttribute("ID", desc.ID),
                                                                                 new XAttribute("Name", desc.Name),
                                                                                 new XAttribute("IsActivity", desc.IsActivity),
                                                                                 new XAttribute("DescriptionType", desc.DescriptionType),
                                                                                 new XAttribute("UpdateTime", desc.UpdateTime)))));
            xml += xdoc_desc.ToString();

            //maps table
            var q_maps = from Maps maps in healthTrackerDB.map
                        select maps;
            XDocument xdoc_map = new XDocument(new XElement("Maps", from map in q_maps
                                                                select (
                                                                new XElement("Map",
                                                                    new XAttribute("ID", map.ID),
                                                                    new XAttribute("ItemId", map.ItemId),
                                                                    new XAttribute("TagId", map.TagId),
                                                                    new XAttribute("IsActivity", map.IsActivity),
                                                                    new XAttribute("UpdateTime", map.UpdateTime)))));
            xml += xdoc_map.ToString();

            //item Table

            var q_items = from Items items in healthTrackerDB.items
                         select items;
            XDocument xdoc_item = new XDocument(new XElement("Items", from item in q_items
                                                                      select (
                                                                      new XElement("Item",
                                                                          new XAttribute("ID", item.ID),
                                                                          new XAttribute("CategoryID", item.CategoryID),
                                                                          new XAttribute("StartTime", item.StartTime),
                                                                          new XAttribute("EndTime", item.EndTime),
                                                                          new XAttribute("IsActivity", item.IsActivity),
                                                                          new XAttribute("Comment", item.Comment == null ? "" : item.Comment),
                                                                          new XAttribute("UpdateTime", item.UpdateTime)))));
            xml += xdoc_item.ToString();

            //Schedules Table
            var q_schedules = from Schedules schedules in healthTrackerDB.schedules
                         select schedules;
            XDocument xdoc_schedule = new XDocument(new XElement("Schedules", from schedule in q_schedules
                                                                      select (
                                                                      new XElement("Schedule",
                                                                          new XAttribute("ID", schedule.ID),
                                                                          new XAttribute("RecurrencePattern", schedule.RecurrencePattern),
                                                                          new XAttribute("Every", schedule.Every),
                                                                          new XAttribute("On", schedule.On),
                                                                          new XAttribute("Action", schedule.Action),
                                                                          new XAttribute("IsActivity", schedule.IsActivity),
                                                                          new XAttribute("UpdateTime", schedule.UpdateTime)))));
            xml += xdoc_schedule.ToString();

            //Task table
            var q_task = from Tasks tasks in healthTrackerDB.tasks
                         select tasks;
            XDocument xdoc_task = new XDocument(new XElement("Tasks", from task in q_task
                                                                      select (
                                                                      new XElement("Task",
                                                                          new XAttribute("ID", task.ID),
                                                                          new XAttribute("Name", task.Name),
                                                                          new XAttribute("ScheduleId", task.ScheduleId),
                                                                          new XAttribute("NextRunTime", task.NextRunTime),
                                                                          new XAttribute("LastRunTime", task.LastRunTime),
                                                                          new XAttribute("IsActivity", task.IsActivity),
                                                                          new XAttribute("UpdateTime", task.UpdateTime)))));
            xml += xdoc_task;
            return xml + "<HealthTracker/>";
        }
    }
}
