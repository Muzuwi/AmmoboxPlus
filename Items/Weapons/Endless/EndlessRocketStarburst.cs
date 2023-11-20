using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class EndlessRocketStarburst : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Endless Starburst Crate");
            // Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nSometimes explodes into a barrage of stars");
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 14;
            Item.maxStack = 1;
            Item.knockBack = 2f;
            Item.value = 15;
            Item.rare = ItemRarityID.Lime;
            Item.shootSpeed = 3f;
            Item.shoot = Mod.Find<ModProjectile>("RocketStarburst").Type;
            Item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("RocketStarburst").Type, 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
