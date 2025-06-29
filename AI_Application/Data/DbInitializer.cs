using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System; // Cần thiết cho TimeSpan
using Microsoft.Extensions.DependencyInjection; // Cần thiết cho CreateScope, GetRequiredService

namespace AI_Application.Data // CHÚ Ý: Đảm bảo namespace này chính xác!
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // 1. Tạo vai trò "Admin" nếu chưa tồn tại
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    Console.WriteLine("Role 'Admin' created.");
                }

                // 2. Tạo người dùng Admin nếu chưa tồn tại
                string adminEmail = "admin@example.com"; // EMAIL ADMIN CỦA BẠN
                string adminPassword = "Admin@123"; // MẬT KHẨU ADMIN CỦA BẠN (HÃY THAY ĐỔI MẬT KHẨU MẠNH HƠN!)

                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);

                    if (createAdmin.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        Console.WriteLine($"Admin user '{adminEmail}' created and assigned 'Admin' role successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error creating admin user:");
                        foreach (var error in createAdmin.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Admin user '{adminEmail}' already exists.");
                    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        Console.WriteLine($"Assigned 'Admin' role to existing user '{adminEmail}'.");
                    }
                }
            }
        }
    }
}