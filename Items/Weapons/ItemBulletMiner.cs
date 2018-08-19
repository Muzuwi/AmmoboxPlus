using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletMiner : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Miner's Bullet");
            Tooltip.SetDefault("Has a low chance of dropping a random amount of ore from enemies");
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
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("BulletMiner");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Dynamite, 20);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
