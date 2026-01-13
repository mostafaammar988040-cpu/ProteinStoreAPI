using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProteinStore.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "Name", "Price", "SubCategory", "Weight" },
                values: new object[,]
                {
                    { 1, "Protein", "Advanced whey protein.", "/products/nitro-tech.jpg", "MuscleTech Nitro Tech Whey", 55m, "Whey", "4 lbs" },
                    { 2, "Protein", "Premium whey protein.", "/products/ghost-whey.jpg", "Ghost Whey Protein", 48m, "Whey", "2 lbs" },
                    { 3, "Protein", "Ultra premium whey.", "/products/bpi-whey-hd.jpg", "BPI Whey HD", 52m, "Whey", "4.07 lbs" },
                    { 4, "Protein", "Clean isolate.", "/products/raw-isotholate.jpg", "RAW Isotholate Protein", 60m, "Isolate", "2.9 lbs" },
                    { 5, "Protein", "Mass whey.", "/products/big-ramy-big-whey.jpg", "Big Ramy Big Whey", 58m, "Whey", "5 lbs" },
                    { 6, "Protein", "Clean blend.", "/products/rule1-whey.jpg", "Rule 1 Whey Blend", 45m, "Whey", "2 lbs" },
                    { 7, "Protein", "Hydro isolate.", "/products/dymatize-iso100.jpg", "Dymatize ISO100", 65m, "Isolate", "1.43 lbs" },
                    { 8, "Protein", "Value whey.", "/products/warrior-whey.jpg", "Warrior Whey Protein", 42m, "Whey", "2.2 lbs" },
                    { 9, "Protein", "Pure isolate.", "/products/iso-hearn.jpg", "ISO Hearn Protein", 68m, "Isolate", "4.9 lbs" },
                    { 10, "Protein", "Zero sugar.", "/products/challenger-whey-isolate.jpg", "Challenger Whey Isolate", 63m, "Isolate", "4.4 lbs" },
                    { 11, "Protein", "Chocolate brownie bar.", "/products/quest-brownie.jpg", "Quest Brownie Bar", 4m, "Bar", "60g" },
                    { 12, "Protein", "Cookie dough bar.", "/products/quest-cookie-dough.jpg", "Quest Cookie Dough Bar", 4m, "Bar", "60g" },
                    { 13, "Protein", "Cookies & cream.", "/products/quest-cookies-cream.jpg", "Quest Cookies & Cream Bar", 4m, "Bar", "60g" },
                    { 14, "Protein", "Soft cookie.", "/products/quest-double-chocolate-cookie.jpg", "Quest Double Chocolate Cookie", 4m, "Bar", "59g" },
                    { 15, "Protein", "Peanut butter.", "/products/quest-protein-bar-chocolate-peanut.jpg", "Quest Peanut Butter Bar", 4m, "Bar", "60g" },
                    { 16, "Gainer", "High calorie.", "/products/critical-mass.jpg", "Critical Mass Gainer", 75m, "Mass", "6 kg" },
                    { 17, "Gainer", "All in one.", "/products/hard-mass-gainer.jpg", "Hard Mass Gainer", 80m, "Mass", "12 lbs" },
                    { 18, "Carbs", "Fast carbs.", "/products/carbox.jpg", "Carbox Carb Powder", 30m, "Carb", "1 kg" },
                    { 19, "Creatine", "Micronized.", "/products/nutrex-creatine.jpg", "Nutrex Creatine", 25m, "Monohydrate", "300g" },
                    { 20, "Creatine", "Pure creatine.", "/products/scitec-creatine.jpg", "Scitec Creatine", 27m, "Monohydrate", "300g" },
                    { 21, "Creatine", "Beauty blend.", "/products/nutrex-creatine-women.jpg", "Creatine for Women", 29m, "Blend", "330g" },
                    { 22, "Creatine", "Strength.", "/products/challenger-creatine.jpg", "Challenger Creatine", 24m, "Monohydrate", "300g" },
                    { 23, "Amino", "Hydration.", "/products/nutrex-eaa-hydration.jpg", "Nutrex EAA Hydration", 35m, "EAA", "390g" },
                    { 24, "Amino", "Recovery.", "/products/nutritech-amino-boost.jpg", "Nutritech Amino Boost", 32m, "Amino", "400g" },
                    { 25, "Vitamins", "ZMA.", "/products/quamtrax-zma.jpg", "Quamtrax ZMA", 20m, "ZMA", "90 Capsules" },
                    { 26, "Vitamins", "Omega 3.", "/products/ruleone-fish-oil.jpg", "Rule One Fish Oil", 18m, "Fish Oil", "100 Softgels" },
                    { 27, "Vitamins", "Daily vitamins.", "/products/muscletech-platinum-multivitamin.jpg", "MuscleTech Multivitamin", 22m, "Multivitamin", "90 Tablets" },
                    { 28, "Vitamins", "Men health.", "/products/mens-multi.jpg", "Men's Multivitamin", 24m, "Multivitamin", "90 Tablets" },
                    { 29, "Vitamins", "Women health.", "/products/womens-multi.jpg", "Women's Multivitamin", 24m, "Multivitamin", "60 Tablets" },
                    { 30, "Health", "Organic moringa.", "/products/organic-moringa.jpg", "Organic Moringa Powder", 19m, "Superfood", "200g" },
                    { 31, "Health", "Testosterone.", "/products/tongkat-ali.jpg", "Tongkat Ali", 28m, "Herbal", "120 Capsules" },
                    { 32, "Health", "Stress.", "/products/ashwagandha.jpg", "Ashwagandha", 26m, "Herbal", "60 Capsules" },
                    { 33, "Protein", "Mint chocolate protein bar.", "/products/fitcrunch-mint.jpg", "FitCrunch Mint Chocolate", 3m, "Bar", "46g" },
                    { 34, "Protein", "White chocolate raspberry bar.", "/products/quest-white-raspberry.jpg", "Quest White Raspberry", 4m, "Bar", "60g" },
                    { 35, "Protein", "Crunchy protein bar.", "/products/crunch-bar.jpg", "Crunch Protein Bar", 3m, "Bar", "60g" },
                    { 36, "Protein", "Chocolate brownie protein bar.", "/products/fitcrunch-brownie.jpg", "FitCrunch Chocolate Brownie", 3m, "Bar", "46g" },
                    { 37, "Health", "CLA fat support.", "/products/cla.jpg", "Allmax CLA", 28m, "Fat Support", "150 Softgels" },
                    { 38, "Health", "Recovery support.", "/products/glutamine.jpg", "Glutamine", 28m, "Recovery Support", "390g" },
                    { 39, "Health", "Muscle supplement.", "/products/biotech-hmb.jpg", "HMB", 28m, "Muscle Support", "30 Servings" },
                    { 40, "Hydration", "Electrolyte hydration.", "/products/1lytes.jpg", "Rule One 1LYTES", 28m, "Electrolytes", "40 Servings" },
                    { 41, "Amino", "BCAA recovery support.", "/products/bcaa-rule1.jpg", "Rule One BCAA", 32m, "BCAA", "30 Servings" },
                    { 42, "Health", "Metabolic support.", "/products/berberine.jpg", "Berberine", 25m, "Metabolism", "60 Capsules" },
                    { 43, "Fat Burner", "Thermogenic fat burner.", "/products/lipo6-black.jpg", "Nutrex Lipo-6 Black", 38m, "Thermogenic", "60 Capsules" },
                    { 44, "Pre-Workout", "High energy.", "/products/total-war.jpg", "Redcon1 Total War", 45m, "High Stim", "30 Servings" },
                    { 45, "Pre-Workout", "Alternate flavor.", "/products/total-wr.jpg", "Redcon1 Total War (Alt)", 45m, "High Stim", "30 Servings" },
                    { 47, "Amino", "Pump support.", "/products/l-arginine.jpg", "Nutrex L-Arginine", 27m, "Nitric Oxide", "120 Capsules" },
                    { 48, "Fat Burner", "Body composition.", "/products/bpi-cla.jpg", "BPI Sports CLA", 28m, "CLA", "150 Softgels" },
                    { 49, "Pre-Workout", "Stim-free pump.", "/products/moons-truck-zero.jpg", "Moons Truck Zero", 42m, "Stim Free", "30 Servings" },
                    { 50, "Pre-Workout", "Energy & focus.", "/products/C4.jpg", "C4 Original Pre-Workout", 45m, "Energy & Focus", "50 Servings" },
                    { 51, "Health", "Collagen support.", "/products/ultra-collagen.jpg", "Ultra Collagen Grass Fed Peptides", 34m, "Collagen", "198 g" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
