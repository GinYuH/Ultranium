using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Eldritch;

public class EldritchCooldown : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = false;
		//DisplayName.SetDefault("Eldritch Cool Down");
		//Description.SetDefault("You can't use your eldrich empowerment");
		Main.pvpBuff[((ModBuff)this).Type] = false;
		Main.debuff[((ModBuff)this).Type] = true;
		Main.buffNoSave[((ModBuff)this).Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
    }
}
