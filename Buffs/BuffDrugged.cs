using Terraria;
using AmmoboxPlus.NPCs;

namespace AmmoboxPlus.Buffs
{
    public class Drugged : ABBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Drugged");
            // Description.SetDefault("Damage enemies nearby");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //npc.GetGlobalNPC<AmmoboxGlobalNPC>().apDruggedDamage = npc.damage;
            npc.GetGlobalNPC<AmmoboxGlobalNPC>().apDrugged = true;
        }
    }
}