using Microsoft.EntityFrameworkCore;
using Processing.Configuration;
using Processing.Configuration.Api.GraphQL;
using Processing.Configuration.Api.GraphQL.Types;
using Processing.Configuration.Infra;
using Processing.Configuration.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddPooledDbContextFactory<ProcessingContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ProcessingContext)),
        pg =>
        {
            pg.EnableRetryOnFailure(2);
        })
        .UseSnakeCaseNamingConvention();
    cfg.EnableDetailedErrors();
});

// Api
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// GraphQL
builder.Services.AddGraphQLServer()
    .RegisterDbContext<ProcessingContext>(DbContextKind.Pooled)
    .AddQueryType<QueryType>()
    .AddType<CurrencyType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .ModifyRequestOptions(o =>
    {
        o.Complexity.Enable = true;
        o.Complexity.ApplyDefaults = true;
        o.Complexity.MaximumAllowed = 150;
    });

// Services
builder.Services.AddCore(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);

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
app.MapGraphQL();

app.Run();
