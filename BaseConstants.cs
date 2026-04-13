using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ultranium;

public class BaseConstants
{
	public static readonly Rectangle FRAME_PLAYER = new Rectangle(0, 0, 40, 54);

	public const int NET_NPC_UPDATE = 23;

	public const int NET_NPC_HIT = 28;

	public const int NET_PROJ_UPDATE = 27;

	public const int NET_PLAYER_UPDATE = 13;

	public const int NET_TILE_UPDATE = 17;

	public const int NET_ITEM_UPDATE = 21;

	public const int NET_PLAYER_LIFE = 16;

	public const int NET_PLAYER_MANA = 42;

	public const int NET_PLAYER_ITEMROT_ITEMANIM = 41;

	public const int NET_PROJ_MANUALKILL = 29;

	public const int DUSTID_FIRE = 6;

	public const int DUSTID_WATERCANDLE = 29;

	public const int DUSTID_GLITTER = 43;

	public const int DUSTID_BLOOD = 5;

	public const int DUSTID_BONE = 26;

	public const int DUSTID_METAL = 30;

	public const int DUSTID_METALDUST = 31;

	public const int DUSTID_CURSEDFIRE = 75;

	public const int DUSTID_ICHOR = 170;

	public const int DUSTID_FROST = 185;

	public const int DUSTID_SOLAR = 6;

	public const int DUSTID_NEBULA = 242;

	public const int DUSTID_STARDUST = 229;

	public const int DUSTID_VORTEX = 229;

	public const int DUSTID_LUNAR = 249;

	public const int ITEMID_HEART = 58;

	public const int ITEMID_MANASTAR = 184;

	public const int ITEMID_HEALTHPOTION_LESSER = 28;

	public const int ITEMID_HEALTHPOTION = 188;

	public const int ITEMID_HEALTHPOTION_GREATER = 499;

	public const int ITEMID_MANAPOTION_LESSER = 110;

	public const int ITEMID_MANAPOTION = 189;

	public const int ITEMID_MANAPOTION_GREATER = 500;

	public static readonly int[] ITEMIDS_GEMS = new int[7] { 181, 180, 177, 178, 179, 182, 999 };

	public static readonly int AMMOTYPE_ARROW = AmmoID.Arrow;

	public static readonly int AMMOTYPE_BULLET = AmmoID.Bullet;

	public static readonly int[] TILEIDS_CONVERTCORRUPTION = new int[6] { 25, 23, 112, 163, 398, 400 };

	public static readonly int[] TILEIDS_CONVERTHALLOW = new int[6] { 117, 109, 116, 164, 402, 403 };

	public static readonly int[] TILEIDS_CONVERTCRIMSON = new int[6] { 203, 199, 234, 200, 399, 401 };

	public static readonly int[] TILEIDS_CONVERTOVERWORLD = new int[6] { 1, 2, 53, 161, 397, 396 };

	public static readonly int[] TILEIDS_CONVERTALL = BaseUtility.CombineArrays(BaseUtility.CombineArrays(TILEIDS_CONVERTOVERWORLD, TILEIDS_CONVERTHALLOW), BaseUtility.CombineArrays(TILEIDS_CONVERTCORRUPTION, TILEIDS_CONVERTCRIMSON));

	public static readonly int[] TILEIDS_DUNGEON = new int[14]
	{
		10, 11, 12, 13, 19, 21, 28, 41, 42, 43,
		44, 48, 49, 50
	};

	public static readonly int[] TILEIDS_DUNGEONSTRICT = new int[7] { 41, 42, 43, 44, 48, 49, 50 };

	public const int TILEID_DOORCLOSED = 10;

	public const int TILEID_CHESTS = 21;

	public const int TILEID_SKYISLANDBRICK = 202;

	public const int CHESTSTYLE_WOOD = 0;

	public const int CHESTSTYLE_GOLD = 1;

	public const int CHESTSTYLE_GOLDLOCKED = 2;

	public const int CHESTSTYLE_SHADOW = 3;

	public const int CHESTSTYLE_SHADOWLOCKED = 4;

	public const int CHESTSTYLE_BARREL = 5;

	public const int CHESTSTYLE_TRASHCAN = 6;

	public const int CHESTSTYLE_EBONWOOD = 7;

	public const int CHESTSTYLE_MOHAGONY = 8;

	public const int CHESTSTYLE_HALLOWWOOD = 9;

	public const int CHESTSTYLE_JUNGLE = 10;

	public const int CHESTSTYLE_ICE = 11;

	public const int CHESTSTYLE_VINED = 12;

	public const int CHESTSTYLE_SKY = 13;

	public const int CHESTSTYLE_SHADEWOOD = 14;

	public const int CHESTSTYLE_WEBBED = 15;

	public const int CHESTSTYLE_LIHZAHRD = 16;

	public const int CHESTSTYLE_SEA = 17;

	public const int CHESTSTYLE_DUNGJUNGLE = 18;

	public const int CHESTSTYLE_DUNGCORRUPT = 19;

	public const int CHESTSTYLE_DUNGCRIMSON = 20;

	public const int CHESTSTYLE_DUNGHALLOWED = 21;

	public const int CHESTSTYLE_DUNGICE = 22;

	public const int CHESTSTYLE_DUNGJUNGLELOCKED = 23;

	public const int CHESTSTYLE_DUNGCORRUPTLOCKED = 24;

	public const int CHESTSTYLE_DUNGCRIMSONLOCKED = 25;

	public const int CHESTSTYLE_DUNGHALLOWEDLOCKED = 26;

	public const int CHESTSTYLE_DUNGICELOCKED = 27;

	public static readonly Color CHATCOLOR_PURPLE = new Color(175, 75, 255);

	public static readonly Color CHATCOLOR_GREEN = new Color(50, 255, 130);

	public static readonly Color CHATCOLOR_RED = new Color(255, 25, 25);

	public static readonly Color CHATCOLOR_YELLOW = new Color(255, 240, 20);

	public static readonly Color NPCTEXTCOLOR_BUFF = new Color(255, 140, 40);

	public const int ARMORID_HEAD = 0;

	public const int ARMORID_BODY = 1;

	public const int ARMORID_LEGS = 2;

	public const int ARMORID_HEADVANITY = 10;

	public const int ARMORID_BODYVANITY = 11;

	public const int ARMORID_LEGSVANITY = 12;

	public const int TIME_DAWNDUSK = 0;

	public const int TIME_MIDDAY = 27000;

	public const int TIME_MIDNIGHT = 16200;

	public const int INVASION_GOBLIN = 1;

	public const int INVASION_FROSTLEGION = 2;

	public const int INVASION_PIRATE = 3;

	public const int INVASION_MARTIAN = 4;

	public static string NAME_MAINPLAYER
	{
		get
		{
			if (Main.netMode != NetmodeID.Server && Main.player[Main.myPlayer] != null)
			{
				return Main.player[Main.myPlayer].name;
			}
			return null;
		}
	}

	public static Player MAINPLAYER
	{
		get
		{
			if (Main.netMode != NetmodeID.Server)
			{
				return Main.player[Main.myPlayer];
			}
			return null;
		}
	}
}
