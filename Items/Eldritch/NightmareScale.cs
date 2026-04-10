using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class NightmareScale : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Erebus Scale");
		// ((ModItem)this).Tooltip.SetDefault("'It's slimey'");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.maxStack = 999;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}
}
