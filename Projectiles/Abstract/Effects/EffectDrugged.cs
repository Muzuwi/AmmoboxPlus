using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectDrugged : IEffect
    {
        public static EffectDrugged Instance = new EffectDrugged();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            // FIXME: Could be expanded..
            if (targetNpc.type == NPCID.TargetDummy)
            {
                return;
            }

            if (targetNpc.realLife != -1)
            {
                //  Apply only to the parent
                if (Main.npc[targetNpc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus)
                {
                    Main.npc[targetNpc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0)
                    { //  Singleplayer
                        Main.npc[targetNpc.realLife].DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    }
                    else
                    { //  Sync with others
                        var packet = cause.Mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(Main.npc[targetNpc.realLife].whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (Main.npc[targetNpc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0)
                {
                    Main.npc[targetNpc.realLife].GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 1000;
                    Main.npc[targetNpc.realLife].AddBuff(ModContent.BuffType<Buffs.Drugged>(), 500);
                }

            }
            else
            {
                //  Normal enemy, do things as usual
                if (targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus)
                {
                    targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = false;
                    if (Main.netMode == 0)
                    { //  Singleplayer
                        targetNpc.DelBuff(ModContent.BuffType<Buffs.Cactus>());
                    }
                    else
                    { //  Sync with others
                        var packet = cause.Mod.GetPacket();
                        int buffType = ModContent.BuffType<Buffs.Cactus>();
                        packet.Write((byte)AmmoboxMsgType.AmmoboxDelBuff);
                        packet.Write(targetNpc.whoAmI);
                        packet.Write(buffType);
                        packet.Send();
                    }
                }

                if (targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown == 0)
                {
                    targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedCooldown = 1000;
                    targetNpc.AddBuff(ModContent.BuffType<Buffs.Drugged>(), 500);
                }
            }
        }
    }
}
