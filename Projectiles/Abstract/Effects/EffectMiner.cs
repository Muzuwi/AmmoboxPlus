using AmmoboxPlus.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace AmmoboxPlus.Projectiles.Abstract.Effects
{
    public class EffectMiner : IEffect
    {
        public static EffectMiner Instance = new EffectMiner();

        public void Proc(ModProjectile cause, NPC targetNpc)
        {
            if (targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre)
            {
                return;
            }

            targetNpc.GetGlobalNPC<AmmoboxGlobalNPC>().apAlreadyDroppedOre = AmmoboxHelpfulMethods.processMinerOreDrop(targetNpc, oneInX: 20);
        }
    }
}
