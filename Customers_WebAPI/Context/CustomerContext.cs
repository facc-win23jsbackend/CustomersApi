using Customers_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers_WebAPI.Context;

public class CustomerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
}
