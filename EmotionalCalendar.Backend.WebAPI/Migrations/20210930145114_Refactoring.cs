using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmotionalCalendar.Backend.WebAPI.Migrations
{
    public partial class Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyEmotions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyEmotions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmotionId = table.Column<long>(type: "bigint", nullable: true),
                    EmotionRate = table.Column<int>(type: "int", nullable: false),
                    EventNoteId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyEmotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyEmotions_Emotions_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyEmotions_EventNotes_EventNoteId",
                        column: x => x.EventNoteId,
                        principalTable: "EventNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyEmotions_EmotionId",
                table: "DailyEmotions",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyEmotions_EventNoteId",
                table: "DailyEmotions",
                column: "EventNoteId");
        }
    }
}
