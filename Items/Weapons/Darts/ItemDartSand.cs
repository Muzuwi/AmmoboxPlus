using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartSand : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sandy Dart");
            Tooltip.SetDefault("Applies 'Clouded Vision'\nEnemies have a very low chance to not hit you.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 15; 
            item.rare = ItemRarityID.White;
            item.shoot = mod.ProjectileType("DartSand");
            item.shootSpeed = 3f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
