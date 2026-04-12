using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class DarkMask : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Darkmatter Mask");
		Tooltip.SetDefault("8% increased summon damage\n+5 max life and mana, and +2 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 80);
		Item.rare = 11;
		Item.defense = 22;
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
		player.setBonus = "\nSummons an eldritch monolith to fight for you (The monolith does not take up minion slots)\nPressing the Special Ability hotkey will grant you the eldritch summon empowerment buff\nThis buff will cause all summons to deal 1.5x damage,\nas well as causing your monolith to become stronger and shoot faster\nThis ability has a 40 second cool down";
		player.GetModPlayer<UltraniumPlayer>().EldritchSummonSet = true;
		player.GetModPlayer<UltraniumPlayer>().EldritchSummonEye = true;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += 5;
		player.statManaMax2 += 5;
		player.maxMinions += 2;
		player.GetDamage(DamageClass.Summon) += 0.08f;
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
