using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class DartCactus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cactus Dart");
            Tooltip.SetDefault("Spikey!\nApplies a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults() {
            item.damage = 5;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("DartCactus");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 10);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
