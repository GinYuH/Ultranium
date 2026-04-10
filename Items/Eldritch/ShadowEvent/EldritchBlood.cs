using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class EldritchBlood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Mind Flayer Blood");
		((ModItem)this).Tooltip.SetDefault("Eldritch and slimey");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.maxStack = 999;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}
}
