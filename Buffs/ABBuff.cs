using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AmmoboxPlus.Buffs
{
    public abstract class ABBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
}