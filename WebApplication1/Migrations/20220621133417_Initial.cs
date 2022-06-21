using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationDomain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    OrganizationID = table.Column<int>(type: "int", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MemberSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MemberNickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => new { x.MemberID, x.OrganizationID });
                    table.ForeignKey(
                        name: "FK_Members_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    OrganizationID = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => new { x.TeamID, x.OrganizationID });
                    table.ForeignKey(
                        name: "FK_Teams_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamID1 = table.Column<int>(type: "int", nullable: false),
                    TeamOrganizationID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: true),
                    MemberOrganizationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => new { x.FileID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_Files_Members_MemberID_MemberOrganizationID",
                        columns: x => new { x.MemberID, x.MemberOrganizationID },
                        principalTable: "Members",
                        principalColumns: new[] { "MemberID", "OrganizationID" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Teams_TeamID1_TeamOrganizationID",
                        columns: x => new { x.TeamID1, x.TeamOrganizationID },
                        principalTable: "Teams",
                        principalColumns: new[] { "TeamID", "OrganizationID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    MembershipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberID1 = table.Column<int>(type: "int", nullable: false),
                    MemberOrganizationID = table.Column<int>(type: "int", nullable: false),
                    TeamID1 = table.Column<int>(type: "int", nullable: false),
                    TeamOrganizationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => new { x.MemberID, x.TeamID });
                    table.ForeignKey(
                        name: "FK_Memberships_Members_MemberID1_MemberOrganizationID",
                        columns: x => new { x.MemberID1, x.MemberOrganizationID },
                        principalTable: "Members",
                        principalColumns: new[] { "MemberID", "OrganizationID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Teams_TeamID1_TeamOrganizationID",
                        columns: x => new { x.TeamID1, x.TeamOrganizationID },
                        principalTable: "Teams",
                        principalColumns: new[] { "TeamID", "OrganizationID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_MemberID_MemberOrganizationID",
                table: "Files",
                columns: new[] { "MemberID", "MemberOrganizationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TeamID1_TeamOrganizationID",
                table: "Files",
                columns: new[] { "TeamID1", "TeamOrganizationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_OrganizationID",
                table: "Members",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_MemberID1_MemberOrganizationID",
                table: "Memberships",
                columns: new[] { "MemberID1", "MemberOrganizationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_TeamID1_TeamOrganizationID",
                table: "Memberships",
                columns: new[] { "TeamID1", "TeamOrganizationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganizationID",
                table: "Teams",
                column: "OrganizationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
