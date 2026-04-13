using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchBlood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Mind Flayer Blood");
		//Tooltip.SetDefault("Eldritch and slimey");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = ItemRarityID.Purple;
		Item.maxStack = 999;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}
}
