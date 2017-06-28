using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Demo.EntityFramework
{
	internal class EfMigrationDbContextFactory : IDbContextFactory<DemoDbContext>
	{
		public DemoDbContext Create(DbContextFactoryOptions options)
		{
			var builder = new DbContextOptionsBuilder<DemoDbContext>();
			builder.UseSqlServer("server=(local);database=Demo;integrated security=true;");

			return new DemoDbContext(builder.Options);
		}
	}
}
