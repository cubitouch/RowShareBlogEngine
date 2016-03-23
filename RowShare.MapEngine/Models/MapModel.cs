using System;
using System.Collections.Generic;
using RowShare.Api;
using System.Collections.ObjectModel;

namespace RowShare.MapEngine.Models
{
    public class MapModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PointModel> Points { get; set; }

        public MapModel()
        {
            Points = new List<PointModel>();
        }

        public void LoadMap(string id)
        {
            List list = List.GetListById(id);

            if (list != null)
            {
                Id = list.Id;
                Title = list.DisplayName;
                Description = list.Description;

                LoadPoints(id);
            }
        }
        public void LoadPoints(string id)
        {
            Collection<Row> rows = Row.GetRowsByListId(id);

            foreach (Row row in rows)
            {
                PointModel point = new PointModel();

                point.Id = row.Id;
                point.Title = row.Values["Title"].ToString();
                point.Latitude = decimal.Parse(row.Values["Latitude"].ToString());
                point.Longitude = decimal.Parse(row.Values["Longitude"].ToString());

                Points.Add(point);
            }
        }
    }
}