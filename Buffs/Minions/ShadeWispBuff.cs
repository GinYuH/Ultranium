using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class ShadeWispBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Shade Wisp");
		// ((ModBuff)this).Description.SetDefault("The Shade Wisp will fight along with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("ShadeWisp").Type] > 0)
		{
			modPlayer.ShadeWisp = true;
		}
		if (!modPlayer.ShadeWisp)
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
