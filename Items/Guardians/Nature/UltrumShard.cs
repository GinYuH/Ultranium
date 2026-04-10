using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltrumShard : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultrum Essence Shard");
		((ModItem)this).Tooltip.SetDefault("\"An ancient energy of nature resides within this fragment\"");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = 100;
		((ModItem)this).item.rare = 11;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(241, 166, 0);
	}
}
