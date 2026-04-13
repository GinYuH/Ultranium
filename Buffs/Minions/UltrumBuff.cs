using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class UltrumBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultrum");
		//Description.SetDefault("The mini ultrum will fight with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("Ultrum").Type] > 0)
		{
			modPlayer.UltrumMinion = true;
		}
		if (!modPlayer.UltrumMinion)
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
