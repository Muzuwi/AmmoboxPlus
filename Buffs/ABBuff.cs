using Terraria;
using Terraria.ModLoader;
using AmmoboxPlus.NPCs;
using Terraria.ID;

namespace AmmoboxPlus.Buffs
{
    public class ABBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactus = true;
            //npc.GetGlobalNPC<AmmoboxGlobalNPC>().apCactusDamage = npc.damage;
        }
    }
}