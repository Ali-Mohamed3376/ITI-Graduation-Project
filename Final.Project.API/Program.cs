using Final.Project.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region Default Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region My Services


builder.Services.AddDbContext<ECommerceContext>(options => options
    .UseSqlServer(@"Server=DESKTOP-35F9698\SQLEXPRESS;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));


#endregion



var app = builder.Build();


#region Middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion


app.Run();
