using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class RealityBendingShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Reality-Bending Shroom");
		//Tooltip.SetDefault("Even touching it seems to distort you\nOnly someone true enough is able to eat this and survive\nWho is worthy enough has yet to be found, but the truth will be told soon");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = -11;
		Item.maxStack = 1;
	}
}
