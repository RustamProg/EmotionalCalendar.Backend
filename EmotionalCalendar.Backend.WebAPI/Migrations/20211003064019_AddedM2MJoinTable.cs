using Microsoft.EntityFrameworkCore.Migrations;

namespace EmotionalCalendar.Backend.WebAPI.Migrations
{
    public partial class AddedM2MJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmotionEventNote");

            migrationBuilder.CreateTable(
                name: "EmotionEventRate",
                columns: table => new
                {
                    EventNoteId = table.Column<long>(type: "bigint", nullable: false),
                    EmotionId = table.Column<long>(type: "bigint", nullable: false),
                    EmotionRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionEventRate", x => new { x.EmotionId, x.EventNoteId });
                    table.ForeignKey(
                        name: "FK_EmotionEventRate_Emotions_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmotionEventRate_EventNotes_EventNoteId",
                        column: x => x.EventNoteId,
                        principalTable: "EventNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmotionEventRate_EventNoteId",
                table: "EmotionEventRate",
                column: "EventNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmotionEventRate");

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
    }
}
