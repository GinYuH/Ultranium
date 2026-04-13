using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DarkResonatorBroken : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Rift Artifact");
		//Tooltip.SetDefault("An ancient artifact of the shadows\nIt seems to be broken, but can possibly be repaired with the right materials...");
	}

	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 18;
		Item.maxStack = 1;
		Item.rare = 11;
		Item.useAnimation = 45;
		Item.useTime = 45;
		Item.useStyle = 4;
		Item.consumable = false;
	}
}
