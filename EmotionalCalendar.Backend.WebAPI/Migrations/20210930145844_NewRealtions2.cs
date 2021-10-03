using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmotionalCalendar.Backend.WebAPI.Migrations
{
    public partial class NewRealtions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emotions_EventNotes_EventNoteId",
                table: "Emotions");

            migrationBuilder.DropIndex(
                name: "IX_Emotions_EventNoteId",
                table: "Emotions");

            migrationBuilder.DropColumn(
                name: "EventNoteId",
                table: "Emotions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "EventNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "EventNotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EmotionEventNote",
                columns: table => new
                {
                    EmotionsId = table.Column<long>(type: "bigint", nullable: false),
                    EventNotesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionEventNote", x => new { x.EmotionsId, x.EventNotesId });
                    table.ForeignKey(
                        name: "FK_EmotionEventNote_Emotions_EmotionsId",
                        column: x => x.EmotionsId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmotionEventNote_EventNotes_EventNotesId",
                        column: x => x.EventNotesId,
                        principalTable: "EventNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmotionEventNote_EventNotesId",
                table: "EmotionEventNote",
                column: "EventNotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmotionEventNote");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "EventNotes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventNotes");

            migrationBuilder.AddColumn<long>(
                name: "EventNoteId",
                table: "Emotions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emotions_EventNoteId",
                table: "Emotions",
                column: "EventNoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emotions_EventNotes_EventNoteId",
                table: "Emotions",
                column: "EventNoteId",
                principalTable: "EventNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
