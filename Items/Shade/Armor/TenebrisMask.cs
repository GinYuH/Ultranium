using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Armor;

[AutoloadEquip(EquipType.Head)]
public class TenebrisMask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tenebris Mask");
		//Tooltip.SetDefault("2% increased summon damage");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 20;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = ItemRarityID.Blue;
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
		player.setBonus = this.GetLocalizedValue("SetBonus");
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "NightmareBar", 8);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
