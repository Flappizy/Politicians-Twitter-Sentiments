using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.EFHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace UnitTests.ECandidatesClassUnitTests
{
    //Thanks to Jon P Smith "Ef Core in Action" for making this really easy for me
    public class CandidatesUnitTests
    {
        [Fact]
        public async Task GetCandidatesAsync_WhenDataIsAskedFor_GetAllCandidates()
        {
            //Arrange
            var inMemDb = new SqliteInMemory();
            int expectedResultCount = 5;

            using (var context = inMemDb.GetContextWithSetup())
            {
                var dbAccess = new Candidates(context);
                context.SeedDatabaseDummyCandidates();

                //Act
                var listOfCandidates = await dbAccess.GetCandidatesAsync();
                var actualResultCount = listOfCandidates.Count();

                //VERIFY
                Assert.Equal(expectedResultCount, actualResultCount);
            }

        }
    }
}
