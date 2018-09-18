using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletCactus : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cactus Bullet");
            Tooltip.SetDefault("Spikey!\nApplies a thorns-like effect that hurts other enemies.");
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
            item.shoot = mod.ProjectileType("BulletCactus");
            item.shootSpeed = 4f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.Cactus, 20);
            recipe.SetResult(this, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }

}
