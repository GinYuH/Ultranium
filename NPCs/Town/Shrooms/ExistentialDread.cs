using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class ExistentialDread : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Existential Dread");
		//Tooltip.SetDefault("Looks hungry, violent, and dangerous. Probably could eat somebody if left alone. Cool.");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = ItemRarityID.Quest;
		Item.maxStack = 1;
	}
}
