using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectIchor : IEffect
    {
        public static EffectIchor Instance = new EffectIchor();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (!Main.rand.NextBool(2))
            {
                return;
            }

            targetNpc.AddBuff(BuffID.Ichor, 240);
        }
    }
}
