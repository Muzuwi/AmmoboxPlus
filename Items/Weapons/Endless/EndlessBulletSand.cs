using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class EndlessBulletSand : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Endless Sand Magazine");
            Tooltip.SetDefault("Inflicts 'Clouded Vision'\nEnemies have a very low chance to miss their attacks.");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 1;
            item.knockBack = 2f; 
            item.value = 15;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("BulletSand");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BulletSand"), 3996);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddRecipe();
        }
    }
}
