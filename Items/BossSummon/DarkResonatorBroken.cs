using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class DarkResonatorBroken : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Rift Artifact");
		// ((ModItem)this).Tooltip.SetDefault("An ancient artifact of the shadows\nIt seems to be broken, but can possibly be repaired with the right materials...");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.useAnimation = 45;
		((ModItem)this).Item.useTime = 45;
		((ModItem)this).Item.useStyle = 4;
		((ModItem)this).Item.consumable = false;
	}
}
