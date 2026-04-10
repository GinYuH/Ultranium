using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class DarkHeadGear : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Darkmatter Headgear");
		((ModItem)this).Tooltip.SetDefault("12% increased ranged damage\n+5 max life");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 18;
		((Entity)(object)((ModItem)this).item).height = 18;
		((ModItem)this).item.value = Item.buyPrice(0, 80);
		((ModItem)this).item.rare = 11;
		((ModItem)this).item.defense = 25;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].overrideColor = new Color(34, 166, 118);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).mod.ItemType("DarkBody"))
		{
			return legs.type == ((ModItem)this).mod.ItemType("DarkLegs");
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n10% increased ranged damage and 20% chance to not consume ammo\n+10 max health\nPressing the Special Ability hotkey will grant you the eldritch ranger empowerment buff\nThis buff will cause all ranged weapons to deal 1.3x damage,\nand a 25% chance to not consume ammo\nThis ability has a 40 second cool down";
		player.GetModPlayer<UltraniumPlayer>().EldritchRangedSet = true;
		player.rangedDamage += 0.1f;
		player.statLifeMax2 += 10;
		player.ammoCost80 = true;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.rangedDamage += 0.12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 10);
		val.AddIngredient((Mod)null, "DarkMatter", 12);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
