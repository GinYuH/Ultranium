using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltrumShard : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultrum Essence Shard");
		//Tooltip.SetDefault("\"An ancient energy of nature resides within this fragment\"");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 10);
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 100;
		Item.rare = 11;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}
}
