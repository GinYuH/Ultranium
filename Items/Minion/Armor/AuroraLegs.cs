using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(EquipType.Legs)]
public class AuroraLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Aurora Crystal Greaves");
		//Tooltip.SetDefault("2% increased movement speed");
	}

	public override void SetDefaults()
	{
		Item.width = 22;
		Item.height = 16;
		Item.value = 10000;
		Item.rare = ItemRarityID.Blue;
		Item.defense = 2;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 2f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "AuroraBar", 5);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
