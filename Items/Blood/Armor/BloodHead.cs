using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class BloodHead : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Sanguine Face Mask");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.buyPrice(0, 1, 35);
		Item.rare = 2;
		Item.defense = 3;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("BloodBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("BloodLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n7% increased damage, +20 max mana, and +1 max minions";
		player.GetDamage(DamageClass.Melee) += 0.07f;
		player.GetDamage(DamageClass.Ranged) += 0.07f;
		player.GetDamage(DamageClass.Magic) += 0.07f;
		player.GetDamage(DamageClass.Summon) += 0.07f;
		player.statManaMax2 += 20;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 10);
		val.AddTile(16);
		val.Register();
	}
}
