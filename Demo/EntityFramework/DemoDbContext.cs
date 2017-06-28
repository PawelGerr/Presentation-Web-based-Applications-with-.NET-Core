using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Demo.EntityFramework
{
	public class DemoDbContext : DbContext
	{
		public DbSet<DemoRecord> Records { get; set; }

		public DemoDbContext(DbContextOptions<DemoDbContext> options)
			: base(options)
		{
		}
	}
}