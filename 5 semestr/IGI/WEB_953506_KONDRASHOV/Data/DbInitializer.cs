using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRASHOV.Data
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if(!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                await roleManager.CreateAsync(roleAdmin);
            }

            if(!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");

                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");

                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            if(!context.EngineClasses.Any())
            {
                context.EngineClasses.AddRange(
                    new List<EngineClass>
                    {
                        new EngineClass{Name = "Японцы"},
                        new EngineClass{Name = "Немцы"},
                        new EngineClass{Name = "Американцы"}
                    });
                await context.SaveChangesAsync();
            }

            if(!context.Engines.Any())
            {
                context.Engines.AddRange(
                    new List<Engine>
                    {
                        new Engine{EngineName = "2JZ-GTE", ClassId = 1,
                                   Description = "Надежный, дешевый при покупке, ЯПОНЦЫ ВЕЩИ ДЕЛАЮТ", Price = 1200, Image = "2JZ-GTE.jpg"},
                        new Engine{EngineName = "1UZ-FE", ClassId = 1,
                                   Description = "Надежный, дешевый в покупке и обслуживании, ЯПОНЦЫ ВЕЩИ ДЕЛАЮТ", Price = 900, Image = "1UZ-FE.jpg"},
                        new Engine{EngineName = "CHHB", ClassId = 2,
                                   Description = "Достаточно ресурсный, валит как черт, Stage 1 без потери ресурса, небольшой расход", Price = 2000, Image = "CHHB.jpg"},
                        new Engine{EngineName = "CRDB", ClassId = 2,
                                   Description = "Огроменный ресурс, валит как сатана, терпит спокойно Stage 3 и 1000 л.с., лучший генератор хорошего настроения", Price = 3400, Image = "CRDB.jpg"},
                        new Engine{EngineName = "L86", ClassId = 3,
                                   Description = "Надежный, древний и поэтому никуда не едет, придется купить свою заправку или хотя бы бензовоз", Price = 1700, Image = "L86.jpg"},
                        new Engine{EngineName = "F18D4", ClassId = 3,
                                   Description = "Стандартный мотор от шевроле, с определенными проблемами, но в комбинации с правильной коробкой пройдет 300 тыс.км.",
                                   Price = 700, Image = "F18D4.jpg"}
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
