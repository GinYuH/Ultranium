using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class SoulCrushingDisappointment : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Soul-Crushing Dissapointment");
		Tooltip.SetDefault("Oddly resembles the skeletal structure of an animal.\nWas it always like this, or could have it been alive at some point?");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = -11;
		Item.maxStack = 1;
	}
}
