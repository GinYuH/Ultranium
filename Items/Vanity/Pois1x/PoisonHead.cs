using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Pois1x;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class PoisonHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		((ModItem)this).DisplayName.SetDefault("Pois1x's Hood");
		((ModItem)this).Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.vanity = true;
		((ModItem)this).item.rare = 9;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("PoisonBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("PoisonLegs");
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlinesForbidden = true;
	}
}
