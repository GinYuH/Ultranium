using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class NightmareScale : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Erebus Scale");
		Tooltip.SetDefault("'It's slimey'");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = 11;
		Item.maxStack = 999;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}
}
