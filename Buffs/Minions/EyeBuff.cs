using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class EyeBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Mini Eye");
		Description.SetDefault("The servant of Cthulhu will fight for you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("EyeMinion").Type] > 0)
		{
			modPlayer.EyeMinion = true;
		}
		if (!modPlayer.EyeMinion)
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
