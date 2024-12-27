using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emlak.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Propertie",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bedrooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propertie", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_Propertie_User_user_ID",
                        column: x => x.user_ID,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequest",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_ID = table.Column<int>(type: "int", nullable: false),
                    pro_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequest", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_ContactRequest_Propertie_pro_ID",
                        column: x => x.pro_ID,
                        principalTable: "Propertie",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactRequest_User_user_ID",
                        column: x => x.user_ID,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImage",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imageURL = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    pro_ID = table.Column<int>(type: "int", nullable: false),
                    Usersuser_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImage", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_PropertyImage_Propertie_pro_ID",
                        column: x => x.pro_ID,
                        principalTable: "Propertie",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyImage_User_Usersuser_id",
                        column: x => x.Usersuser_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_pro_ID",
                table: "ContactRequest",
                column: "pro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequest_user_ID",
                table: "ContactRequest",
                column: "user_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Propertie_user_ID",
                table: "Propertie",
                column: "user_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_pro_ID",
                table: "PropertyImage",
                column: "pro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_Usersuser_id",
                table: "PropertyImage",
                column: "Usersuser_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRequest");

            migrationBuilder.DropTable(
                name: "PropertyImage");

            migrationBuilder.DropTable(
                name: "Propertie");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
