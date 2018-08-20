using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletBunny : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Peculiar Bullet");
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
            item.shoot = mod.ProjectileType("BulletBunny");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bunny, 10);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
