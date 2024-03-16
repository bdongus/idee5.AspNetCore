using idee5.AspNetCore;
using idee5.Common;
using idee5.Common.Data;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

var assembly = Assembly.GetExecutingAssembly();
services.AddControllers().AddCQRSHandlers(assembly, assembly);
services.RegisterQueryHandlers();
services.RegisterCommandHandlers();
services.AddHttpContextAccessor();
services.AddSingleton<ICurrentUserIdProvider, HttpContextCurrentUserProvider>();
services.AddAuthorizationBuilder()
    .AddPolicy("CommandPolicy", p => p.RequireAssertion(_ => true))
    .AddPolicy("QueryPolicy", p => p.RequireAssertion(_ => true));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapRouteDump("/debug/routes");
app.UseAuthorization();

app.MapControllers();

app.Run();

/* Don't forget to add this in the csproj
<ItemGroup>
     <InternalsVisibleTo Include="MyTestProject" />
</ItemGroup>
 */
public partial class Program { }