using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrKnife : ModItem
{
	private int Use;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Zephyr Knife");
		Tooltip.SetDefault("Throws out short lived zephyr knives\nEvery 20 throws will throw a water knife\nThe water knife will leave behind lingering bubbles as it flies");
	}

	public override void SetDefaults()
	{
		Item.damage = 17;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = 2;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ZephyrKnife").Type;
		Item.shootSpeed = 6.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Use++;
		if (Use >= 20)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y);
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("WaterKnife").Type, Item.damage, knockback, player.whoAmI, 0f, 0f);
			Use = 0;
			return false;
		}
		return true;
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
