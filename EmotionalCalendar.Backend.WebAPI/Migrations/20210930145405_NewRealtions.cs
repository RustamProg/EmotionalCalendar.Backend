using Microsoft.EntityFrameworkCore.Migrations;

namespace EmotionalCalendar.Backend.WebAPI.Migrations
{
    public partial class NewRealtions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
