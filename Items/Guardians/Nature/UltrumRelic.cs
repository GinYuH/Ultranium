using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltrumRelic : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ultrum Relic Shard");
		// ((ModItem)this).Tooltip.SetDefault("\"Perhaps if combined with the other half, it's power can be restored...\"");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.value = 100;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.expert = true;
	}
}
