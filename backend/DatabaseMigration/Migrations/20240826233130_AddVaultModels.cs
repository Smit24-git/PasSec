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
                name: "Vault",
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
                    table.PrimaryKey("PK_Vault", x => x.VaultId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultStorageKey",
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
                    table.PrimaryKey("PK_VaultStorageKey", x => x.VaultStorageKeyId);
                    table.ForeignKey(
                        name: "FK_VaultStorageKey_Vault_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vault",
                        principalColumn: "VaultId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VaultStorageKeySecurityQA",
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
                    table.PrimaryKey("PK_VaultStorageKeySecurityQA", x => x.VaultStorageKeySecurityQAId);
                    table.ForeignKey(
                        name: "FK_VaultStorageKeySecurityQA_VaultStorageKey_VaultStorageKeyId",
                        column: x => x.VaultStorageKeyId,
                        principalTable: "VaultStorageKey",
                        principalColumn: "VaultStorageKeyId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VaultStorageKey_VaultId",
                table: "VaultStorageKey",
                column: "VaultId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultStorageKeySecurityQA_VaultStorageKeyId",
                table: "VaultStorageKeySecurityQA",
                column: "VaultStorageKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaultStorageKeySecurityQA");

            migrationBuilder.DropTable(
                name: "VaultStorageKey");

            migrationBuilder.DropTable(
                name: "Vault");
        }
    }
}
