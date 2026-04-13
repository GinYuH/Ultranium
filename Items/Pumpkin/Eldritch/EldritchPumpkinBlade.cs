using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Pumpkin Buster");
		//Tooltip.SetDefault("Fires flaming eldritch seeds\nHas a chance to shoot out a spread of eldritch bolts that deal twice the sword's damage");
	}

	public override void SetDefaults()
	{
		Item.damage = 55;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 10, 50);
		Item.rare = ItemRarityID.LightRed;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("EldritchSeed").Type;
		Item.shootSpeed = 6.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(7) == 0)
		{
			float num = 5f;
			float num2 = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
			for (int i = 0; (float)i < num; i++)
			{
				Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
				Projectile.NewProjectile(source, position.X, position.Y, vector.X * 10f, vector.Y * 10f, Mod.Find<ModProjectile>("EldritchPumpkinTentacle").Type, damage * 2, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
		return true;
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
		val.AddIngredient((Mod)null, "PumpkinBlade", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(ItemID.SoulofNight, 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
