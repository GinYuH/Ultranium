using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealDidgeridoo : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Didgeridoo");
		((ModItem)this).Tooltip.SetDefault("Conujures lingering ethereal notes that create ethereal tentacles upon death");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 75;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 12;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.UseSound = ((ModItem)this).mod.GetLegacySoundSlot((SoundType)2, "Sounds/Item/Didgeridoo");
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EtherealNote");
		((ModItem)this).item.shootSpeed = 10f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-8f, 0f);
	}
}
