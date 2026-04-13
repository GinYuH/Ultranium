using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Pumpkin Scepter");
		//Tooltip.SetDefault("Summons a stationary eldritch pumpkin that shoots flaming seeds at enemies\nOnly one pumpkin sentry can be active at once");
	}

	public override void SetDefaults()
	{
		((Entity)(object)Item).width = 26;
		((Entity)(object)Item).height = 28;
		Item.rare = ItemRarityID.LightRed;
		Item.mana = 20;
		Item.damage = 45;
		Item.knockBack = 1f;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.DamageType = DamageClass.Summon;
		Item.noMelee = true;
		Item.UseSound = SoundID.Item117;
		Item.shoot = Mod.Find<ModProjectile>("EldritchPumpkinSentry").Type;
		Item.shootSpeed = 0f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		for (int i = 0; i < Main.projectile.Length; i++)
		{
			Projectile projectile = Main.projectile[i];
			if (((Entity)projectile).active && projectile.type == Item.shoot && projectile.owner == player.whoAmI)
			{
				((Entity)projectile).active = false;
			}
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
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
		val.AddIngredient((Mod)null, "PumpkinSummon", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(ItemID.SoulofNight, 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
