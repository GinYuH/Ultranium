using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class PumpBuff : ModBuff
{
	public override void SetDefaults()
	{
		((ModBuff)this).DisplayName.SetDefault("Pumpkin");
		((ModBuff)this).Description.SetDefault("The Living Pumpkin will fight for you!");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer ultraniumPlayer = (UltraniumPlayer)(object)player.GetModPlayer(((ModBuff)this).mod, "UltraniumPlayer");
		if (player.ownedProjectileCounts[((ModBuff)this).mod.ProjectileType("PumpSlime")] > 0)
		{
			ultraniumPlayer.PumpSlime = true;
		}
		if (!ultraniumPlayer.PumpSlime)
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
