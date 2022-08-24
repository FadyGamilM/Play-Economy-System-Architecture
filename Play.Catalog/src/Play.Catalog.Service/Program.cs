using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Play.Catalog.Service.Configurations;
using Play.Catalog.Service.Interfaces;
using Play.Catalog.Service.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//! Add my services
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbConfig>();
var serviceSettings = builder.Configuration.GetSection("ServiceSettings").Get<ServiceConfig>();
builder.Services.AddScoped<IMongoClient>(
    ServiceProvider => {
        return new MongoClient(mongoDbSettings.ConnectionString);
    }
);
BsonSerializer.RegisterSerializer(
    new GuidSerializer(
        BsonType.String
    )
);
BsonSerializer.RegisterSerializer(
    new DateTimeOffsetSerializer(
        BsonType.String
    )
);
builder.Services.AddScoped<IItemsRepo, ItemsRepo>();
//!-----------------------------------------



var app = builder.Build();

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
