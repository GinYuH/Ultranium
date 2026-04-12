using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class DreadMinionBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Mini Dread");
		Description.SetDefault("The small dread will fight with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("DreadMinion").Type] > 0)
		{
			modPlayer.DreadMinion = true;
		}
		if (!modPlayer.DreadMinion)
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
