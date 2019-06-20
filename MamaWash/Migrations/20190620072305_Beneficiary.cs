using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MamaWash.Migrations
{
    public partial class Beneficiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(maxLength: 10, nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    BankID = table.Column<string>(nullable: true),
                    BankCodeID = table.Column<int>(nullable: true),
                    BankNameID = table.Column<int>(nullable: true),
                    RecipientCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_BankList_BankCodeID",
                        column: x => x.BankCodeID,
                        principalTable: "BankList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_BankList_BankNameID",
                        column: x => x.BankNameID,
                        principalTable: "BankList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BeneficiariesID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Beneficiaries_BeneficiariesID",
                        column: x => x.BeneficiariesID,
                        principalTable: "Beneficiaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_BankCodeID",
                table: "Beneficiaries",
                column: "BankCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_BankNameID",
                table: "Beneficiaries",
                column: "BankNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_BeneficiariesID",
                table: "TransactionHistory",
                column: "BeneficiariesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "Beneficiaries");
        }
    }
}
