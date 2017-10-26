using System;
using System.Collections.Generic;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Core.Scene.Room
{
    public class RoomModel
    {
        public List<RoomModel> AlternateRooms { get; set; }
        public byte EchoEffect { get; set; }
        public RoomBehaviorModel RoomBehavior { get; set; }
        public SkyboxModifierModel SkyboxModifier { get; set; }
        public TimeSettingModel TimeSetting { get; set; }
        public MeshModel Mesh { get; set; }
        public List<short> Objects { get; set; }
        public List<ActorEntryModel> Actors { get; set; }
        public WindSettingModel WindSetting { get; set; }
        public List<LightSourceModel> LightSources { get; set; }
    }

    public class WindSettingModel
    {
        public byte WestwardStrength { get; set; }
        public byte VerticalStrength { get; set; }
        public byte SouthernStrength { get; set; }
        public byte ClothFlexibility { get; set; }
    }

    public class RoomBehaviorModel
    {
        public enum ActionRestrictionSettings
        {
            None = 0x00,
            NoSunSong = 0x01,
            NoJumping = 0x02,
            BossRoom = 0x05
        }

        public enum WarpSongAndInvisbleActorRestrictionMask
        {
            DisableWarpSongs = 0xF0,
            HideInvisibleActors = 0x00,
            ShowInvisibleActors = 0x01
        }

        public enum IdleAnimationSettings
        {
            GlanceBack = 0x00,
            ColdSneeze = 0x01,
            WipeHead = 0x02,
            TooHotTrigger = 0x03,
            StretchYawn = 0x04,
            Unknown = 0x05,
            Unknown2 = 0x06,
            BendOverGasping = 0x07,
            Unknown3 = 0x08,
            BrandishSword = 0x09,
            AdjustTunic = 0x0A
        }

        public ActionRestrictionSettings ActionRestrictionSetting { get; set; }
        public List<WarpSongAndInvisbleActorRestrictionMask> WarpSongAndInvisibleActorRestrictions { get; set; }
        public IdleAnimationSettings IdleAnimationSetting { get; set; }
    }

    public class TimeSettingModel
    {
        public static ushort CurrentGameTime { get; set; } = 0xFFFF;
        public static byte DefaultTimeSpeed { get; } = 0x0A;
        
        public ushort StartTime { get; set; }
        public byte TimeSpeed { get; set; }
    }

    public class SkyboxModifierModel
    {
        public bool DisableSkybox { get; set; }
        public bool DisableSunMoon { get; set; }
    }

    // TODO: Not sure if I'm going to support this. The encoding violates several SOLID principles (might actually be why the devs cut it out)
    public class LightSourceModel
    {
        public enum LightSourceTypes : byte
        {
            Directional = 0x01,
            Point = 0x02
        }

        public LightSourceTypes LightSourceType { get; set; }
        public Vector3D<short> PositionOrDirection { get; set; }
        public ColorRGB<byte> Color { get; set; }
        public byte Attenuation { get; set; }
    }
}
