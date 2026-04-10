using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class IceBody : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ice Walker's Chestplate");
		// ((ModItem)this).Tooltip.SetDefault("5% increased damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 2, 50);
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.defense = 7;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(664, 55);
		val.AddIngredient((Mod)null, "IcePelt", 12);
		val.AddTile(16);
		val.Register();
	}
}
