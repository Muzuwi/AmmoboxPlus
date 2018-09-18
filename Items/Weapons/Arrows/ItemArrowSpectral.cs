using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowSpectral : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Phantasmal Arrow");
            Tooltip.SetDefault("Penetrates walls up to 4 blocks.\nAccuracy decreases significantly with each penetrated block.");
        }

        public override void SetDefaults() {
            item.damage = 14;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 15; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("ArrowSpectral");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 150);
            recipe.AddRecipe();
        }
    }

}
