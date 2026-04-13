using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch;

public class DarkMatter : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Other-Worldy Essence");
		//Tooltip.SetDefault("'It pulses with an eldritch energy'");
		ItemID.Sets.ItemIconPulse[Item.type] = true;
		ItemID.Sets.ItemNoGravity[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.value = Item.buyPrice(0, 1);
		Item.rare = 11;
		Item.maxStack = 999;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Lighting.AddLight(Item.position, 0.38f, 0.01f, 0.47f);
	}
}
