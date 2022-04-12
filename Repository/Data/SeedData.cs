using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repository.Data
{
    public class SeedData
    {
        public async static Task<string> ConvertToBase64(string filePath)
        {
            byte[] fileArray = await File.ReadAllBytesAsync(filePath);
            string base64 = @String.Format("data:image/*;base64,{0}", Convert.ToBase64String(fileArray));
            return base64;
        }

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
                        CandidateBase64Pic = await ConvertToBase64(tinubuPicFilePath) 
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "peter obi",
                        CandidateBase64Pic = await ConvertToBase64(obiPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "atiku",
                        CandidateBase64Pic = await ConvertToBase64(atikuPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "bukola saraki",
                        CandidateBase64Pic = await ConvertToBase64(sarakiPicFilePath)
                    },
                    new PresidentialCandidateSearchTerm 
                    { 
                        CandidateSearchTerm = "yemi osinbajo",
                        CandidateBase64Pic = await ConvertToBase64(osinbajoPicFilePath)
                    });
            }
            context.SaveChanges();
        }
    }
}
