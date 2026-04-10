using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class LuxWings : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Lux's Wings");
		// ((ModItem)this).Tooltip.SetDefault("Allows flight and slow fall\n~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.value = Item.buyPrice(1, 20);
		((ModItem)this).Item.rare = 9;
		((ModItem)this).Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.wingTimeMax = 150;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.75f;
		ascentWhenRising = 0.11f;
		maxCanAscendMultiplier = 1f;
		maxAscentMultiplier = 2.6f;
		constantAscend = 0.135f;
	}

	public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
	{
		speed = 7f;
		acceleration *= 2f;
	}
}
