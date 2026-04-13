using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class Moorhsum : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Moorhsum");
		//Tooltip.SetDefault("A peculiar mushroom native to the underground.\nSeems useless, only a dead man would care to look at it");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = ItemRarityID.Quest;
		Item.maxStack = 1;
	}
}
