﻿namespace ProvaPub.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
