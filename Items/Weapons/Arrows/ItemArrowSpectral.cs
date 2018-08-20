using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class ArrowSpectral : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Spectral Arrow");
            Tooltip.SetDefault("Penetrates walls up to 4 blocks.\nAccuracy decreases significantly with each penetrated block.");
        }

        public override void SetDefaults() {
            item.damage = 20;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 50; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("ArrowSpectral");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 1);         //  TODO: CHANGE
            recipe.AddIngredient(ItemID.EmptyBullet, 100);      //  TODO: CHANGE
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
