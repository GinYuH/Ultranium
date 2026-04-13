using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class NightmareFuel : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Nightmare Fuel");
		//Tooltip.SetDefault("");
		ItemID.Sets.ItemNoGravity[Item.type] = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		Item.width = Item.width;
		Item.height = Item.height;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = 4;
	}
}
