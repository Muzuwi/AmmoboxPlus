using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletRubber : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Rubber Bullet");
            Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 10f; 
            item.value = 25; 
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletRubber");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.PinkGel, 1);
            recipe.SetResult(this, 300);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }

}
