using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class WispBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ethereal Wisp");
		Description.SetDefault("The Ethereal Wisp will fight along with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("EtherealWisp").Type] > 0)
		{
			modPlayer.Wisp = true;
		}
		if (!modPlayer.Wisp)
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
