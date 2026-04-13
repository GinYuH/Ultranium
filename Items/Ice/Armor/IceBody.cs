using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(EquipType.Body)]
public class IceBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ice Walker's Chestplate");
		//Tooltip.SetDefault("5% increased damage");
	}

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 22;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 3;
		Item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.GetDamage(DamageClass.Summon) += 0.05f;
		player.GetDamage(DamageClass.Magic) += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(664, 55);
		val.AddIngredient((Mod)null, "IcePelt", 12);
		val.AddTile(16);
		val.Register();
	}
}
