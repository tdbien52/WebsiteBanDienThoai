namespace TranDinhBien_BanHang.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TranDinhBien_BanHang.Models.DBConn>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TranDinhBien_BanHang.Models.DBConn";
        }

        protected override void Seed(TranDinhBien_BanHang.Models.DBConn context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
