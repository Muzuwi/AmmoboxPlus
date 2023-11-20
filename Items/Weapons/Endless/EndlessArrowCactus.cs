using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class EndlessArrowCactus : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Endless Cactus Quiver");
            // Tooltip.SetDefault("Spikey!\nInflicts a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 1;
            Item.knockBack = 2f;
            Item.value = 15;
            Item.rare = ItemRarityID.Green;
            Item.shoot = Mod.Find<ModProjectile>("ArrowCactus").Type;
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("ArrowCactus").Type, 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
