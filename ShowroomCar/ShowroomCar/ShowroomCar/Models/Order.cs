namespace ShowroomCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public int CarID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public int Type { get; set; }
        public int? Status { get; set; }
    }
}
