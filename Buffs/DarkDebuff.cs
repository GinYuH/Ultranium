using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class DarkDebuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Eldritch Decay");
		((ModBuff)this).Description.SetDefault("Nullified life regeneration and decreased damage\nRapidly loosing life");
		Main.debuff[((ModBuff)this).Type] = true;
		Main.pvpBuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
		base.longerExpertDebuff = true;
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
			Dust.NewDust(npc.position, npc.width, npc.height, 89);
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.lifeRegen = 0;
		player.magicDamage *= 0.85f;
		player.meleeDamage *= 0.85f;
		player.rangedDamage *= 0.85f;
		player.minionDamage *= 0.85f;
		player.GetModPlayer<UltraniumPlayer>().DarkDebuff = true;
		int num = Dust.NewDust(player.position, player.width, player.height, 89);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
