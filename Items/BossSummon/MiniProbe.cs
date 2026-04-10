using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class MiniProbe : ModItem
{
	private const int XOffset = 0;

	private const int YOffset = -200;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Miniature Martian Probe");
		// ((ModItem)this).Tooltip.SetDefault("Calls upon the martians");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.maxStack = 20;
		((ModItem)this).Item.value = 100;
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.consumable = true;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		Main.StartInvasion(4);
		return true;
	}
}
