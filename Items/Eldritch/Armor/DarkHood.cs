using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(EquipType.Head)]
public class DarkHood : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Darkmatter Hood");
		Tooltip.SetDefault("12% increased magic damage\n+5 max life and +10 max mana");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 80);
		Item.rare = 11;
		Item.defense = 25;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("DarkBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("DarkLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n10% increased magic damage and 20% reduced mana usage\n+10 max health and mana\nPressing the Special Ability hotkey will grant you the eldritch magic empowerment buff\nThis buff will cause all magic weapons to deal 1.3x damage,\nand makes magic weapons cost no mana\nThis ability has a 40 second cool down";
		player.GetModPlayer<UltraniumPlayer>().EldritchMagicSet = true;
		player.GetDamage(DamageClass.Magic) += 0.1f;
		player.statLifeMax2 += 10;
		player.statManaMax2 += 10;
		player.manaCost -= 0.2f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 10;
		player.GetDamage(DamageClass.Magic) += 0.12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareScale", 8);
		val.AddIngredient((Mod)null, "NightmareBar", 10);
		val.AddIngredient((Mod)null, "DarkMatter", 12);
		val.AddTile(412);
		val.Register();
	}
}
