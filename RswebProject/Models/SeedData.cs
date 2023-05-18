using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RswebProject.Areas.Identity.Data;
using RswebProject.Data;

namespace RswebProject.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<RswebProjectUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }

            RswebProjectUser user = await UserManager.FindByEmailAsync("admin@rswebproject.com");
            if (user == null)
            {
                var User = new RswebProjectUser();
                User.Email = "admin@rswebproject.com";
                User.UserName = "admin@rswebproject.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin      
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RswebProjectContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RswebProjectContext>>()))
            {

                // Look for any movies.
                if (context.Book.Any() || context.Author.Any() || context.PHouse.Any())
                {
                    return;   // DB has been seeded
                }

                context.Author.AddRange(
                    new Author { /*Id = 1, */FirstName = "Оливера", LastName = "Николова", BirthDate = DateTime.Parse("1936-1-3") },
                    new Author { /*Id = 2, */FirstName = "Славко", LastName = "Јаневски", BirthDate = DateTime.Parse("1920-3-1") },
                    new Author { /*Id = 3, */FirstName = "Ацо", LastName = "Шопов", BirthDate = DateTime.Parse("1923-2-1") }
                );
                context.SaveChanges();

                context.PHouse.AddRange(
                    new PHouse {/*Id = 1, */FirstName = "Детска Радост", LastName = "Скопје" },
                    new PHouse {/*Id = 2, */FirstName = "Култура", LastName = "Скопје" },
                    new PHouse {/*Id = 3, */FirstName = "Топер", LastName = "Скопје" },
                    new PHouse {/*Id = 4, */FirstName = "Блесок", LastName = "Скопје" },
                    new PHouse {/*Id = 5, */FirstName = "Збор", LastName = "Скопје" },
                    new PHouse {/*Id = 6, */FirstName = "Икона", LastName = "Скопје" },
                    new PHouse {/*Id = 7, */FirstName = "Матица", LastName = "Скопје" },
                    new PHouse {/*Id = 8, */FirstName = "Просветно Дело", LastName = "Скопје" }
                    );
                context.SaveChanges();

                context.Book.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Зоки Поки",
                        ReleaseDate = DateTime.Parse("1963-2-12"),
                        Genre = "Раскази",
                        Rating = 5,
                        Price = 7.99M,
                        AuthorId = context.Author.Single(d => d.FirstName == "Оливера" && d.LastName == "Николова").Id
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Тврдоглави",
                        ReleaseDate = DateTime.Parse("1969-2-8"),
                        Genre = "Роман",
                        Rating = 5,
                        Price = 5.99M,
                        AuthorId = context.Author.Single(d => d.FirstName == "Славко" && d.LastName == "Јаневски").Id
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Јус Универзум",
                        ReleaseDate = DateTime.Parse("1968-2-1"),
                        Genre = "Поезија",
                        Rating = 5,
                        Price = 5.99M,
                        AuthorId = context.Author.Single(d => d.FirstName == "Ацо" && d.LastName == "Шопов").Id
                    },
                    new Book
                    {
                        //Id = 4,
                        Title = "Петар Пан",
                        ReleaseDate = DateTime.Parse("2000-2-1"),
                        Genre = "Роман",
                        Rating = 5,
                        Price = 5.99M,
                        AuthorId = context.Author.Single(d => d.FirstName == "Ацо" && d.LastName == "Шопов").Id
                    },
                    new Book
                    {
                        //Id = 5,
                        Title = "Волшебникот од Оз",
                        ReleaseDate = DateTime.Parse("1998-2-1"),
                        Genre = "Лектира",
                        Rating = 5,
                        Price = 5.99M,
                        AuthorId = context.Author.Single(d => d.FirstName == "Славко" && d.LastName == "Јаневски").Id
                    }
                    );
                context.SaveChanges();

                context.BookPHouses.AddRange(
                    new BookPHouse { PHouseId = 1, BookId = 1 },
                    new BookPHouse { PHouseId = 2, BookId = 2 },
                    new BookPHouse { PHouseId = 3, BookId = 3 },
                    new BookPHouse { PHouseId = 4, BookId = 4 },
                    new BookPHouse { PHouseId = 5, BookId = 5 }

                    );
                context.SaveChanges();

            }
        }
    }

}
