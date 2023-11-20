using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class BulletSand : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Bullet");
            // Tooltip.SetDefault("Inflicts 'Clouded Vision'\nEnemies have a very low chance to miss their attacks.");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 15;
            Item.rare = ItemRarityID.White;
            Item.shoot = Mod.Find<ModProjectile>("BulletSand").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

}
