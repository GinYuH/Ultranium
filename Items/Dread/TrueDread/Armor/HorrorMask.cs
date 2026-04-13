using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread.Armor;

[AutoloadEquip(EquipType.Head)]
public class HorrorMask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Horror Mask");
		//Tooltip.SetDefault("10% increased summon damage\n+1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(1);
		Item.rare = ItemRarityID.Purple;
		Item.defense = 22;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("HorrorBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("HorrorLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nSummon weapons will shoot dread flame bolts when used\n(This effect will still apply if you have maximum minions active)\n+3 max minions";
		player.GetModPlayer<UltraniumPlayer>().HorrorSummonSet = true;
		player.maxMinions += 3;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.1f;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 9);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
