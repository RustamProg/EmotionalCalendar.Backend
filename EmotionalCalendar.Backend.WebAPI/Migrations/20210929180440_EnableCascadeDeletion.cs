using Microsoft.EntityFrameworkCore.Migrations;

namespace EmotionalCalendar.Backend.WebAPI.Migrations
{
    public partial class EnableCascadeDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyEmotions_Emotions_EmotionId",
                table: "DailyEmotions");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyEmotions_EventNotes_EventNoteId",
                table: "DailyEmotions");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyEmotions_Emotions_EmotionId",
                table: "DailyEmotions",
                column: "EmotionId",
                principalTable: "Emotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyEmotions_EventNotes_EventNoteId",
                table: "DailyEmotions",
                column: "EventNoteId",
                principalTable: "EventNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyEmotions_Emotions_EmotionId",
                table: "DailyEmotions");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyEmotions_EventNotes_EventNoteId",
                table: "DailyEmotions");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyEmotions_Emotions_EmotionId",
                table: "DailyEmotions",
                column: "EmotionId",
                principalTable: "Emotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyEmotions_EventNotes_EventNoteId",
                table: "DailyEmotions",
                column: "EventNoteId",
                principalTable: "EventNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
