using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(EquipType.Body)]
public class AldinBody : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cosmic Mage's Body");
		Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 18;
		((Entity)(object)Item).height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}
}
