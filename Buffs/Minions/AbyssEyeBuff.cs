using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class AbyssEyeBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Monolith");
		//Description.SetDefault("The monolith of the abyss watches over you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("AbyssalEye").Type] > 0)
		{
			modPlayer.AbyssalEye = true;
		}
		if (!modPlayer.AbyssalEye)
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
