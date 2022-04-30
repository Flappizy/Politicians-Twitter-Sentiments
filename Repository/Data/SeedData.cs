using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repository.Data
{
    public class SeedData
    {        
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationContext context = 
                app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.PresidentialCandidatesSearchTerms.Any())
            {
                string tinubuPicFilePath = Directory.GetCurrentDirectory() + "\\Assets\\Asiwaju-Bola-Ahmed-Tinubu-PDR.jpeg";
                string obiPicFilePath = Directory.GetCurrentDirectory() + "\\Assets\\Peter-Obi.jpg";
                string atikuPicFilePath = Directory.GetCurrentDirectory() + "\\Assets\\atiku.jpg";
                string sarakiPicFilePath = Directory.GetCurrentDirectory() + "\\Assets\\Saraki_5.jpg";
                string osinbajoPicFilePath = Directory.GetCurrentDirectory() + "\\Assets\\Yemi-Osinbajo-Harvard-600x600.jpg";
                //byte[] guarantorFormBytes = await FileUploadService.ConvertFilePathToByteArray(filePath);
                await context.AddRangeAsync(
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "tinubu", 
                        CandidatePicFile = await File.ReadAllBytesAsync(tinubuPicFilePath) 
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "peter obi",
                        CandidatePicFile = await File.ReadAllBytesAsync(obiPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "atiku",
                        CandidatePicFile = await File.ReadAllBytesAsync(atikuPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "bukola saraki",
                        CandidatePicFile = await File.ReadAllBytesAsync(sarakiPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "yemi osinbajo",
                        CandidatePicFile = await File.ReadAllBytesAsync(osinbajoPicFilePath)
                    });
            }
            context.SaveChanges();
        }
    }
}
