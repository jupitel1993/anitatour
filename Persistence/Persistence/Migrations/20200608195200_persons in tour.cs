using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class personsintour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Tour_TourId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Users_UserId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Program_ProgramId",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "SuggestedTourist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tour",
                table: "Tour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Program",
                table: "Program");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Tour",
                newName: "Tours");

            migrationBuilder.RenameTable(
                name: "Program",
                newName: "Programs");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_ProgramId",
                table: "Tours",
                newName: "IX_Tours_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_Id",
                table: "Tours",
                newName: "IX_Tours_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Program_Id",
                table: "Programs",
                newName: "IX_Programs_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Person_UserId",
                table: "Persons",
                newName: "IX_Persons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_TourId",
                table: "Persons",
                newName: "IX_Persons_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_Id",
                table: "Persons",
                newName: "IX_Persons_Id");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                table: "Programs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Tours_TourId",
                table: "Persons",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Programs_ProgramId",
                table: "Tours",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Tours_TourId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Programs_ProgramId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Tours",
                newName: "Tour");

            migrationBuilder.RenameTable(
                name: "Programs",
                newName: "Program");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_ProgramId",
                table: "Tour",
                newName: "IX_Tour_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_Id",
                table: "Tour",
                newName: "IX_Tour_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Programs_Id",
                table: "Program",
                newName: "IX_Program_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_UserId",
                table: "Person",
                newName: "IX_Person_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_TourId",
                table: "Person",
                newName: "IX_Person_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_Id",
                table: "Person",
                newName: "IX_Person_Id");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "Person",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tour",
                table: "Tour",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Program",
                table: "Program",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SuggestedTourist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestedTourist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestedTourist_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuggestedTourist_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuggestedTourist_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedTourist_Id",
                table: "SuggestedTourist",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedTourist_PersonId",
                table: "SuggestedTourist",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedTourist_TourId",
                table: "SuggestedTourist",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedTourist_UserId",
                table: "SuggestedTourist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Tour_TourId",
                table: "Person",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Users_UserId",
                table: "Person",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Program_ProgramId",
                table: "Tour",
                column: "ProgramId",
                principalTable: "Program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
