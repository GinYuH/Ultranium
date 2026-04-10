using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.Materials;

public class DreadFlame : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Dread Flame");
		// ((ModItem)this).Tooltip.SetDefault("'The Essence of pure, concentrated fear'");
		ItemID.Sets.ItemNoGravity[((ModItem)this).Item.type] = true;
		Main.RegisterItemAnimation(((ModItem)this).Item.type, new DrawAnimationVertical(7, 6));
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
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 15);
		((ModItem)this).Item.rare = 4;
	}
}
