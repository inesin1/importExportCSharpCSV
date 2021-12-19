using System;
using System.Collections.Generic;
using System.Text;

namespace testImportExport
{
    class Product
    {
        public Product(string ID, string ProductName, string Category, string Unit, string UnitPrice) {
            this.ID = ID;
            this.ProductName = ProductName;
            this.Category = Category;
            this.Unit = Unit;
            this.UnitPrice = UnitPrice;
        }
        public string ID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public string UnitPrice { get; set; }
    }
}
