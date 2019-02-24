using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartAcupuncture : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Acupuncture Pouch");
            Tooltip.SetDefault("Gets stronger with each hit.\nThe bonus is reset when you miss the enemy.");
        }

        public override void SetDefaults() {
            item.damage = 1;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f; 
            item.value = 15;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartAcupuncture");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartAcupuncture"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
