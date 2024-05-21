﻿using Cars.Models;

namespace Cars.ViewModels
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
