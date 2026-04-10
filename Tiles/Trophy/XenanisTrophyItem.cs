using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Trophy;

public class XenanisTrophyItem : ModItem
{
	public static int _type;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Xenanis Trophy");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 30;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useTime = 10;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.value = 0;
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.createTile = ((ModItem)this).Mod.Find<ModTile>("XenanisTrophy").Type;
		((ModItem)this).Item.placeStyle = 0;
	}
}
