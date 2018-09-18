using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletMarked : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Markershot");
            Tooltip.SetDefault("Inflicts 'Marked for Death'.\nInflicted enemies receive 15% more damage.");
        }

        public override void SetDefaults() {
            item.damage = 2;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 250; 
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("BulletMarked");
            item.shootSpeed = 10f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 1);
            recipe.SetResult(this, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }

}
