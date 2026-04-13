using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(EquipType.Body)]
public class ShadowflameBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowflame Chestmail");
		//Tooltip.SetDefault("5% increased summon damage and +1 max minions");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Pink;
		Item.defense = 15;
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "ShadowFlame", 10);
		val.AddIngredient(ItemID.SoulofNight, 10);
		val.AddIngredient(ItemID.Silk, 17);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
