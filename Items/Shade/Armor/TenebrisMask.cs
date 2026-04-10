using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class TenebrisMask : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Tenebris Mask");
		// Tooltip.SetDefault("2% increased summon damage");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 24;
		((Entity)(object)Item).height = 20;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("TenebrisBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("TenebrisLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "+2 max minions and 4% increased summon damage";
		player.maxMinions += 2;
		player.GetDamage(DamageClass.Summon) += 0.04f;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.02f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 8);
		val.AddTile(16);
		val.Register();
	}
}
