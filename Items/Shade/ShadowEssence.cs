using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowEssence : ModItem
{
	private Color[] itemNameCycleColors = new Color[4]
	{
		new Color(65, 74, 112),
		new Color(51, 49, 95),
		new Color(54, 19, 95),
		new Color(58, 11, 67)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadow Mana");
		//Tooltip.SetDefault("'The Essence of shadow magic'");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
		ItemID.Sets.ItemNoGravity[Item.type] = true;
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 26;
		((Entity)(object)Item).height = 34;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 4);
		return Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 4], amount);
	}
}
