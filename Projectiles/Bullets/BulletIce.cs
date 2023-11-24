using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class BulletIce : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.scale = 2f;
            Projectile.spriteDirection = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;


            //  Check if enemy is in water
            //  Horrible way to actually do it, might not get all edge cases
            int posXlow = (int)target.position.X / 16;
            int posYlow = (int)target.position.Y / 16;
            int posXhi = ((int)target.position.X + target.width) / 16;
            int posYhi = ((int)target.position.Y + target.height) / 16;
            if (AmmoboxPlus.isEnemyBlacklisted(target.type)) return;


            //  Have we reached Stuck limit?
            if (target.GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit)
            {

                //  If we have more parts in the chain, apply to the rest
                if (target.realLife != -1)
                {
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc)
                    {
                        if (n.realLife == target.realLife)
                        {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
            else
            { //  No stuck limit

                //  If enemy is in water, apply higher chance
                // FIXME: Actually check if it's water lol
                if (Main.tile[posXlow, posYlow].LiquidAmount > 0 || Main.tile[posXhi, posYhi].LiquidAmount > 0)
                {
                    if (WorldGen.genRand.Next(50) == 0)
                    {
                        processAddBuffIce(ref target, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }
                else
                {
                    if (WorldGen.genRand.Next(100) == 0)
                    {
                        processAddBuffIce(ref target, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }

                //  If multi-part enemy
                if (target.realLife != -1)
                {
                    Main.npc[target.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc)
                    {
                        if (n.realLife == target.realLife)
                        {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(Projectile.Top, Color.SkyBlue.ToVector3());
        }

        public void processAddBuffIce(ref NPC npc, int type, int time)
        {
            //  If multi part enemy
            if (npc.realLife != -1)
            {
                Main.npc[npc.realLife].AddBuff(type, time);
                Main.npc[npc.realLife].velocity = new Vector2(0, 0);
                Main.npc[npc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;

                int index = 0;
                foreach (NPC n in Main.npc)
                {
                    if (n.realLife == npc.realLife)
                    {
                        Main.npc[index].AddBuff(type, time);
                        Main.npc[index].velocity = new Vector2(0, 0);
                        Main.npc[index].GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;
                    }
                    ++index;
                }
            }
            else
            {
                npc.AddBuff(type, time);
                npc.velocity = new Vector2(0, 0);
                npc.GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit = true;
            }
            SoundStyle style = new SoundStyle("AmmoboxPlus/Sounds/Custom/iceBullet");
            SoundEngine.PlaySound(style, Projectile.position);
        }
    }
}