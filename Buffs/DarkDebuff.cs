using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class DarkDebuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Decay");
		//Description.SetDefault("Nullified life regeneration and decreased damage\nRapidly loosing life");
		Main.debuff[((ModBuff)this).Type] = true;
		Main.pvpBuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = true;
    }

	public override void Update(NPC npc, ref int buffIndex)
	{
		if (!npc.friendly)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			npc.lifeRegen -= 25;
			Dust.NewDust(npc.position, npc.width, npc.height, DustID.GemEmerald);
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.lifeRegen = 0;
		player.GetDamage(DamageClass.Magic) *= 0.85f;
		player.GetDamage(DamageClass.Melee) *= 0.85f;
		player.GetDamage(DamageClass.Ranged) *= 0.85f;
		player.GetDamage(DamageClass.Summon) *= 0.85f;
		player.GetModPlayer<UltraniumPlayer>().DarkDebuff = true;
		int num = Dust.NewDust(player.position, player.width, player.height, DustID.GemEmerald);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
