using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletStarfall : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Starfall Bullet");
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
            item.value = 10; 
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("BulletStarfall");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(75, 10);  // Will fix later
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
