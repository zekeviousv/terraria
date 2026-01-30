using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms;

public class LivingTreeDungeonRoom : DungeonRoom
{
	private ShapeData _innerShapeData = new ShapeData();

	private ShapeData _outerShapeData = new ShapeData();

	private int _floodedTileCount;

	private Point BasePosition;

	public LivingTreeDungeonRoom(DungeonRoomSettings settings)
		: base(settings)
	{
	}

	public override void CalculateRoom(DungeonData data)
	{
		calculated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		LivingTreeRoom(data, x, y, generating: false);
		calculated = true;
	}

	public override bool GenerateRoom(DungeonData data)
	{
		generated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		LivingTreeRoom(data, x, y, generating: true);
		generated = true;
		return true;
	}

	public override int GetFloodedRoomTileCount()
	{
		return _floodedTileCount;
	}

	public override void FloodRoom(byte liquidType)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (_innerShapeData == null)
		{
			base.FloodRoom(liquidType);
			return;
		}
		_ = (WormlikeDungeonRoomSettings)settings;
		WorldUtils.Gen(BasePosition, new ModShapes.All(_innerShapeData), Actions.Chain(new Modifiers.IsBelowHeight(base.Center.Y, inclusive: true), new Modifiers.IsNotSolid(), new Actions.SetLiquid(liquidType)));
	}

	public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (_innerShapeData == null || _outerShapeData == null || (calculated && !OuterBounds.Contains(x, y)))
		{
			return base.GetProtectionTypeFromPoint(x, y);
		}
		Point basePosition = BasePosition;
		if (!_outerShapeData.Contains(x - basePosition.X, y - basePosition.Y))
		{
			return ProtectionType.None;
		}
		return ProtectionType.Walls;
	}

	public override bool IsInsideRoom(int x, int y)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Point basePosition = BasePosition;
		if (base.IsInsideRoom(x, y))
		{
			return _innerShapeData.Contains(x - basePosition.X, y - basePosition.Y);
		}
		return false;
	}

	public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		UnifiedRandom unifiedRandom = new UnifiedRandom(settings.RandomSeed);
		ushort brickTileType = settings.StyleData.BrickTileType;
		ushort brickCrackedTileType = settings.StyleData.BrickCrackedTileType;
		int growthLength = (int)((float)InnerBounds.Height * 0.1f) + unifiedRandom.Next(4);
		int branchDensity = 2 + unifiedRandom.Next(2);
		int leafDensity = 3 + unifiedRandom.Next(4);
		Point startPoint = default(Point);
		((Point)(ref startPoint))._002Ector(InnerBounds.Center.X, InnerBounds.Top);
		DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, OuterBounds, startPoint, growthLength, branchDensity, leafDensity, brickCrackedTileType, brickTileType, settings.OverridePaintTile, settings.OverridePaintTile);
		growthLength = (int)((float)InnerBounds.Height * 0.15f) + unifiedRandom.Next(5);
		branchDensity = 3 + unifiedRandom.Next(2);
		leafDensity = 4 + unifiedRandom.Next(4);
		((Point)(ref startPoint))._002Ector(InnerBounds.Left + 2 + unifiedRandom.Next(3), InnerBounds.Top);
		DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, OuterBounds, startPoint, growthLength, branchDensity, leafDensity, brickCrackedTileType, brickTileType, settings.OverridePaintTile, settings.OverridePaintTile);
		growthLength = (int)((float)InnerBounds.Height * 0.15f) + unifiedRandom.Next(5);
		branchDensity = 3 + unifiedRandom.Next(2);
		leafDensity = 4 + unifiedRandom.Next(4);
		((Point)(ref startPoint))._002Ector(InnerBounds.Right - 2 - unifiedRandom.Next(3), InnerBounds.Top);
		DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, OuterBounds, startPoint, growthLength, branchDensity, leafDensity, brickCrackedTileType, brickTileType, settings.OverridePaintTile, settings.OverridePaintTile);
		base.GenerateEarlyDungeonFeaturesInRoom(data);
	}

	public override void GenerateLateDungeonFeaturesInRoom(DungeonData data)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		UnifiedRandom unifiedRandom = new UnifiedRandom(settings.RandomSeed);
		LivingTreeDungeonRoomSettings livingTreeDungeonRoomSettings = (LivingTreeDungeonRoomSettings)settings;
		ushort brickTileType = settings.StyleData.BrickTileType;
		ushort brickCrackedTileType = settings.StyleData.BrickCrackedTileType;
		ushort brickWallType = settings.StyleData.BrickWallType;
		for (int i = 0; i < 50; i++)
		{
			int num = unifiedRandom.Next(InnerBounds.Left + 1, InnerBounds.Right);
			int num2 = unifiedRandom.Next(InnerBounds.Top + 1, InnerBounds.Bottom);
			Point val = DungeonUtils.FirstSolid(ceiling: false, new Point(num, num2), InnerBounds);
			num = val.X;
			num2 = val.Y - 1;
			Tile tile = Main.tile[num, num2];
			if (tile.active() || tile.wall != brickWallType)
			{
				continue;
			}
			if (unifiedRandom.Next(2) == 0)
			{
				WorldGen.PlaceTile(num, num2, 187, mute: true, forced: false, -1, unifiedRandom.Next(47, 50));
				continue;
			}
			int num3 = unifiedRandom.Next(2);
			int pileStyle = 72;
			if (num3 == 1)
			{
				pileStyle = unifiedRandom.Next(59, 62);
			}
			WorldGen.PlaceSmallPile(num, num2, pileStyle, num3, 185);
		}
		for (int j = 0; j < 10; j++)
		{
			int num4 = unifiedRandom.Next(InnerBounds.Left + 1, InnerBounds.Right);
			int num5 = unifiedRandom.Next(InnerBounds.Top + 1, InnerBounds.Bottom);
			Point val2 = DungeonUtils.FirstSolid(ceiling: true, new Point(num4, num5), InnerBounds);
			num4 = val2.X;
			num5 = val2.Y + 1;
			Tile tile2 = Main.tile[num4, num5];
			Tile tile3 = Main.tile[num4, num5 - 1];
			if (tile2.active() || tile2.wall != brickWallType || !tile3.active() || tile3.type != brickCrackedTileType)
			{
				continue;
			}
			ushort type = 52;
			if (brickTileType == 383)
			{
				type = 62;
			}
			for (int num6 = unifiedRandom.Next(3, 12); num6 > 0; num6--)
			{
				Tile tile4 = Main.tile[num4, num5];
				if (tile4.active())
				{
					break;
				}
				tile4.ClearTile();
				tile4.active(active: true);
				tile4.type = type;
				if (livingTreeDungeonRoomSettings.OverridePaintTile > -1)
				{
					WorldGen.paintTile(num4, num5, (byte)livingTreeDungeonRoomSettings.OverridePaintTile, broadCast: false, paintEffects: false);
				}
				num5++;
			}
		}
	}

	public void LivingTreeRoom(DungeonData data, int i, int j, bool generating)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		UnifiedRandom unifiedRandom = new UnifiedRandom(settings.RandomSeed);
		LivingTreeDungeonRoomSettings livingTreeDungeonRoomSettings = (LivingTreeDungeonRoomSettings)settings;
		ushort brickTileType = settings.StyleData.BrickTileType;
		ushort brickCrackedTileType = settings.StyleData.BrickCrackedTileType;
		ushort brickWallType = settings.StyleData.BrickWallType;
		Point basePosition = default(Point);
		((Point)(ref basePosition))._002Ector(i, j);
		if (calculated)
		{
			basePosition = BasePosition;
		}
		Point val = default(Point);
		((Point)(ref val))._002Ector(basePosition.X, basePosition.Y + livingTreeDungeonRoomSettings.InnerHeight / 2);
		int num = val.Y - livingTreeDungeonRoomSettings.InnerHeight;
		int innerWidth = livingTreeDungeonRoomSettings.InnerWidth;
		int depth = livingTreeDungeonRoomSettings.Depth;
		int num2 = innerWidth;
		int num3 = num2 + depth;
		OuterBounds.SetBounds(basePosition.X, basePosition.Y, basePosition.X, basePosition.Y);
		InnerBounds.SetBounds(basePosition.X, basePosition.Y, basePosition.X, basePosition.Y);
		while (val.Y > num)
		{
			OuterBounds.UpdateBounds(val.X - num3, val.Y - num3, val.X + num3, val.Y + num3);
			InnerBounds.UpdateBounds(val.X - num2, val.Y - num2, val.X + num2, val.Y + num2);
			_outerShapeData.AddBounds(val.X - num3 - basePosition.X, val.Y - num3 - basePosition.Y, val.X + num3 - basePosition.X, val.Y + num3 - basePosition.Y);
			_innerShapeData.AddBounds(val.X - num2 - basePosition.X, val.Y - num2 - basePosition.Y, val.X + num2 - basePosition.X, val.Y + num2 - basePosition.Y);
			if (generating)
			{
				GenerateDungeonSquareRoom(data, Vector2D.op_Implicit(val), brickTileType, brickCrackedTileType, brickWallType, livingTreeDungeonRoomSettings.InnerWidth, livingTreeDungeonRoomSettings.Depth);
			}
			if (val.Y % 4 == 0)
			{
				val.X += ((unifiedRandom.Next(2) != 0) ? 1 : (-1));
			}
			val.Y--;
		}
		InnerBounds.CalculateHitbox();
		OuterBounds.CalculateHitbox();
		BasePosition = basePosition;
		_floodedTileCount = DungeonUtils.CalculateFloodedTileCountFromShapeData(InnerBounds, _innerShapeData);
	}
}
