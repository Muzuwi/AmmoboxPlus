using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons {
    public class BulletDrugged : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Drugged Bullet");
            Tooltip.SetDefault("Makes enemies attack other enemies");
        }

        public override void SetDefaults() {
            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f; 
            item.value = 50; 
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("BulletDrugged");
            item.shootSpeed = 2f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }

}
