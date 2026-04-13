using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.BossMasks;

[AutoloadEquip(EquipType.Head)]
public class MindFlayerMask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Mind Flayer Mask");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 28;
		Item.height = 26;
		Item.value = Item.sellPrice();
		Item.rare = ItemRarityID.Blue;
		Item.vanity = true;
	}
}
