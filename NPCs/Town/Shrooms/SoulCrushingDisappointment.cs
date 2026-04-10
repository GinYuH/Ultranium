using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class SoulCrushingDisappointment : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Soul-Crushing Dissapointment");
		((ModItem)this).Tooltip.SetDefault("Oddly resembles the skeletal structure of an animal.\nWas it always like this, or could have it been alive at some point?");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.rare = -11;
		((ModItem)this).item.maxStack = 1;
	}
}
