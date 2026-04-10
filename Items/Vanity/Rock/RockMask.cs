using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Rock;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class RockMask : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		// ((ModItem)this).DisplayName.SetDefault("RockWizard5's Mask");
		// ((ModItem)this).Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.vanity = true;
		((ModItem)this).Item.rare = 9;
	}

	public override bool DrawHead()/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false if you returned false */
	{
		return false;
	}
}
