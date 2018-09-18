using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletSpectral : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Phantasmal Bullet");
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
            item.shoot = mod.ProjectileType("BulletSpectral");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.SetResult(this, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }

}
