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
		// ((ModItem)this).DisplayName.SetDefault("Shadowflame");
		// ((ModItem)this).Tooltip.SetDefault("");
		ItemID.Sets.ItemNoGravity[((ModItem)this).Item.type] = true;
		Main.RegisterItemAnimation(((ModItem)this).Item.type, new DrawAnimationVertical(6, 6));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((Entity)(object)((ModItem)this).Item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).Item).height = ((Entity)(object)item).height;
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.value = 1000;
		((ModItem)this).Item.rare = 5;
	}
}
