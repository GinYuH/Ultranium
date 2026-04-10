using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DarkResonatorBroken : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Rift Artifact");
		((ModItem)this).Tooltip.SetDefault("An ancient artifact of the shadows\nIt seems to be broken, but can possibly be repaired with the right materials...");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.maxStack = 1;
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.consumable = false;
	}
}
