using System;
using System.Text.RegularExpressions;
using RowShare.API;

namespace RowShare.MapEngine.Models
{
    public class PointModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}