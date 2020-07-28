using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication7.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Betreuer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    EMail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betreuer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thesen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    SupervisorId = table.Column<int>(nullable: true),
                    Bachelor = table.Column<bool>(nullable: false),
                    Master = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StudentName = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    ProgrammeId = table.Column<int>(nullable: true),
                    Registration = table.Column<DateTime>(nullable: true),
                    Filing = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Strenghts = table.Column<string>(nullable: true),
                    Weaknesses = table.Column<string>(nullable: true),
                    Evaluation = table.Column<string>(nullable: true),
                    ContentVal = table.Column<int>(nullable: true),
                    LayoutVal = table.Column<int>(nullable: true),
                    StructureVal = table.Column<int>(nullable: true),
                    StyleVal = table.Column<int>(nullable: true),
                    LiteratureVal = table.Column<int>(nullable: true),
                    DifficultyVal = table.Column<int>(nullable: true),
                    NoveltyVal = table.Column<int>(nullable: true),
                    RichnessVal = table.Column<int>(nullable: true),
                    ContentWt = table.Column<int>(nullable: false),
                    LayoutWt = table.Column<int>(nullable: false),
                    StrucutreWt = table.Column<int>(nullable: false),
                    StyleWt = table.Column<int>(nullable: false),
                    LiteratureWt = table.Column<int>(nullable: false),
                    DifficultyWt = table.Column<int>(nullable: false),
                    NoveltyWt = table.Column<int>(nullable: false),
                    RichnessWt = table.Column<int>(nullable: false),
                    WeightSum = table.Column<int>(nullable: true),
                    Grade = table.Column<double>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thesen_Programme_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Thesen_Betreuer_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Betreuer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thesen_ProgrammeId",
                table: "Thesen",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_Thesen_SupervisorId",
                table: "Thesen",
                column: "SupervisorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thesen");

            migrationBuilder.DropTable(
                name: "Programme");

            migrationBuilder.DropTable(
                name: "Betreuer");
        }
    }
}
