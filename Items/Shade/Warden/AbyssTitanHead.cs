using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(EquipType.Head)]
public class AbyssTitanHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyss Titan Helmet");
		//Tooltip.SetDefault("10% increased ranged damage and critical strike chance");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Lime;
		Item.defense = 18;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Ranged) += 0.1f;
		player.GetCritChance(DamageClass.Ranged) += 10;
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
        player.ammoCost75 = true;
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
