using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class BabySquidBuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Baby Squid");
		((ModBuff)this).Description.SetDefault("The Baby Squid will fight for you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).mod.ProjectileType("BabySquid")] > 0)
		{
			modPlayer.BabySquid = true;
		}
		if (!modPlayer.BabySquid)
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
