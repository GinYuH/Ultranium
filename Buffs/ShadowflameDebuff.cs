using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class ShadowflameDebuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = false;
		//DisplayName.SetDefault("Shadowflame");
		//Description.SetDefault("Losing life");
		Main.pvpBuff[((ModBuff)this).Type] = false;
		Main.debuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
		BuffID.Sets.LongerExpertDebuff[Type] = false;
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
			Dust.NewDust(player.position, player.width, player.height, DustID.Shadowflame);
		}
	}
}
