using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadeWispStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Shadow Wisp Staff");
		// Tooltip.SetDefault("Summons a Shade Wisp to fight with you");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 20;
		Item.damage = 20;
		((Entity)(object)Item).width = 26;
		((Entity)(object)Item).height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 2, 50);
		Item.rare = 1;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("ShadeWisp").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("ShadeWispBuff").Type;
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.Register();
	}
}
