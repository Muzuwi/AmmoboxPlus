using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class DartCactus : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cactus Dart");
            // Tooltip.SetDefault("Spikey!\nApplies a thorns-like effect that hurts other enemies.");
        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 10;
            Item.rare = ItemRarityID.White;
            Item.shoot = Mod.Find<ModProjectile>("DartCactus").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

}
