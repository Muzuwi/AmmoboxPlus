using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Peculiar Dart");
            Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults() {
            item.damage = 8;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 25; 
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("DartBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bunny, 5);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
