
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Forum.Persistence;

namespace Forum.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Forum.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }
    } 
}