using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class MiniProbe : ModItem
{
	private const int XOffset = 0;

	private const int YOffset = -200;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Miniature Martian Probe");
		//Tooltip.SetDefault("Calls upon the martians");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 30;
		Item.maxStack = 20;
		Item.value = 100;
		Item.rare = ItemRarityID.Orange;
		Item.useAnimation = 30;
		Item.useTime = 30;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.consumable = true;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		Main.StartInvasion(4);
		return true;
	}
}
