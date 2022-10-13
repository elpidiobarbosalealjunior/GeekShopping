using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Api.Model.Context;

public class MySQLContext : DbContext
{
	public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
	{

	}

	public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
