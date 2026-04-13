using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.BossMasks;

[AutoloadEquip(EquipType.Head)]
public class SquidMask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Zephyr Squid Mask");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 26;
		Item.value = Item.sellPrice();
		Item.rare = 1;
		Item.vanity = true;
	}
}
