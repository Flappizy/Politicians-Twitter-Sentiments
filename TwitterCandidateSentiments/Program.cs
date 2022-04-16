using BizLogic;
using BizLogic.AppClasses;
using Contracts.IRepository;
using Contracts.Sentiment;
using Microsoft.EntityFrameworkCore;
using Repository;
using Hangfire;
using Contracts;
using Contracts.Twitter;
using TwitterCandidateSentiments.Services;
using Repository.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var services = builder.Services;
var _policyName = "AllowAllOrigin";
var env = builder.Environment;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

string connectionString = null!;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    connectionString = builder.Configuration.GetConnectionString("ProdConnection");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}


//JobStorage.Current = new PostgreSqlStorage(connectionString);
GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(connectionString));

services.AddCors(options =>
{
    options.AddPolicy(_policyName,
        builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
});


services.AddHangfireServer();

services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
services.AddDatabaseDeveloperPageExceptionFilter();

TwitterSettings twitterSettings = new TwitterSettings();
builder.Configuration.GetSection("TwitterSetting").Bind(twitterSettings);

//This is used to set up the twitter client that will be injected by dependency injection
TweetClientWrapper twitterClient = new TweetClientWrapper(twitterSettings.ConsumerKey, twitterSettings.ConsumerSecret, twitterSettings.AccessToken, twitterSettings.AccessTokenSecret);

services.Configure<TwitterSettings>(builder.Configuration.GetSection("TwitterSetting"));
services.AddTransient<ISentimentClassifier, SentimentClassifierWrappedClass>();
services.AddTransient<ISentimentForTweetWith100CharsOrLess, SentimentForTweetWith100CharsOrLess>();
services.AddTransient<ISentimentForTweetWithMoreThan100Chars, SentimentForTweetWithMoreThan100Chars>();
services.AddTransient<ITweetExistence, TweetExistence>();
services.AddSingleton<ICustomTwitterClient>(twitterClient);
services.AddScoped<ICandidates, Candidates>();
services.AddTransient<ITweetSearch, TweetSearch>();
services.AddTransient<ISentiment, Sentiment>();
services.AddTransient<ILogicCombinationService, LogicCombinationService>();
services.AddScoped<IOpinionsRepo, OpinionsRepo>();
services.AddTransient<LogicRunner>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(_policyName);

RecurringJob.AddOrUpdate<LogicRunner>(x => x.GetTweetsPerformSentimentAndStorage(),
   "*/20 * * * *");


app.MapControllers();
app.UseHangfireDashboard();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

SeedData.EnsurePopulated(app).Wait();

app.Run();
