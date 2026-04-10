using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltrumRelic : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ultrum Relic Shard");
		((ModItem)this).Tooltip.SetDefault("\"Perhaps if combined with the other half, it's power can be restored...\"");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.value = Item.buyPrice(0, 45);
		((Entity)(object)((ModItem)this).item).width = 24;
		((Entity)(object)((ModItem)this).item).height = 24;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = 100;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.expert = true;
	}
}
