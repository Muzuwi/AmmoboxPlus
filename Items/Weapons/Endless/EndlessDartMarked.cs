using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class EndlessDartMarked : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Endless Marker Pouch");
            // Tooltip.SetDefault("Inflicts 'Marked for Death'.\nInflicted enemies receive 15% more damage.");
        }

        public override void SetDefaults()
        {
            Item.damage = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 1;
            Item.knockBack = 1f;
            Item.value = 250;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = Mod.Find<ModProjectile>("DartMarked").Type;
            Item.shootSpeed = 9f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod.Find<ModItem>("DartMarked").Type, 50);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
