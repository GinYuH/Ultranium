using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(EquipType.Head)]
public class AbyssHood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyss Warden Hood");
		//Tooltip.SetDefault("5% increased summon damage");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Lime;
		Item.defense = 15;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.05f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("AbyssWardenBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("AbyssWardenLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalizedValue("SetBonus");
        if (player.statLife < (int)((float)player.statLifeMax2 * 0.75f))
		{
			player.maxMinions++;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.5f))
		{
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.maxMinions++;
		}
		if (player.statLife < (int)((float)player.statLifeMax2 * 0.25f))
		{
			player.maxMinions++;
		}
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "AbyssRockItem", 45);
		val.AddIngredient(null, "DepthGlowstoneItem", 35);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
