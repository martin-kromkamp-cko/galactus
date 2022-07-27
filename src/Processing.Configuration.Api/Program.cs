using Processing.Configuration;
using Processing.Configuration.Api.GraphQL;
using Processing.Configuration.Api.GraphQL.Types;
using Processing.Configuration.Infra;
using Processing.Configuration.Infra.Data.Processing;

var builder = WebApplication.CreateBuilder(args);

// Api
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// GraphQL
builder.Services.AddGraphQLServer()
    .RegisterDbContext<ProcessingContext>(DbContextKind.Pooled)
    .AddQueryType<QueryType>()
    .AddType<CurrencyType>()
    .AddType<CardSchemeType>()
    .AddType<MerchantCategoryCodeType>()
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
builder.Services.AddAuditing(builder.Configuration);

var app = builder.Build();
app.AddApiAuditing();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
