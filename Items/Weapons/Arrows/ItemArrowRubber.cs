using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class ArrowRubber : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rubber Arrow");
            // Tooltip.SetDefault("Very strong knockback, perfect for crowd control.");
        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 10f;
            Item.value = 15;
            Item.rare = ItemRarityID.White;
            Item.shoot = Mod.Find<ModProjectile>("ArrowRubber").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddIngredient(ItemID.PinkGel, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

}
