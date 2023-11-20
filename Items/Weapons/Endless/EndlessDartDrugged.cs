using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class EndlessDartDrugged : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Endless Drugged Pouch");
            // Tooltip.SetDefault("Creates an aura around an enemy that damages nearby enemies");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 1;
            Item.knockBack = 2f;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("DartDrugged").Type;
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("DartDrugged").Type, 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
