namespace ShowroomCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
        public int Solid { get; set; }
        public string Image { get; set; }
        public string Trademark { get; set; }
        public int Status { get; set; }
    }
}
