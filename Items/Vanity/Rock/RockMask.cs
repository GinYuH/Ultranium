using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Rock;

[AutoloadEquip(EquipType.Head)]
public class RockMask : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		DisplayName.SetDefault("RockWizard5's Mask");
		Tooltip.SetDefault("~Developer item~");
		ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = false;
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}
}
