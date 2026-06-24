using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using NoteService;
using NoteService.Data;
using NoteService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder =
            System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrez : Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Connexion à MongoDB
builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseMongoDB(
        builder.Configuration["MongoDB:ConnectionString"] ?? "mongodb://mongodb:27017",
        builder.Configuration["MongoDB:DatabaseName"] ?? "NoteDb"
    ));

var app = builder.Build();

// Seed des notes de test au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NoteDbContext>();

    if (!db.Notes.Any())
    {
        db.Notes.AddRange(
            new Note { PatientId = 1, PatientName = "TestNone", Content = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé" },
            new Note { PatientId = 2, PatientName = "TestBorderline", Content = "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement" },
            new Note { PatientId = 2, PatientName = "TestBorderline", Content = "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale" },
            new Note { PatientId = 3, PatientName = "TestInDanger", Content = "Le patient déclare qu'il fume depuis peu" },
            new Note { PatientId = 3, PatientName = "TestInDanger", Content = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière Il se plaint également de crises d'apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé" },
            new Note { PatientId = 4, PatientName = "TestEarlyOnset", Content = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d'être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
            new Note { PatientId = 4, PatientName = "TestEarlyOnset", Content = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
            new Note { PatientId = 4, PatientName = "TestEarlyOnset", Content = "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé" },
            new Note { PatientId = 4, PatientName = "TestEarlyOnset", Content = "Taille, Poids, Cholestérol, Vertige et Réaction" }
        );
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();