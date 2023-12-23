using DeviceApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var specOrigin = "MySpecOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specOrigin, policy =>
    {
        policy.WithOrigins("https://localhost:7026")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.RegisterUserEndpoints();
app.UseCors(specOrigin);
app.Run();

