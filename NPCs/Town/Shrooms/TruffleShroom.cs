using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class TruffleShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Truffle");
		// Tooltip.SetDefault("He is probably very upset, and would like to go back to his old body immediately.\nIts surprising that fungi can achieve this level of sentience.");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = -11;
		Item.maxStack = 1;
	}
}
