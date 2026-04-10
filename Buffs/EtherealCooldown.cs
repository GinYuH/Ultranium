using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs;

public class EtherealCooldown : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = false;
		// ((ModBuff)this).DisplayName.SetDefault("Ethereal Cool Down");
		// ((ModBuff)this).Description.SetDefault("Xenanis's core is recharging...");
		Main.pvpBuff[((ModBuff)this).Type] = false;
		Main.debuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<UltraniumPlayer>();
		player.GetModPlayer<UltraniumPlayer>().XenanisCore = false;
	}
}
