using Microsoft.EntityFrameworkCore;
using ProteinStore.API.Models;

namespace ProteinStore.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>()
    .HasOne(oi => oi.Order)
    .WithMany(o => o.OrderItems)
    .HasForeignKey(oi => oi.OrderId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasData(

            // ===================== PROTEIN =====================
            new Product { Id = 1, Name = "MuscleTech Nitro Tech Whey", Category = "Protein", SubCategory = "Whey", Weight = "4 lbs", Price = 55, Description = "Advanced whey protein.", ImageUrl = "/products/nitro-tech.jpg" },
            new Product { Id = 2, Name = "Ghost Whey Protein", Category = "Protein", SubCategory = "Whey", Weight = "2 lbs", Price = 48, Description = "Premium whey protein.", ImageUrl = "/products/ghost-whey.jpg" },
            new Product { Id = 3, Name = "BPI Whey HD", Category = "Protein", SubCategory = "Whey", Weight = "4.07 lbs", Price = 52, Description = "Ultra premium whey.", ImageUrl = "/products/bpi-whey-hd.jpg" },
            new Product { Id = 4, Name = "RAW Isotholate Protein", Category = "Protein", SubCategory = "Isolate", Weight = "2.9 lbs", Price = 60, Description = "Clean isolate.", ImageUrl = "/products/raw-isotholate.jpg" },
            new Product { Id = 5, Name = "Big Ramy Big Whey", Category = "Protein", SubCategory = "Whey", Weight = "5 lbs", Price = 58, Description = "Mass whey.", ImageUrl = "/products/big-ramy-big-whey.jpg" },
            new Product { Id = 6, Name = "Rule 1 Whey Blend", Category = "Protein", SubCategory = "Whey", Weight = "2 lbs", Price = 45, Description = "Clean blend.", ImageUrl = "/products/rule1-whey.jpg" },
            new Product { Id = 7, Name = "Dymatize ISO100", Category = "Protein", SubCategory = "Isolate", Weight = "1.43 lbs", Price = 65, Description = "Hydro isolate.", ImageUrl = "/products/dymatize-iso100.jpg" },
            new Product { Id = 8, Name = "Warrior Whey Protein", Category = "Protein", SubCategory = "Whey", Weight = "2.2 lbs", Price = 42, Description = "Value whey.", ImageUrl = "/products/warrior-whey.jpg" },
            new Product { Id = 9, Name = "ISO Hearn Protein", Category = "Protein", SubCategory = "Isolate", Weight = "4.9 lbs", Price = 68, Description = "Pure isolate.", ImageUrl = "/products/iso-hearn.jpg" },
            new Product { Id = 10, Name = "Challenger Whey Isolate", Category = "Protein", SubCategory = "Isolate", Weight = "4.4 lbs", Price = 63, Description = "Zero sugar.", ImageUrl = "/products/challenger-whey-isolate.jpg" },

            // ===================== PROTEIN BARS =====================
            new Product { Id = 11, Name = "Quest Brownie Bar", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 4, Description = "Chocolate brownie bar.", ImageUrl = "/products/quest-brownie.jpg" },
            new Product { Id = 12, Name = "Quest Cookie Dough Bar", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 4, Description = "Cookie dough bar.", ImageUrl = "/products/quest-cookie-dough.jpg" },
            new Product { Id = 13, Name = "Quest Cookies & Cream Bar", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 4, Description = "Cookies & cream.", ImageUrl = "/products/quest-cookies-cream.jpg" },
            new Product { Id = 14, Name = "Quest Double Chocolate Cookie", Category = "Protein", SubCategory = "Bar", Weight = "59g", Price = 4, Description = "Soft cookie.", ImageUrl = "/products/quest-double-chocolate-cookie.jpg" },
            new Product { Id = 15, Name = "Quest Peanut Butter Bar", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 4, Description = "Peanut butter.", ImageUrl = "/products/quest-protein-bar-chocolate-peanut.jpg" },
            new Product { Id = 33, Name = "FitCrunch Mint Chocolate", Category = "Protein", SubCategory = "Bar", Weight = "46g", Price = 3, Description = "Mint chocolate protein bar.", ImageUrl = "/products/fitcrunch-mint.jpg" },
            new Product { Id = 34, Name = "Quest White Raspberry", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 4, Description = "White chocolate raspberry bar.", ImageUrl = "/products/quest-white-raspberry.jpg" },
            new Product { Id = 35, Name = "Crunch Protein Bar", Category = "Protein", SubCategory = "Bar", Weight = "60g", Price = 3, Description = "Crunchy protein bar.", ImageUrl = "/products/crunch-bar.jpg" },
            new Product { Id = 36, Name = "FitCrunch Chocolate Brownie", Category = "Protein", SubCategory = "Bar", Weight = "46g", Price = 3, Description = "Chocolate brownie protein bar.", ImageUrl = "/products/fitcrunch-brownie.jpg" },

            // ===================== GAINERS / CARBS =====================
            new Product { Id = 16, Name = "Critical Mass Gainer", Category = "Gainer", SubCategory = "Mass", Weight = "6 kg", Price = 75, Description = "High calorie.", ImageUrl = "/products/critical-mass.jpg" },
            new Product { Id = 17, Name = "Hard Mass Gainer", Category = "Gainer", SubCategory = "Mass", Weight = "12 lbs", Price = 80, Description = "All in one.", ImageUrl = "/products/hard-mass-gainer.jpg" },
            new Product { Id = 18, Name = "Carbox Carb Powder", Category = "Carbs", SubCategory = "Carb", Weight = "1 kg", Price = 30, Description = "Fast carbs.", ImageUrl = "/products/carbox.jpg" },

            // ===================== CREATINE =====================
            new Product { Id = 19, Name = "Nutrex Creatine", Category = "Creatine", SubCategory = "Monohydrate", Weight = "300g", Price = 25, Description = "Micronized.", ImageUrl = "/products/nutrex-creatine.jpg" },
            new Product { Id = 20, Name = "Scitec Creatine", Category = "Creatine", SubCategory = "Monohydrate", Weight = "300g", Price = 27, Description = "Pure creatine.", ImageUrl = "/products/scitec-creatine.jpg" },
            new Product { Id = 21, Name = "Creatine for Women", Category = "Creatine", SubCategory = "Blend", Weight = "330g", Price = 29, Description = "Beauty blend.", ImageUrl = "/products/nutrex-creatine-women.jpg" },
            new Product { Id = 22, Name = "Challenger Creatine", Category = "Creatine", SubCategory = "Monohydrate", Weight = "300g", Price = 24, Description = "Strength.", ImageUrl = "/products/challenger-creatine.jpg" },

            // ===================== AMINO =====================
            new Product { Id = 23, Name = "Nutrex EAA Hydration", Category = "Amino", SubCategory = "EAA", Weight = "390g", Price = 35, Description = "Hydration.", ImageUrl = "/products/nutrex-eaa-hydration.jpg" },
            new Product { Id = 24, Name = "Nutritech Amino Boost", Category = "Amino", SubCategory = "Amino", Weight = "400g", Price = 32, Description = "Recovery.", ImageUrl = "/products/nutritech-amino-boost.jpg" },
            new Product { Id = 41, Name = "Rule One BCAA", Category = "Amino", SubCategory = "BCAA", Weight = "30 Servings", Price = 32, Description = "BCAA recovery support.", ImageUrl = "/products/bcaa-rule1.jpg" },
            new Product { Id = 47, Name = "Nutrex L-Arginine", Category = "Amino", SubCategory = "Nitric Oxide", Weight = "120 Capsules", Price = 27, Description = "Pump support.", ImageUrl = "/products/1-arginine.jpg" },

            // ===================== VITAMINS =====================
            new Product { Id = 25, Name = "Quamtrax ZMA", Category = "Vitamins", SubCategory = "ZMA", Weight = "90 Capsules", Price = 20, Description = "ZMA.", ImageUrl = "/products/quamtrax-zma.jpg" },
            new Product { Id = 26, Name = "Rule One Fish Oil", Category = "Vitamins", SubCategory = "Fish Oil", Weight = "100 Softgels", Price = 18, Description = "Omega 3.", ImageUrl = "/products/ruleone-fish-oil.jpg" },
            new Product { Id = 27, Name = "MuscleTech Multivitamin", Category = "Vitamins", SubCategory = "Multivitamin", Weight = "90 Tablets", Price = 22, Description = "Daily vitamins.", ImageUrl = "/products/muscletech-platinum-multivitamin.jpg" },
            new Product { Id = 28, Name = "Men's Multivitamin", Category = "Vitamins", SubCategory = "Multivitamin", Weight = "90 Tablets", Price = 24, Description = "Men health.", ImageUrl = "/products/mens-multi.jpg" },
            new Product { Id = 29, Name = "Women's Multivitamin", Category = "Vitamins", SubCategory = "Multivitamin", Weight = "60 Tablets", Price = 24, Description = "Women health.", ImageUrl = "/products/womens-multi.jpg" },

            // ===================== HEALTH =====================
            new Product { Id = 30, Name = "Organic Moringa Powder", Category = "Health", SubCategory = "Superfood", Weight = "200g", Price = 19, Description = "Organic moringa.", ImageUrl = "/products/organic-moringa.jpg" },
            new Product { Id = 31, Name = "Tongkat Ali", Category = "Health", SubCategory = "Herbal", Weight = "120 Capsules", Price = 28, Description = "Testosterone.", ImageUrl = "/products/tongkat-ali.jpg" },
            new Product { Id = 32, Name = "Ashwagandha", Category = "Health", SubCategory = "Herbal", Weight = "60 Capsules", Price = 26, Description = "Stress.", ImageUrl = "/products/ashwagandha.jpg" },
            new Product { Id = 37, Name = "Allmax CLA", Category = "Health", SubCategory = "Fat Support", Weight = "150 Softgels", Price = 28, Description = "CLA fat support.", ImageUrl = "/products/cla.jpg" },
            new Product { Id = 38, Name = "Glutamine", Category = "Health", SubCategory = "Recovery Support", Weight = "390g", Price = 28, Description = "Recovery support.", ImageUrl = "/products/glutamine.jpg" },
            new Product { Id = 39, Name = "HMB", Category = "Health", SubCategory = "Muscle Support", Weight = "30 Servings", Price = 28, Description = "Muscle supplement.", ImageUrl = "/products/biotech-hmb.jpg" },
            new Product { Id = 42, Name = "Berberine", Category = "Health", SubCategory = "Metabolism", Weight = "60 Capsules", Price = 25, Description = "Metabolic support.", ImageUrl = "/products/berberine.jpg" },
            new Product { Id = 51, Name = "Ultra Collagen Grass Fed Peptides", Category = "Health", SubCategory = "Collagen", Weight = "198 g", Price = 34, Description = "Collagen support.", ImageUrl = "/products/ultra-collagen.jpg" },

            // ===================== HYDRATION =====================
            new Product { Id = 40, Name = "Rule One 1LYTES", Category = "Hydration", SubCategory = "Electrolytes", Weight = "40 Servings", Price = 28, Description = "Electrolyte hydration.", ImageUrl = "/products/1lytes.jpg" },

            // ===================== FAT BURNER =====================
            new Product { Id = 43, Name = "Nutrex Lipo-6 Black", Category = "Fat Burner", SubCategory = "Thermogenic", Weight = "60 Capsules", Price = 38, Description = "Thermogenic fat burner.", ImageUrl = "/products/lipo6-black.jpg" },
            new Product { Id = 48, Name = "BPI Sports CLA", Category = "Fat Burner", SubCategory = "CLA", Weight = "150 Softgels", Price = 28, Description = "Body composition.", ImageUrl = "/products/bpi-cla.jpg" },

            // ===================== PRE-WORKOUT =====================
            new Product { Id = 44, Name = "Redcon1 Total War", Category = "Pre-Workout", SubCategory = "High Stim", Weight = "30 Servings", Price = 45, Description = "High energy.", ImageUrl = "/products/total-war.jpg" },
            new Product { Id = 45, Name = "Redcon1 Total War (Alt)", Category = "Pre-Workout", SubCategory = "High Stim", Weight = "30 Servings", Price = 45, Description = "Alternate flavor.", ImageUrl = "/products/total-wr.jpg" },
            new Product { Id = 49, Name = "Moons Truck Zero", Category = "Pre-Workout", SubCategory = "Stim Free", Weight = "30 Servings", Price = 42, Description = "Stim-free pump.", ImageUrl = "/products/moons-truck-zero.jpg" },
            new Product { Id = 50, Name = "C4 Original Pre-Workout", Category = "Pre-Workout", SubCategory = "Energy & Focus", Weight = "50 Servings", Price = 45, Description = "Energy & focus.", ImageUrl = "/products/C4.jpg" }

            );
        }
    }
}