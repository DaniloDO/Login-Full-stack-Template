using System;
using Microsoft.AspNetCore.Identity;

using LoginApp.Models; 

namespace LoginApp.Data;

public static class IdentityDataSeeder
{
    private static readonly string[] _roles = { "User", "Admin" };

    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in _roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role)); 
            }
        }

        //Create admin user if doesn't exists
        var adminName = Environment.GetEnvironmentVariable("ADMIN_NAME");
        var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var adminPassword =  Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin is null)
        {
            var adminUser = new User
            {
                UserName = adminName,
                Email = adminEmail,
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword); 
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else 
            {
                throw new Exception("Failed to create seeding admin user: " + string.Join(", ", result.Errors.Select(e => e.Description))); 
            }
        } 

    } 
}
