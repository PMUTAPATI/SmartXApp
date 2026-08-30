using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Reference: Microsoft Learn - Upload files in ASP.NET Core Minimal APIs
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api
// Telemetry endpoints
app.MapPost("/api/telemetry/numeric", ([FromBody] object packet) =>
{
    return Results.Ok(new { status = "Ingested", timestamp = DateTime.UtcNow, payload = packet });
});

app.MapPost("/api/telemetry/boolean", ([FromBody] object packet) =>
{
    return Results.Ok(new { status = "Ingested", timestamp = DateTime.UtcNow, payload = packet });
});

// Config file upload
app.MapPost("/api/sensors/upload-config", async (HttpRequest request) =>
{
    if (!request.HasFormContentType)
    {
        return Results.BadRequest("Expected multipart/form-data content type.");
    }

    var form = await request.ReadFormAsync();
    var file = form.Files.GetFile("file");
    var macAddress = form["macAddress"].ToString();

    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded.");
    }

    // Replace colons/dashes in MAC address for file path
    var safeMac = string.IsNullOrWhiteSpace(macAddress)
        ? "UNKNOWN_MAC"
        : macAddress.Replace(":", "_").Replace("-", "_");

    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedConfigs");
    Directory.CreateDirectory(uploadPath);

    var fullPath = Path.Combine(uploadPath, $"{safeMac}_{file.FileName}");

    using (var stream = new FileStream(fullPath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return Results.Ok(new { message = "File uploaded successfully", path = fullPath });
}).DisableAntiforgery();

app.Run();