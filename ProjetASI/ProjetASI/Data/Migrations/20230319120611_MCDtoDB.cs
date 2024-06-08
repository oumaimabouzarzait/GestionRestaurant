using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetASI.Data.Migrations
{
    public partial class MCDtoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caissier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caissier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<decimal>(type: "Money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serveur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serveur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NbPlaces = table.Column<int>(type: "int", nullable: false),
                    NbClients = table.Column<int>(type: "int", nullable: false),
                    Etat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Etat = table.Column<int>(type: "int", nullable: false),
                    Encaissee = table.Column<bool>(type: "bit", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    ServeurId = table.Column<int>(type: "int", nullable: false),
                    BarmanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commande_Barman_BarmanId",
                        column: x => x.BarmanId,
                        principalTable: "Barman",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commande_Serveur_ServeurId",
                        column: x => x.ServeurId,
                        principalTable: "Serveur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commande_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeProduit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaCommandeId = table.Column<int>(type: "int", nullable: true),
                    LeProduitId = table.Column<int>(type: "int", nullable: false),
                    QuantiteProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeProduit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommandeProduit_Commande_LaCommandeId",
                        column: x => x.LaCommandeId,
                        principalTable: "Commande",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommandeProduit_Produit_LeProduitId",
                        column: x => x.LeProduitId,
                        principalTable: "Produit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontantTotal = table.Column<decimal>(type: "Money", nullable: false),
                    DateHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaissierId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facture_Caissier_CaissierId",
                        column: x => x.CaissierId,
                        principalTable: "Caissier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facture_Commande_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commande",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encaissement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<decimal>(type: "Money", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FactureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encaissement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Encaissement_Facture_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Facture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commande_BarmanId",
                table: "Commande",
                column: "BarmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_ServeurId",
                table: "Commande",
                column: "ServeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_TableId",
                table: "Commande",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeProduit_LaCommandeId",
                table: "CommandeProduit",
                column: "LaCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeProduit_LeProduitId",
                table: "CommandeProduit",
                column: "LeProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Encaissement_FactureId",
                table: "Encaissement",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_CaissierId",
                table: "Facture",
                column: "CaissierId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_CommandeId",
                table: "Facture",
                column: "CommandeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandeProduit");

            migrationBuilder.DropTable(
                name: "Encaissement");

            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Caissier");

            migrationBuilder.DropTable(
                name: "Commande");

            migrationBuilder.DropTable(
                name: "Barman");

            migrationBuilder.DropTable(
                name: "Serveur");

            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
