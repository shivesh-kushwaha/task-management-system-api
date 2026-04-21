using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TypeIdIsRequiredForWorkItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Ensure a default WorkItemType (Id = 1) exists
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM WorkItemTypes WHERE Id = 1)
                BEGIN
                    INSERT INTO WorkItemTypes (Id, Name, Status) 
                    VALUES (1, 'Uncategorized', 1);
                END
            ");

            // 2. Update all existing NULL TypeId values to the default (1)
            migrationBuilder.Sql("UPDATE WorkItems SET TypeId = 1 WHERE TypeId IS NULL");

            // 3. Alter the column to NOT NULL, with a valid default value (1)
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 1,                          // ← matches the default type
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert to nullable column (no default value)
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "WorkItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}