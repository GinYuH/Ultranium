using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.Armor;

[AutoloadEquip(EquipType.Head)]
public class DarkHeadGear : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Darkmatter Headgear");
		//Tooltip.SetDefault("12% increased ranged damage\n+5 max life");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 80);
		Item.rare = ItemRarityID.Purple;
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
		player.setBonus = this.GetLocalizedValue("SetBonus");
        player.GetModPlayer<UltraniumPlayer>().EldritchRangedSet = true;
		player.GetDamage(DamageClass.Ranged) += 0.1f;
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
		player.GetDamage(DamageClass.Ranged) += 0.12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareScale", 8);
		val.AddIngredient(null, "NightmareBar", 10);
		val.AddIngredient(null, "DarkMatter", 12);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
