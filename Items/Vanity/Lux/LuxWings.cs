using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class LuxWings : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Lux's Wings");
		Tooltip.SetDefault("Allows flight and slow fall\n~Developer item~");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.value = Item.buyPrice(1, 20);
		Item.rare = 9;
		Item.accessory = true;
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
