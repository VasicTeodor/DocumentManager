using System;

namespace DocumentManager.Domain.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Writers { get; set; }
        public string Genre { get; set; }
        public long Isbn { get; set; }
        public string KeyWords { get; set; }
        public string Publisher { get; set; }
        public DateTime Year { get; set; }
        public string Place { get; set; }
        public string Synopsis { get; set; }
        public int PageNumber { get; set; }
    }
}