using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchSummonBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Eldritch Summon Empowerment");
		// ((ModBuff)this).Description.SetDefault("1.5x summon damage, abyssal eye is empowered");
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Summon) *= 1.5f;
		player.GetModPlayer<UltraniumPlayer>().EldritchSummonBuff = true;
		int num = Dust.NewDust(player.position, player.width, player.height, 89);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
