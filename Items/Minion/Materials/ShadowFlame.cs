using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Materials;

public class ShadowFlame : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Shadowflame");
		// Tooltip.SetDefault("");
		ItemID.Sets.ItemNoGravity[Item.type] = true;
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		Item.width = Item.width;
		Item.height = Item.height;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = 5;
	}
}
