using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Pois1x;

[AutoloadEquip(EquipType.Head)]
public class PoisonHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pois1x's Hood");
		//Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("PoisonBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("PoisonLegs").Type;
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlinesForbidden = true;
	}
}
