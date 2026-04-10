using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class MysticTentacle : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Zephyr Squid's Tentacle");
		((ModItem)this).Tooltip.SetDefault("10% increased critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.value = Item.buyPrice(0, 2);
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.magicCrit += 10;
		player.meleeCrit += 10;
		player.rangedCrit += 10;
	}
}
