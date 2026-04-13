using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchMagicBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Mage Empowerment");
		//Description.SetDefault("1.3x Magic damage, magic weapons cost no mana");
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetDamage(DamageClass.Magic) *= 1.3f;
		player.manaCost += -1f;
		int num = Dust.NewDust(player.position, player.width, player.height, DustID.GemEmerald);
		Main.dust[num].scale = 1.2f;
		Main.dust[num].velocity *= 3f;
		Main.dust[num].noGravity = true;
	}
}
