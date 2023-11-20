using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class DartSlime : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime Dart");
            // Tooltip.SetDefault("Slows down shot enemies.");
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
            Item.shoot = Mod.Find<ModProjectile>("DartSlime").Type;
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Dart;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddRecipeGroup("IronBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }

}
