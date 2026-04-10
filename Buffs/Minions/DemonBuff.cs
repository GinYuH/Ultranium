using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class DemonBuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Shade Demon");
		((ModBuff)this).Description.SetDefault("The Shade Demon will fight for you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).mod.ProjectileType("DemonMinion")] > 0)
		{
			modPlayer.DemonMinion = true;
		}
		if (!modPlayer.DemonMinion)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}
