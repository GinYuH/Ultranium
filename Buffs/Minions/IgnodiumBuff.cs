using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class IgnodiumBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Ignodium");
		// ((ModBuff)this).Description.SetDefault("The mini ignodium will fight with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("Ignodium").Type] > 0)
		{
			modPlayer.IgnodiumMinion = true;
		}
		if (!modPlayer.IgnodiumMinion)
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
