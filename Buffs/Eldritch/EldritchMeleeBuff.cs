using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchMeleeBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Warrior Empowerment");
		//Description.SetDefault("1.5x melee damage, however you take 1.3x damage");
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Melee) *= 1.5f;
		player.GetModPlayer<UltraniumPlayer>().damageTaken *= 1.3f;
		int num = Dust.NewDust(player.position, player.width, player.height, 89);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
