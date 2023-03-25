using Microsoft.AspNetCore.Identity;
using Reservator.Data;
using Reservator.Models;

namespace Reservator.Services
{
    public class TestService : ITestService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public TestService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        
        
        public string Name { get; set; } = "Experiment";
    }

    public interface ITestService
    {
        string Name { get; set; }
    }
}
