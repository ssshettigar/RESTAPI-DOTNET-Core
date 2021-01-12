using System;
using Microsoft.EntityFrameworkCore;
using RestAPI_DOTNET_Core.Models;

namespace RestAPI_DOTNET_Core.Data
{
    public class QuoteDbContext:DbContext 
    {
        //public QuoteContext()
        //{
        //}
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options):base(options)
        {

        }
        public DbSet<Quote> Quotes { get; set; }
    }
}
