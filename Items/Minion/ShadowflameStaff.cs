using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion;

public class ShadowflameStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowflame Staff");
		//Tooltip.SetDefault("Summons a Shadowflame Apparition to fight for you");
	}

	public override void SetDefaults()
	{
		Item.mana = 20;
		Item.damage = 44;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = ItemRarityID.Pink;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("ShadowApparition").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("ShadowApparitionBuff").Type;
		Item.buffTime = 3600;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return player.altFunctionUse != 2;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (player.altFunctionUse == 2)
		{
			player.MinionNPCTargetAim(false);
		}
		return null;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "ShadowFlame", 8);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
