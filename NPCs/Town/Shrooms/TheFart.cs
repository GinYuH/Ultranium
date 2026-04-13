using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class TheFart : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The Fart");
		//Tooltip.SetDefault("A mystical fart cloud that is constantly taking the shape of a mushroom.\nIts smell is legendary, whether that is a good thing or not is up to you.");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = ItemRarityID.Quest;
		Item.maxStack = 1;
	}
}
