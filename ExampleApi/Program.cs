using ExampleApi.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<PostDataContext>(opt =>
{
    opt.UseSqlite("Data Source=local.sqlite");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PostDataContext>().Database.EnsureCreated();
}
app.UseRouting();
app.MapControllers();
app.Run();