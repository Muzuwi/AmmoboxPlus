using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Projectiles
{
    public class RocketIce : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Rocket");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 16;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.alpha = 1;
            Projectile.timeLeft = 600;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int damage = hit.Damage;
            float knockback = hit.Knockback;
            bool crit = hit.Crit;

            //  TODO: I have to rewrite this someday

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
                // FIXME: Liquid etc etd
                if (Main.tile[posXlow, posYlow].LiquidAmount > 0 || Main.tile[posXhi, posYhi].LiquidAmount > 0)
                {
                    if (WorldGen.genRand.Next(3) == 0)
                    {
                        processAddBuffIce(ref target, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }
                else
                {
                    int temp = WorldGen.genRand.Next(6);
                    if (temp == 0 || temp == 1 || temp == 2 || temp == 3)
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

        public override void AI()
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher)
            {
                Projectile.velocity = Projectile.oldVelocity;
                if (Math.Abs(Projectile.velocity.X) < 15f && Math.Abs(Projectile.velocity.Y) < 15f) Projectile.velocity *= 1.1f;
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }

            //  Grenade launcher
            if (shotFrom == ItemID.GrenadeLauncher)
            {
                //Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                //  If going to explode 
                if (Projectile.ai[1] == 200)
                {
                    Projectile.Kill();
                }
                else
                {
                    Projectile.ai[1] += 1;
                }
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher)
            {
                if (Projectile.ai[1] < 3)
                {
                    Projectile.velocity *= 0.98f;
                }
                if (Projectile.ai[1] >= 3 && Projectile.alpha < 150)
                {
                    Projectile.alpha += 1;
                }

            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon)
            {
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                AmmoboxHelpfulMethods.chaseEnemy(Projectile.identity, Projectile.type);
            }

            //  Common for all launchers
            for (int i = 0; i < 1; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, newColor: Color.WhiteSmoke);
            }
            Lighting.AddLight(Projectile.Center, Color.SkyBlue.ToVector3());

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            //  Rocket launcher
            if (shotFrom == ItemID.RocketLauncher)
            {
                Projectile.Kill();
            }

            //  Proximity mine
            if (shotFrom == ItemID.ProximityMineLauncher)
            {
                if (Projectile.ai[1] > 3)
                {
                    Projectile.velocity = Vector2.Zero;
                }
                else
                {
                    Projectile.ai[1] += 1;
                }
            }

            //  Snowman
            if (shotFrom == ItemID.SnowmanCannon)
            {
                Projectile.Kill();
            }

            return true;
        }

        public override void Kill(int timeLeft)
        {
            int shotFrom = Projectile.GetGlobalProjectile<AmmoboxGlobalProjectile>().apShotFromLauncherID;

            AmmoboxHelpfulMethods.explodeRocket(shotFrom, Projectile.identity, Projectile.type, largeBlast: true);
        }

        public void processAddBuffIce(ref NPC npc, int type, int time)
        {
            //  If multipart enemy
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