namespace ShowroomCar.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShowroomCarDbContext : DbContext
    {
        public ShowroomCarDbContext()
            : base("name=ChuoiKn")
        {
        }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> users { get; set; }


    }
}
