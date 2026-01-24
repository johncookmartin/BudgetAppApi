using AuthLibrary;
using Microsoft.AspNetCore.Builder;

namespace BudgetApp.Application;

public static class InitializeExtensions
{
    public static async Task ServiceInitialization(this WebApplication app)
    {
        // Initialization code goes here
        await app.AuthInitialization();
    }
}
