using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

[AutoloadEquip(EquipType.Wings)]
public class XenanisWings : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ethereal Wings");
		Tooltip.SetDefault("Allows flight and slow fall");
		ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(165, 6.2f, 3.2f);
    }

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.value = Item.buyPrice(0, 25);
		Item.rare = 9;
		Item.accessory = true;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.85f;
		ascentWhenRising = 0.15f;
		maxCanAscendMultiplier = 1.1f;
		maxAscentMultiplier = 3f;
		constantAscend = 0.095f;
	}

	public override bool WingUpdate(Player player, bool inUse)
	{
		int num = 5;
		if (inUse)
		{
			if (player.controlJump && player.wingTime <= 0f)
			{
				player.wingFrame = 2;
			}
			player.wingFrameCounter++;
			if (player.wingFrameCounter > num)
			{
				player.wingFrame++;
				player.wingFrameCounter = 0;
				if (player.wingFrame > 3)
				{
					player.wingFrame = 0;
				}
			}
		}
		else
		{
			player.wingFrame = 0;
			if (player.velocity.Y != 0f)
			{
				player.wingFrame = 1;
			}
		}
		return true;
	}
}
