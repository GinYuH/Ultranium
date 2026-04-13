using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
		//DisplayName.SetDefault("Eldritch Pumpkin Staff");
		//Tooltip.SetDefault("Casts a spread of pumpkin fire");
	}

	public override void SetDefaults()
	{
		Item.damage = 40;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 15;
		Item.width = 80;
		Item.height = 80;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 10, 50);
		Item.rare = ItemRarityID.LightRed;
		Item.UseSound = SoundID.DD2_BetsysWrathShot;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchPumpkinFire").Type;
		Item.shootSpeed = 8f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num = 2 + Main.rand.Next(4);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(20f));
			float num2 = 1f - Main.rand.NextFloat() * 0.3f;
			vector *= num2;
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "PumpkinStaff", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(ItemID.SoulofNight, 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
