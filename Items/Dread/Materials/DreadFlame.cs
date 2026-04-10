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
		((ModItem)this).DisplayName.SetDefault("Dread Flame");
		((ModItem)this).Tooltip.SetDefault("'The Essence of pure, concentrated fear'");
		ItemID.Sets.ItemNoGravity[((ModItem)this).item.type] = true;
		Main.RegisterItemAnimation(((ModItem)this).item.type, new DrawAnimationVertical(7, 6));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void SetDefaults()
	{
		Item item = new Item();
		((Entity)(object)((ModItem)this).item).width = ((Entity)(object)item).width;
		((Entity)(object)((ModItem)this).item).height = ((Entity)(object)item).height;
		((ModItem)this).item.maxStack = 999;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 15);
		((ModItem)this).item.rare = 4;
	}
}
