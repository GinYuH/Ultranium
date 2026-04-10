using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class TruffleShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Truffle");
		((ModItem)this).Tooltip.SetDefault("He is probably very upset, and would like to go back to his old body immediately.\nIts surprising that fungi can achieve this level of sentience.");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.rare = -11;
		((ModItem)this).item.maxStack = 1;
	}
}
