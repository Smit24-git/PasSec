using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddVaultModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaults",
                columns: table => new
                {
                    VaultId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VaultName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppliedCustomKey = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IV = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaults", x => x.VaultId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultStorageKeys",
                columns: table => new
                {
                    VaultStorageKeyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VaultId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IV = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultStorageKeys", x => x.VaultStorageKeyId);
                    table.ForeignKey(
                        name: "FK_VaultStorageKeys_Vaults_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vaults",
                        principalColumn: "VaultId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultStorageKeysSecurityQAs",
                columns: table => new
                {
                    VaultStorageKeySecurityQAId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VaultStorageKeyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Answer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IV = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultStorageKeysSecurityQAs", x => x.VaultStorageKeySecurityQAId);
                    table.ForeignKey(
                        name: "FK_VaultStorageKeysSecurityQAs_VaultStorageKeys_VaultStorageKey~",
                        column: x => x.VaultStorageKeyId,
                        principalTable: "VaultStorageKeys",
                        principalColumn: "VaultStorageKeyId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VaultStorageKeys_VaultId",
                table: "VaultStorageKeys",
                column: "VaultId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultStorageKeysSecurityQAs_VaultStorageKeyId",
                table: "VaultStorageKeysSecurityQAs",
                column: "VaultStorageKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaultStorageKeysSecurityQAs");

            migrationBuilder.DropTable(
                name: "VaultStorageKeys");

            migrationBuilder.DropTable(
                name: "Vaults");
        }
    }
}
