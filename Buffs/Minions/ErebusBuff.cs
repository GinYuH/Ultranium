using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class ErebusBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Mini Erebus");
		// ((ModBuff)this).Description.SetDefault("It looks like its hungry... for souls");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer ultraniumPlayer = (UltraniumPlayer)(object)player.GetModPlayer(((ModBuff)this).Mod, "UltraniumPlayer");
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("SmolErebusHead").Type] > 0)
		{
			ultraniumPlayer.ErebusMinion = true;
		}
		if (!ultraniumPlayer.ErebusMinion)
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
