using System;
using System.Collections.Generic;
using System.Text;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Core.Scene
{
    public class CollisionModel
    {
        public List<PolygonModel> Polygons { get; set; }
        public SkyboxCameraModel SkyboxCamera { get; set; }
        public List<WaterBoxModel> WaterBoxes { get; set; }
    }

    public class SkyboxCameraModel
    {
        public Point3D<short> Position { get; set; }
        public Rotation3D<short> Rotation { get; set; }
        public byte Zoom { get; set; }
    }

    public class PolygonModel
    {
        public PolygonTypeModel PolygonType { get; set; }
        public Triangle3D<short> Triangle { get; set; }
    }

    public class PolygonTypeModel
    {
        public enum ObjectSetTriggerEffectMask : byte
        {
            Unknown = 0x01,
            Unknown2 = 0x02,
            Unknown3 = 0x04,
            Unknown4 = 0x08,
            Set0 = 0x00,
            Set1 = 0x10,
            Set2 = 0x20,
            Set3 = 0x30,
            Set4 = 0x40,
            Set5 = 0x50,
            Set6 = 0x60,
            Set7 = 0x70,
            Set8 = 0x80,
            Set9 = 0x90,
            Set10 = 0xA0,
            Set11 = 0xB0,
            Set12 = 0xC0,
            Set13 = 0xD0,
            Set14 = 0xE0,
            Set15 = 0xF0
        }

        public enum HorizontalCollisionEffectMask : ushort
        {
            Climbable = 0x001,
            Descendable = 0x002, 
            Crawlable = 0x004,
            Unknown = 0x008,
            Unknown2 = 0x010,
            Unknown3 = 0x020,
            Unknown4 = 0x040,
            JabuDamage = 0x080,
            OutOfBounds = 0x100,
            Harmful = 0x200,
            Unknown5 = 0x400,
            WarpTransition = 0x800
        }

        public enum ExitTriggerEffects : byte
        {
            Index0 = 0x0,
            Index1 = 0x1,
            Index2 = 0x2,
            Index3 = 0x3,
            Index4 = 0x4,
            Index5 = 0x5,
            Index6 = 0x6,
            Index7 = 0x7,
            Index8 = 0x8,
            Index9 = 0x9,
            Index10 = 0xA,
            Index11 = 0xB,
            Index12 = 0xC,
            Index13 = 0xD,
            Index14 = 0xE,
            Index15 = 0xF
        }

        public enum CameraAngleEffects : byte
        {
            Default = 0x0,
            HighAbove = 0x1,
            CloseBehind = 0x3,
            FarBehind = 0x4
        }

        public enum VerticalCollisionEffectMask : byte
        {
            Harmful = 0x1,
            Unknown = 0x2,
            Unknown2 = 0x4,
            Unknown3 = 0x8
        }

        public enum InteractionCollisionEffectMask : byte
        {
            Unknown = 0x1,
            Unknown1 = 0x2,
            Hookshotable = 0x4,
            Unknown2 = 0x8
        }

        public enum TerrainTypeEffects : byte
        {
            Walkable = 0x0,
            HighAngle = 0x1,
            Walkable2 = 0x2,
            Walkable3 = 0x3,
            Unknown = 0x4,
            Unknown2 = 0x5,
            Unknown3 = 0x6,
            Unknown4 = 0x7,
            Unknown5 = 0x8,
            Unknown6 = 0x9,
            Unknown7 = 0xA,
            Unknown8 = 0xB,
            Unknown9 = 0xC,
            Unknown10 = 0xD,
            Unknown11 = 0xE,
            Unknown12 = 0xF
        }

        public enum SoundEffects : byte
        {
            Earth = 0x0,
            Sand = 0x1,
            Stone = 0x2,
            WetStone = 0x3,
            ShallowWater = 0x4,
            ShallowWater2 = 0x5,
            Underbrush = 0x6,
            LavaGoo = 0x7,
            Earth2 = 0x8,
            Wooden = 0x9,
            PackedEarth = 0xA,
            Earth3 = 0xB,
            Ceramic = 0xC,
            LooseEarth = 0xD,
            Earth4 = 0xE,
            Earth5 = 0xF
        }

        public List<ObjectSetTriggerEffectMask> ObjectSetTriggerEffects { get; set; }
        public List<HorizontalCollisionEffectMask> HorizontalCollisionEffects { get; set; }
        public ExitTriggerEffects ExitTriggerEffect { get; set; }
        public CameraAngleEffects CameraAngleEffect { get; set; }
        public List<VerticalCollisionEffectMask> VerticalCollisionEffects { get; set; }
        public List<InteractionCollisionEffectMask> InteractionCollisionEffects { get; set; }
        public byte EchoEffect { get; set; }
        public byte AmbientLightEffect { get; set; }
        public TerrainTypeEffects TerrainTypeEffect { get; set; }
        public SoundEffects SoundEffect { get; set; }
    }


    public class WaterBoxModel
    {
        public Point3D<short> Position { get; set; }
        public ushort Length { get; set; }
        public ushort Width { get; set; }
    }
}
