using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class DreadDebuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Dread");
		((ModBuff)this).Description.SetDefault("Pure fear pulses through your body...\nReduced defense and attack damage");
		Main.debuff[((ModBuff)this).Type] = true;
		Main.pvpBuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
		base.longerExpertDebuff = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		if (!npc.friendly)
		{
			Dust.NewDust(npc.position, npc.width, npc.height, 90);
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			npc.lifeRegen -= 16;
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.lifeRegen = 0;
		player.statDefense -= 10;
		player.magicDamage *= 0.9f;
		player.meleeDamage *= 0.9f;
		player.rangedDamage *= 0.9f;
		player.minionDamage *= 0.9f;
		player.thrownDamage *= 0.9f;
		int num = Dust.NewDust(player.position, player.width, player.height, 90);
		Main.dust[num].scale = 1f;
		Main.dust[num].velocity *= 1f;
		Main.dust[num].noGravity = true;
	}
}
