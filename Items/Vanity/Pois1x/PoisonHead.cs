using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Pois1x;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class PoisonHead : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).SetStaticDefaults();
		// ((ModItem)this).DisplayName.SetDefault("Pois1x's Hood");
		// ((ModItem)this).Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.vanity = true;
		((ModItem)this).Item.rare = 9;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("PoisonBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("PoisonLegs").Type;
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlinesForbidden = true;
	}
}
