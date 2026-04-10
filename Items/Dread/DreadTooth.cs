using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadTooth : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Dread Tooth");
		// ((ModItem)this).Tooltip.SetDefault("Increases armor penetration by 8");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 28;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetArmorPenetration(DamageClass.Generic) += 8;
	}
}
