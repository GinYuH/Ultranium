using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class HorrorHood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Horror Hood");
		((ModItem)this).Tooltip.SetDefault("12% increased magic critical strike chance\n+50 max mana");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(1);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.defense = 22;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(200, 0, 0);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("HorrorBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("HorrorLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nMagic weapons have a chance to shoot out dread flame bolts\n20% reduced mana cost";
		player.GetModPlayer<UltraniumPlayer>().HorrorMagicSet = true;
		player.manaCost -= 0.2f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.magicCrit += 10;
		player.statManaMax2 += 20;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareFuel", 9);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
