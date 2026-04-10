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
		// ((ModItem)this).DisplayName.SetDefault("Other-Worldy Essence");
		// ((ModItem)this).Tooltip.SetDefault("'It pulses with an eldritch energy'");
		ItemID.Sets.ItemIconPulse[((ModItem)this).Item.type] = true;
		ItemID.Sets.ItemNoGravity[((ModItem)this).Item.type] = true;
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.value = Item.buyPrice(0, 1);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.maxStack = 999;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Lighting.AddLight(((Entity)(object)((ModItem)this).Item).position, 0.38f, 0.01f, 0.47f);
	}
}
