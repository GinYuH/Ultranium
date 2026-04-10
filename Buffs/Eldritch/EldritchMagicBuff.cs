using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchMagicBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Eldritch Mage Empowerment");
		// ((ModBuff)this).Description.SetDefault("1.3x Magic damage, magic weapons cost no mana");
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Magic) *= 1.3f;
		player.manaCost += -1f;
		int num = Dust.NewDust(player.position, player.width, player.height, 89);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
