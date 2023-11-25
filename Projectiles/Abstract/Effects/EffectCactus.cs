using AmmoboxPlus.Globals;
using AmmoboxPlus.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectCactus : IEffect
    {
        public static EffectCactus Instance = new EffectCactus();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            var globalNpc = targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>();
            if (globalNpc.apDrugged)
            {
                return;
            }

            globalNpc.apCactus = true;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                targetNpc.AddBuff(ModContent.BuffType<Buffs.Cactus>(), 300);
            }
            else
            {
                var packet = cause.Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.Cactus>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxCactus);
                packet.Write(targetNpc.whoAmI);
                packet.Write(buffType);
                packet.Write(300);
                packet.Send();
            }
        }
    }
}
