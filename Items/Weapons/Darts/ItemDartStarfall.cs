using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartStarfall : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Starfall Dart");
            Tooltip.SetDefault("Has a low chance of turning into an enemy-piercing star.");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 15; 
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("DartStarfall");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
