using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class HorrorHelm : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Horror Helmet");
		// ((ModItem)this).Tooltip.SetDefault("12% increased melee critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 18;
		((Entity)(object)((ModItem)this).Item).height = 18;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.defense = 27;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("HorrorBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("HorrorLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nMelee weapons have a chance to shoot out dread flame bolts\n10% increased melee damage";
		player.GetModPlayer<UltraniumPlayer>().HorrorMeleeSet = true;
		player.GetDamage(DamageClass.Melee) += 0.1f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance(DamageClass.Melee) += 12;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 9);
		val.AddTile(412);
		val.Register();
	}
}
