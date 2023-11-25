using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectIce : IEffect
    {
        public static EffectIce Instance = new EffectIce();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            //  Check if enemy is in water
            //  Horrible way to actually do it, might not get all edge cases
            int posXlow = (int)targetNpc.position.X / 16;
            int posYlow = (int)targetNpc.position.Y / 16;
            int posXhi = ((int)targetNpc.position.X + targetNpc.width) / 16;
            int posYhi = ((int)targetNpc.position.Y + targetNpc.height) / 16;

            //  Have we reached Stuck limit?
            if (targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apStuckLimit)
            {

                if (AmmoboxPlus.isEnemyBlacklisted(targetNpc.type)) return;

                //  If we have more parts in the chain, apply to the rest
                if (targetNpc.realLife != -1)
                {
                    Main.npc[targetNpc.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc)
                    {
                        if (n.realLife == targetNpc.realLife)
                        {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else
                {
                    targetNpc.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
            else
            { //  No stuck limit

                if (AmmoboxPlus.isEnemyBlacklisted(targetNpc.type)) return;

                //  If enemy is in water, apply higher chance
                // FIXME: Check liquid type
                if (Main.tile[posXlow, posYlow].LiquidAmount > 0 || Main.tile[posXhi, posYhi].LiquidAmount > 0)
                {
                    if (WorldGen.genRand.Next(50) == 0)
                    {
                        AddIceBuff(ref targetNpc, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }
                else
                {
                    if (WorldGen.genRand.Next(100) == 0)
                    {
                        AddIceBuff(ref targetNpc, ModContent.BuffType<Buffs.Stuck>(), 300);
                    }
                }

                //  If multi-part enemy
                if (targetNpc.realLife != -1)
                {
                    Main.npc[targetNpc.realLife].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                    int index = 0;
                    foreach (NPC n in Main.npc)
                    {
                        if (n.realLife == targetNpc.realLife)
                        {
                            Main.npc[index].AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                        }
                        ++index;
                    }
                }
                else
                {
                    targetNpc.AddBuff(ModContent.BuffType<Buffs.Cold>(), 500);
                }
            }
        }

        private void AddIceBuff(ref NPC npc, int type, int time)
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
            SoundEngine.PlaySound(style, npc.position);
        }
    }
}
