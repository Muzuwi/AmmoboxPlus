using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    public class BulletMiner : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Miner's Bullet");
            // Tooltip.SetDefault("Has a low chance of dropping a random amount of ore from enemies");
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
            Item.rare = ItemRarityID.Orange;
            Item.shoot = Mod.Find<ModProjectile>("BulletMiner").Type;
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ItemID.Dynamite, 20);
            recipe.AddIngredient(ItemID.EmptyBullet, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

}
