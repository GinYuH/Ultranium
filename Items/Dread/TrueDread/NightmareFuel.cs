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
		// ((ModItem)this).DisplayName.SetDefault("Nightmare Fuel");
		// ((ModItem)this).Tooltip.SetDefault("");
		ItemID.Sets.ItemNoGravity[((ModItem)this).Item.type] = true;
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
		((Entity)(object)((ModItem)this).Item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).Item).height = ((Entity)(object)item).height;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.value = 1000;
		((ModItem)this).Item.rare = 4;
	}
}
