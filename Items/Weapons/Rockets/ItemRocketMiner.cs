using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class RocketMiner : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Miner's Rocket");
            // Tooltip.SetDefault("Large blast radius. Will destroy tiles.\nEnemies have a chance to drop random amounts of ore.\nMining ore has a chance to drop extra ore");
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
            Item.shootSpeed = 2f;
            Item.shoot = Mod.Find<ModProjectile>("RocketMiner").Type;
            Item.ammo = AmmoID.Rocket;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.Dynamite, 1);
            recipe.AddIngredient(ItemID.RocketII, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}