using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(EquipType.Legs)]
public class AldinRobe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Mage's Robe");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 18;
		((Entity)(object)Item).height = 18;
		Item.vanity = true;
		Item.rare = ItemRarityID.Cyan;
	}
}
