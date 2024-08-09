using BelleVue_Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BelleVue_DataAccess.Seeds;


namespace BelleVue_DataAccess.Data
{
	public class BelleVueDbContext : IdentityDbContext
	{



		public BelleVueDbContext(DbContextOptions<BelleVueDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseInMemoryDatabase("scaffolding");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);

			//// Seed data using the HasData method
			//ModelBuilderDataGenerator.GenerateData(modelBuilder);
		}
	}
}
