using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class IgnodiumRelic : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ignodium Relic Shard");
		// Tooltip.SetDefault("\"Perhaps if combined with the other half, it's power can be restored...\"");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 45);
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 100;
		Item.rare = 11;
		Item.expert = true;
	}
}
