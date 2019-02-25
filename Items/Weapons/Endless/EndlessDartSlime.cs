using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessDartSlime : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Slime Pouch");
            Tooltip.SetDefault("Slows down shot enemies.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f; 
            item.value = 15;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartSlime");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DartSlime"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
