using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Minions;

public class BloodBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Bloody Dripper");
		Description.SetDefault("The Bloody Dripper will fight along with you");
		Main.buffNoSave[((ModBuff)this).Type] = true;
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("BloodMinion").Type] > 0)
		{
			modPlayer.BloodMinion = true;
		}
		if (!modPlayer.BloodMinion)
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
