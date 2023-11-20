using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class BulletRubber : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rubber Bullet");
            // Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 10f;
            Item.value = 25;
            Item.rare = ItemRarityID.Green;
            Item.shoot = Mod.Find<ModProjectile>("BulletRubber").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(300);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddIngredient(ItemID.PinkGel, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

}
