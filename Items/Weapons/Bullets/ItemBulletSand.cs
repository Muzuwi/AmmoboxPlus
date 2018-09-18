using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletSand : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sand Bullet");
            Tooltip.SetDefault("Inflicts 'Clouded Vision'\nEnemies have a very low chance to miss their attacks.");
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
            item.shoot = mod.ProjectileType("BulletSand");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.SetResult(this, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.AddRecipe();
        }
    }

}
