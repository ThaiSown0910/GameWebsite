using Microsoft.EntityFrameworkCore;
using Minio;
using StackExchange.Redis;
using WebAPIDemo1.Data;
using WebAPIDemo1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameDataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("GameDb")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // Địa chỉ frontend Angular
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});


// Lấy ConnectionString từ cấu hình và tạo kết nối Redis
var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");
var redis = ConnectionMultiplexer.Connect(redisConnectionString);

// Đăng ký dịch vụ Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();


// Add services to the container.
builder.Services.AddSingleton<MinioService>();




var app = builder.Build();

app.UseCors("AllowAngularApp");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
