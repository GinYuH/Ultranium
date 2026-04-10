using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class CosmicWings : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Cosmic Wings");
		((ModItem)this).Tooltip.SetDefault("Gives infinite flight time and very fast flight speed");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.value = Item.buyPrice(1, 20);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.wingTimeMax = 999999;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.85f;
		ascentWhenRising = 0.15f;
		maxCanAscendMultiplier = 1.1f;
		maxAscentMultiplier = 3f;
		constantAscend = 0.095f;
	}

	public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
	{
		speed = 10f;
		acceleration *= 5.5f;
	}
}
