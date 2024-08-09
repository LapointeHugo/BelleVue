using BelleVue_Core.Models;
using BelleVue_DataAccess.Data;
using BelleVue_Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BelleVue_DataAccess.Seeds;

namespace BelleVue_DataAccess.Initializer
{
	public class DbInitialiser : IdbInitialiser
	{
		private readonly BelleVueDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitialiser(BelleVueDbContext db,
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager
			)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public void Initialize()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
					//ModelBuilderDataGenerator.GenerateData();
				}

			}
			catch (Exception ex) { }
			//Créer les rôles suivants si aucun rôle ne figure dans la bd
			if (!_roleManager.RoleExistsAsync(AppConstants.AdminBelleVueRole).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(AppConstants.AdminBelleVueRole))
					.GetAwaiter().GetResult();

				//_roleManager.CreateAsync(new IdentityRole(AppConstants.MedicinRole))
				//				.GetAwaiter().GetResult();


				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "admin@bellevue.com",
					NormalizedUserName = "ADMIN@BELLEVUE.COM",
					Email = "admin@bellevue.com",
					NormalizedEmail = "ADMIN@BELLEVUE.COM",
					PhoneNumber = "1234567890",
					EmailConfirmed = true
				}, "Admin123!").GetAwaiter().GetResult();
				ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "admin@bellevue.com");
				_userManager.AddToRoleAsync(user, AppConstants.SuperAdminRole)
					.GetAwaiter().GetResult();

			}
		}
	}
}
