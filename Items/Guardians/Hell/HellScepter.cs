using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Igneous Scepter");
		Tooltip.SetDefault("Summons a miniature ignodium to fight with you\nOnly one can be summoned at once\nDoes not take up any minion slots");
	}

	public override void SetDefaults()
	{
		Item.mana = 35;
		Item.damage = 200;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Summon;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("Ignodium").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("IgnodiumBuff").Type;
		Item.buffTime = 3600;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Ignodium").Type] > 0)
		{
			return false;
		}
		return true;
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
