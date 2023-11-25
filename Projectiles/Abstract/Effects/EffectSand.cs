using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectSand : IEffect
    {
        public static EffectSand Instance = new EffectSand();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                targetNpc.AddBuff(ModContent.BuffType<Buffs.CloudedVision>(), 300);
            }
            else
            {
                var packet = cause.Mod.GetPacket();
                int buffType = ModContent.BuffType<Buffs.CloudedVision>();
                packet.Write((byte)AmmoboxMsgType.AmmoboxClouded);
                packet.Write(targetNpc.whoAmI);
                packet.Write(buffType);
                packet.Write(300);
                packet.Send();
            }

        }
    }
}
