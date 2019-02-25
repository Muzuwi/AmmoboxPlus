using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Peculiar Pouch");
            Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f; 
            item.value = 25;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Dart;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartBunny"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
