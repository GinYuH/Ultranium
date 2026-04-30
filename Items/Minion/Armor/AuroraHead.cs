using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(EquipType.Head)]
public class AuroraHead : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Aurora Crystal Mask");
		//Tooltip.SetDefault("5% increased summon damage");
	}

	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 20;
		Item.value = 10000;
		Item.rare = ItemRarityID.Blue;
		Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("AuroraBody").Type)
		{
			return legs.type == Mod.Find<ModItem>("AuroraLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalizedValue("SetBonus");
        player.starCloakItem = Item;
		player.maxMinions += 2;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.05f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "AuroraBar", 6);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
