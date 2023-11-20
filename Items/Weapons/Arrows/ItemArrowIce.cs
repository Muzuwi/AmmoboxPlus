using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class ArrowIce : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Arrow");
            // Tooltip.SetDefault("Applies 'Cold' debuff (Like 'Chilled')\nHas a chance to freeze enemies in place.\nChance to freeze is doubled when enemy is in water.");
        }

        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 20;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("ArrowIce").Type;
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(75);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

}
