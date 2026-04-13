using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(EquipType.Head)]
public class ShadowflameHood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowflame Cowl");
		//Tooltip.SetDefault("5% increased summon damage and +1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Pink;
		Item.defense = 7;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("ShadowflameBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("ShadowflameLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nYour minions will inflict shadow flame on enemies";
		player.GetModPlayer<UltraniumPlayer>().ShadowflameSet = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.05f;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "ShadowFlame", 7);
		val.AddIngredient(ItemID.SoulofNight, 7);
		val.AddIngredient(ItemID.Silk, 12);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
