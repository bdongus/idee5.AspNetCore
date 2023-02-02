using idee5.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

var assembly = Assembly.GetExecutingAssembly();
services.AddControllers().AddCQRSHandlers(assembly, assembly);
services.RegisterQueryHandlers();
services.RegisterCommandHandlers();
services.AddAuthorization(o => {
    o.AddPolicy("CommandPolicy", p => p.RequireAssertion(_ => true));
    o.AddPolicy("QueryPolicy", p => p.RequireAssertion(_ => true));
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

/* Don't forget to add this in the csproj
<ItemGroup>
     <InternalsVisibleTo Include="MyTestProject" />
</ItemGroup>
 */
public partial class Program { }