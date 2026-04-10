using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class MiniProbe : ModItem
{
	private const int XOffset = 0;

	private const int YOffset = -200;

	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Miniature Martian Probe");
		((ModItem)this).Tooltip.SetDefault("Calls upon the martians");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 34;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.value = 100;
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.consumable = true;
	}

	public override bool UseItem(Player player)
	{
		Main.StartInvasion(4);
		return true;
	}
}
