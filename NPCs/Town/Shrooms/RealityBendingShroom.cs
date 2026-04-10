using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class RealityBendingShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Reality-Bending Shroom");
		// ((ModItem)this).Tooltip.SetDefault("Even touching it seems to distort you\nOnly someone true enough is able to eat this and survive\nWho is worthy enough has yet to be found, but the truth will be told soon");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.rare = -11;
		((ModItem)this).Item.maxStack = 1;
	}
}
