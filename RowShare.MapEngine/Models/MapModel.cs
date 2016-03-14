using System;
using System.Collections.Generic;
using RowShare.API;

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
            Table table = Table.GetTableById(id);

            if (table != null)
            {
                Id = table.Id;
                Title = table.DisplayName;
                Description = table.Description;

                LoadPoints(id);
            }
        }
        public void LoadPoints(string id)
        {
            List<Row> rows = Row.GetRowsByTableId(id);

            foreach (Row row in rows)
            {
                PointModel point = new PointModel();

                point.Id = row.Id;
                point.Title = row.Values["Title"];
                point.Latitude = decimal.Parse(row.Values["Latitude"]);
                point.Longitude = decimal.Parse(row.Values["Longitude"]);

                Points.Add(point);
            }
        }
    }
}