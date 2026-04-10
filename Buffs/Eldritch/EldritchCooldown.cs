using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchCooldown : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = false;
		// ((ModBuff)this).DisplayName.SetDefault("Eldritch Cool Down");
		// ((ModBuff)this).Description.SetDefault("You can't use your eldrich empowerment");
		Main.pvpBuff[((ModBuff)this).Type] = false;
		Main.debuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
		base.longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
	}
}
