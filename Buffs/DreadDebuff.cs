using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class DreadDebuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread");
		//Description.SetDefault("Pure fear pulses through your body...\nReduced defense and attack damage");
		Main.debuff[((ModBuff)this).Type] = true;
		Main.pvpBuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = true;
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
		player.GetDamage(DamageClass.Magic) *= 0.9f;
		player.GetDamage(DamageClass.Melee) *= 0.9f;
		player.GetDamage(DamageClass.Ranged) *= 0.9f;
		player.GetDamage(DamageClass.Summon) *= 0.9f;
		player.GetDamage(DamageClass.Throwing) *= 0.9f;
		int num = Dust.NewDust(player.position, player.width, player.height, 90);
		Main.dust[num].scale = 1f;
		Main.dust[num].velocity *= 1f;
		Main.dust[num].noGravity = true;
	}
}
