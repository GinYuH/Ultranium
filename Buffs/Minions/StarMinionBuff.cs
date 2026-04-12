using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class StarMinionBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Spacial Star");
		Description.SetDefault("The Spacial Star will fight for you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("StarMinion").Type] > 0)
		{
			modPlayer.StarMinion = true;
		}
		if (!modPlayer.StarMinion)
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
