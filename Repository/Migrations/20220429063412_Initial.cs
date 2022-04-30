using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PresidentialCandidatesSearchTerms",
                columns: table => new
                {
                    PresidentialCandidateSearchTermId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateSearchTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalNumberOfTweetsAssesed = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfNeutralTweets = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfNegativeTweets = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPositiveTweets = table.Column<long>(type: "bigint", nullable: false),
                    OverAllPublicSentimentOfCandidate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverAllSentimentProbability = table.Column<double>(type: "float", nullable: false),
                    CandidatePicFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LatestDateAndTimeOfLastCollectedTweet = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresidentialCandidatesSearchTerms", x => x.PresidentialCandidateSearchTermId);
                });

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    TweetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TweetText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProbabilityOfBeingPositive = table.Column<float>(type: "real", nullable: false),
                    BestClassProbability = table.Column<float>(type: "real", nullable: false),
                    PresidentialCandidateSearchTermId = table.Column<int>(type: "int", nullable: false),
                    SentimentHasBeenPerformed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_Opinions_PresidentialCandidatesSearchTerms_PresidentialCandidateSearchTermId",
                        column: x => x.PresidentialCandidateSearchTermId,
                        principalTable: "PresidentialCandidatesSearchTerms",
                        principalColumn: "PresidentialCandidateSearchTermId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opinions_PresidentialCandidateSearchTermId",
                table: "Opinions",
                column: "PresidentialCandidateSearchTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropTable(
                name: "PresidentialCandidatesSearchTerms");
        }
    }
}
