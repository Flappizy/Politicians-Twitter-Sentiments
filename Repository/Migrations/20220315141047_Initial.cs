using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    PresidentialCandidateSearchTermId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateSearchTerm = table.Column<string>(type: "text", nullable: true),
                    TotalNumberOfTweetsAssesed = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfNeutralTweets = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfNegativeTweets = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPositiveTweets = table.Column<long>(type: "bigint", nullable: false),
                    OverAllPublicSentimentOfCandidate = table.Column<string>(type: "text", nullable: true),
                    OverAllSentimentProbability = table.Column<double>(type: "double precision", nullable: false),
                    CandidateBase64Pic = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresidentialCandidatesSearchTerms", x => x.PresidentialCandidateSearchTermId);
                });

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    TweetId = table.Column<Guid>(type: "uuid", nullable: false),
                    TweetText = table.Column<string>(type: "text", nullable: true),
                    BestClassName = table.Column<string>(type: "text", nullable: true),
                    ProbabilityOfBeingPositive = table.Column<float>(type: "real", nullable: false),
                    BestClassProbability = table.Column<float>(type: "real", nullable: false),
                    PresidentialCandidateSearchTermId = table.Column<int>(type: "integer", nullable: false),
                    SentimentHasBeenPerformed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_Opinions_PresidentialCandidatesSearchTerms_PresidentialCand~",
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
