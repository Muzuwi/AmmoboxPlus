using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class RocketIchor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ichor Rocket");
            // Tooltip.SetDefault("Small blast radius. Will not destroy tiles.\nInflicts 'Ichor'");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 14;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 15;
            Item.rare = ItemRarityID.Lime;
            Item.shootSpeed = 4f;
            Item.shoot = Mod.Find<ModProjectile>("RocketIchor").Type;
            Item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.Ichor, 5);
            recipe.AddIngredient(ItemID.RocketI, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}