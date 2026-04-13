using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumBow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultranium Pulse Bow");
		//Tooltip.SetDefault("Fires a spread of nature arrows\n50% chance to not consume ammo");
	}

	public override void SetDefaults()
	{
		Item.damage = 230;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 48;
		Item.height = 74;
		Item.useTime = 32;
		Item.useAnimation = 32;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 1f;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.useAmmo = AmmoID.Arrow;
		Item.shoot = ProjectileID.VilePowder;
		Item.shootSpeed = 65f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.NextFloat() > 0.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 5f;
		float num2 = MathHelper.ToRadians(10f);
		position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 10f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("UltraniumArrow").Type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-10f, 0f);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(TileID.LunarCraftingStation);
		val.Register();
	}
}
