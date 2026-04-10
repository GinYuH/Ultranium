using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchRangedBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Eldritch Ranger Empowerment");
		// ((ModBuff)this).Description.SetDefault("1.3x Ranged damage, 25% chance to not consume ammo");
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Ranged) *= 1.3f;
		player.ammoCost75 = true;
		int num = Dust.NewDust(player.position, player.width, player.height, 89);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
