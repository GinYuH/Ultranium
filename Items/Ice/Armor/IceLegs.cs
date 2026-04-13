using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(EquipType.Legs)]
public class IceLegs : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ice Walker's Legs");
		//Tooltip.SetDefault("3% increased movement speed");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = ItemRarityID.Orange;
		Item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += 3f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.IceBlock, 35);
		val.AddIngredient((Mod)null, "IcePelt", 10);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
