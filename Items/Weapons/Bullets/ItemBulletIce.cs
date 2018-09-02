using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletIce : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice Bullet");
            Tooltip.SetDefault("Applies 'Cold' debuff (Like 'Chilled')\nHas a chance to freeze enemies in place.\nChance to freeze is doubled when enemy is in water.");
        }

        public override void SetDefaults() {
            item.damage = 13;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 10; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("BulletIce");
            item.shootSpeed = 5f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 75);
            recipe.SetResult(this, 75);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }

}
