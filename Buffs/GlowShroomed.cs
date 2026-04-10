using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class GlowShroomed : ModBuff
{
	public override void SetStaticDefaults()
	{
		// ((ModBuff)this).DisplayName.SetDefault("Glow Shroom'd");
		// ((ModBuff)this).Description.SetDefault("Defense lowered by 3 and you emit a stange glow");
		Main.debuff[((ModBuff)this).Type] = true;
		Main.pvpBuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

	public override void Update(Player player, ref int buffIndex)
	{
		player.statDefense -= 3;
		Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 1.2f, 0.5f, 1.2f);
	}
}
