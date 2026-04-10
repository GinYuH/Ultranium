using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Zephyr Scepter");
		// Tooltip.SetDefault("Summons a baby zephyr squid to fight with you");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 20;
		Item.damage = 14;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = 2;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("BabySquid").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("BabySquidBuff").Type;
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
			player.MinionNPCTargetAim();
		}
		return UseItem(player);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "OceanScale", 8);
		val.AddIngredient(275, 5);
		val.AddTile(16);
		val.Register();
	}
}
