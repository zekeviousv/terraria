using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms;

public class WormlikeDungeonRoom : DungeonRoom
{
	private ShapeData _innerShapeData = new ShapeData();

	private ShapeData _outerShapeData = new ShapeData();

	private int _floodedTileCount;

	public int InnerBoundsSizeMin;

	public int InnerBoundsSizeMax;

	public Vector2[] Positions;

	public WormlikeDungeonRoom(DungeonRoomSettings settings)
		: base(settings)
	{
	}

	public override void CalculateRoom(DungeonData data)
	{
		calculated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		WormlikeRoom(data, x, y, generating: false);
		calculated = true;
	}

	public override bool GenerateRoom(DungeonData data)
	{
		generated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		WormlikeRoom(data, x, y, generating: true);
		generated = true;
		return true;
	}

	public override int GetFloodedRoomTileCount()
	{
		return _floodedTileCount;
	}

	public override void FloodRoom(byte liquidType)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (_innerShapeData == null || Positions == null)
		{
			base.FloodRoom(liquidType);
			return;
		}
		_ = (WormlikeDungeonRoomSettings)settings;
		WorldUtils.Gen(Positions[0].ToPoint(), new ModShapes.All(_innerShapeData), Actions.Chain(new Modifiers.IsBelowHeight(base.Center.Y, inclusive: true), new Modifiers.IsNotSolid(), new Actions.SetLiquid(liquidType)));
	}

	public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (_innerShapeData == null || _outerShapeData == null || Positions == null || (calculated && !OuterBounds.Contains(x, y)))
		{
			return base.GetProtectionTypeFromPoint(x, y);
		}
		Point val = Positions[0].ToPoint();
		if (!_outerShapeData.Contains(x - val.X, y - val.Y))
		{
			return ProtectionType.None;
		}
		return ProtectionType.Walls;
	}

	public override bool IsInsideRoom(int x, int y)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (Positions == null)
		{
			return base.IsInsideRoom(x, y);
		}
		Point val = Positions[0].ToPoint();
		if (base.IsInsideRoom(x, y))
		{
			return _innerShapeData.Contains(x - val.X, y - val.Y);
		}
		return false;
	}

	public void WormlikeRoom(DungeonData data, int i, int j, bool generating)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		UnifiedRandom unifiedRandom = new UnifiedRandom(settings.RandomSeed);
		WormlikeDungeonRoomSettings wormlikeDungeonRoomSettings = (WormlikeDungeonRoomSettings)settings;
		ushort brickTileType = settings.StyleData.BrickTileType;
		ushort brickCrackedTileType = settings.StyleData.BrickCrackedTileType;
		ushort brickWallType = settings.StyleData.BrickWallType;
		Point val = default(Point);
		((Point)(ref val))._002Ector(i, j);
		if (base.Processed)
		{
			val = Positions[0].ToPoint();
		}
		int num = 9 + unifiedRandom.Next(3);
		int num2 = Math.Max(4, num / 5);
		if (base.Processed)
		{
			num = InnerBoundsSizeMax;
			num2 = InnerBoundsSizeMin;
		}
		int num3 = num;
		int num4 = 8;
		int num5 = num + num4;
		InnerBounds.SetBounds(val.X, val.Y, val.X, val.Y);
		OuterBounds.SetBounds(val.X, val.Y, val.X, val.Y);
		Vector2 val2 = val.ToVector2();
		Vector2 val3 = val2;
		List<Vector2> list = new List<Vector2>();
		if (base.Processed)
		{
			list.AddRange(Positions);
		}
		val2 = val3;
		Vector2 val4 = unifiedRandom.NextVector2CircularEdge(1f, 1f);
		Vector2 spinningpoint = val4;
		int firstSideIterations = wormlikeDungeonRoomSettings.FirstSideIterations;
		int num6 = 0;
		for (int k = 0; k < firstSideIterations; k++)
		{
			float num7 = (float)k / (float)firstSideIterations;
			num3 = (int)Utils.Lerp(num, num2, num7);
			num5 = num3 + num4;
			Point val5 = val2.ToPoint();
			OuterBounds.UpdateBounds(val5.X - num5, val5.Y - num5, val5.X + num5, val5.Y + num5);
			InnerBounds.UpdateBounds(val5.X - num3, val5.Y - num3, val5.X + num3, val5.Y + num3);
			_outerShapeData.AddBounds(val5.X - num5 - (int)val3.X, val5.Y - num5 - (int)val3.Y, val5.X + num5 - (int)val3.X, val5.Y + num5 - (int)val3.Y);
			_innerShapeData.AddBounds(val5.X - num3 - (int)val3.X, val5.Y - num3 - (int)val3.Y, val5.X + num3 - (int)val3.X, val5.Y + num3 - (int)val3.Y);
			if (!base.Processed)
			{
				list.Add(val2);
			}
			if (generating)
			{
				GenerateDungeonSquareRoom(data, Vector2D.op_Implicit(val5), brickTileType, brickCrackedTileType, brickWallType, num3, num4);
			}
			if (base.Processed)
			{
				num6++;
				if (num6 < Positions.Length)
				{
					val2 = Positions[num6];
				}
			}
			else
			{
				val2 += val4;
				val4 = spinningpoint.RotatedBy(Utils.Lerp(0.0, 1.5707963705062866, num7));
			}
		}
		val2 = val3;
		val4 = spinningpoint.RotatedBy(3.1415927410125732, Vector2.Zero).RotatedByRandom(0.7853981852531433);
		spinningpoint = val4;
		firstSideIterations = wormlikeDungeonRoomSettings.SecondSideIterations;
		for (int l = 0; l < firstSideIterations; l++)
		{
			float num8 = (float)l / (float)firstSideIterations;
			num3 = (int)Utils.Lerp(num, num2, num8);
			num5 = num3 + num4;
			Point val6 = val2.ToPoint();
			OuterBounds.UpdateBounds(val6.X - num5, val6.Y - num5, val6.X + num5, val6.Y + num5);
			InnerBounds.UpdateBounds(val6.X - num3, val6.Y - num3, val6.X + num3, val6.Y + num3);
			_outerShapeData.AddBounds(val6.X - num5 - (int)val3.X, val6.Y - num5 - (int)val3.Y, val6.X + num5 - (int)val3.X, val6.Y + num5 - (int)val3.Y);
			_innerShapeData.AddBounds(val6.X - num3 - (int)val3.X, val6.Y - num3 - (int)val3.Y, val6.X + num3 - (int)val3.X, val6.Y + num3 - (int)val3.Y);
			if (!base.Processed)
			{
				list.Add(val2);
			}
			if (generating)
			{
				GenerateDungeonSquareRoom(data, Vector2D.op_Implicit(val6), brickTileType, brickCrackedTileType, brickWallType, num3, num4);
			}
			if (base.Processed)
			{
				num6++;
				if (num6 < Positions.Length)
				{
					val2 = Positions[num6];
				}
			}
			else
			{
				val2 += val4;
				val4 = spinningpoint.RotatedBy(Utils.Lerp(0.0, 1.5707963705062866, num8));
			}
		}
		Positions = Enumerable.ToArray(list);
		InnerBoundsSizeMin = num2;
		InnerBoundsSizeMax = num;
		InnerBounds.CalculateHitbox();
		OuterBounds.CalculateHitbox();
		_floodedTileCount = DungeonUtils.CalculateFloodedTileCountFromShapeData(InnerBounds, _innerShapeData);
	}
}
