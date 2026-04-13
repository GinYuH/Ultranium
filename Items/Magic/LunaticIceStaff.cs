using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticIceStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ice Mist Staff");
		//Tooltip.SetDefault("Shoots a giant ice ball that explodes into a circle of ice shards");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 100;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 22;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(1);
		Item.rare = ItemRarityID.Yellow;
		Item.UseSound = SoundID.Item120;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("IceBall").Type;
		Item.shootSpeed = 10f;
	}
}
