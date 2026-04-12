using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.BossMasks;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class IgnodiumMask : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ignodium Mask");
		Tooltip.SetDefault("");
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
