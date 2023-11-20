using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class DartBunny : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Peculiar Dart");
            // Tooltip.SetDefault("Has a very low chance of turning an enemy into a bunny.");
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 25;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = Mod.Find<ModProjectile>("DartBunny").Type;
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.Bunny, 5);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

}
