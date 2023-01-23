using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using dbase;
using dbase.Repository;
namespace Tests;

public class dbTests
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _contextOptionsBuilder;

        public dbTests()
        {
            
            var configuration = 
                new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..")))
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(configuration["DefaultConnection"]);
            _contextOptionsBuilder = optionsBuilder;
        }

        [Fact]
        public void UserCreation()
        {
            var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var UserRepository = new UserRepository(context);
            UserRepository.Create(new Domain.Models.User(0, "123", "fullname", "name", "password"));
            context.SaveChanges();
            Assert.True(context.Users.Any(u => u.UserName == "name"));
        }

        [Fact]
        public void DatabaseAdd()
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            context.Users.Add(new Database.Models.User()
            {
                UserName = "asdfg"
            });
            context.SaveChanges();
            Assert.True(context.Users.Any(u => u.UserName == "asdfg"));
        }

        [Fact]
        public void GetFirstElementFromDB()
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var user = context.Users.FirstOrDefault(u => u.UserName == "name");
            context.Users.Remove(user!);
            context.SaveChanges();
            Assert.True(!context.Users.Any(u => u.UserName == "name"));
        }

        [Fact]
        public void GetByLogin()
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var userRepository = new UserRepository(context);
            var userService = new UserInteractor(userRepository);

            var res = userService.GetUserByLogin("asdfghj");

            Assert.NotNull(res.Value);
        }
}