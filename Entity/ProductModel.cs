﻿namespace MatysProjekt.Entity
{
    public class ProductModel
    {
        public int Id { get; set; } 
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string PathToImage { get; set; }
    }
}
