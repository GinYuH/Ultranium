using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class GuardianCore : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Guardian's Insignia");
		//Tooltip.SetDefault("15% increased damage and 10% increased critical strike chance\nincreases max life and max mana by 40\n+3 max minions\n20% reduced mana usage");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(1);
		Item.accessory = true;
		Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 40;
		player.statLifeMax2 += 40;
		player.maxMinions += 3;
		player.manaCost += -0.2f;
		player.GetDamage(DamageClass.Summon) += 0.15f;
		player.GetDamage(DamageClass.Throwing) += 0.15f;
		player.GetDamage(DamageClass.Magic) += 0.15f;
		player.GetDamage(DamageClass.Melee) += 0.15f;
		player.GetDamage(DamageClass.Ranged) += 0.15f;
		player.GetCritChance(DamageClass.Throwing) += 10;
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
		player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "UltrumRelic", 1);
		val.AddIngredient(null, "IgnodiumRelic", 1);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
