using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class ShadowflameDebuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = false;
		// ((ModBuff)this).DisplayName.SetDefault("Shadowflame");
		// ((ModBuff)this).Description.SetDefault("Losing life");
		Main.pvpBuff[((ModBuff)this).Type] = false;
		Main.debuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
		base.longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.lifeRegen > 0)
		{
			player.lifeRegen = 0;
		}
		player.lifeRegen -= 12;
		if (Utils.NextBool(Main.rand, 4))
		{
			Dust.NewDust(player.position, player.width, player.height, 27);
		}
	}
}
