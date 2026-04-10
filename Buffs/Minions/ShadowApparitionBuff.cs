using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class ShadowApparitionBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Shadowflame Apparition");
		// ((ModBuff)this).Description.SetDefault("The Shadowflame Apparition will fight for you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("ShadowApparition").Type] > 0)
		{
			modPlayer.ShadowApparition = true;
		}
		if (!modPlayer.ShadowApparition)
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
