using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Items.Weapons
{
    class Marine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Muzuwi's Marine");
            // Tooltip.SetDefault("It gives off an aura of Doom\nThank you for supporting Ammobox+!");
            Item.autoReuse = true;
            Item.knockBack = 7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.width = 50;
            Item.height = 14;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item38;
            Item.damage = 29;
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.value = 700000;
            Item.expert = true;
            Item.expertOnly = false;
            Item.DamageType = DamageClass.Ranged;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 6; i++)
            {
                velocity.X += Main.rand.Next(-40, 41) * 0.05f;
                velocity.Y += Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }

    }
}
