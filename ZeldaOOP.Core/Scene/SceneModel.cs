using System;
using System.Collections;
using System.Collections.Generic;
using ZeldaOOP.Core.Drawing;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Core.Scene
{
    public class SceneModel
    {
        public List<SceneModel> AlternateScenes { get; set; }
        public SoundSettingModel SoundSetting { get; set; }
        public List<RoomModel> Rooms { get; set; }
        public List<TransitionActorEntryModel> TransitionActors { get; set; }
        public CameraSettingModel CameraSetting { get; set; }
        public List<CollisionModel> Collision { get; set; }
        public List<EntranceModel> Entrances { get; set; }
        public GlobalObjectResources GlobalResourceObject { get; set; }
        public List<ActorEntryModel> StartPositions { get; set; }
        public SkyboxSettingModel SkyboxSetting { get; set; }
        public List<ushort> Exits { get; set; }
        public List<LightingSettingModel> LightingSettings { get; set; }
        public List<CutsceneModel> Cutscenes { get; set; }
        public GlobalTextResources GlobalTextResource { get; set; }
        public List<PathwayModel> Pathways { get; set; }
    }

    public enum GlobalTextResources
    {
        MessageField = 0x01,
        MessageYdan = 0x02
    }

    public enum GlobalObjectResources
    {
        GamplayKeep = 0x0001,
        GameplayFieldKeep = 0x0002,
        GameplayDangeonKeep = 0x0003
    }

    public class EntranceModel
    {
        public byte StartPositionIndex { get; set; }
        public byte RoomIndex { get; set; }
    }

    public class PathwayModel : IEnumerable<Point3D<short>>
    {
        public List<Point3D<short>> CheckPoints { get; set; }

        public IEnumerator<Point3D<short>> GetEnumerator()
        {
            return CheckPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class TransitionActorEntryModel
    {
        public enum CameraEffects
        {
            NormalEffect = 0x00,
            NormalEffect2 = 0x0F,
            NormalEffect3 = 0xFF
        }

        public byte FrontTransitionRoomIndex { get; set; }
        public CameraEffects FrontTransitionCameraEffect { get; set; }
        public byte BackTransitionRoomIndex { get; set; }
        public CameraEffects BackTransitionCameraEffect { get; set; }
        public ushort ActorNumber { get; set; }
        public Point3D<short> Position { get; set; }
        public short YRotation { get; set; }
        public ushort Variable { get; set; }
    }

    public class LightingSettingModel
    {
        public static ColorRGB<byte> DefaultAmbientLightColor { get; } = new ColorRGB<byte> { R = 0x46, G = 0x2D, B = 0x39 };
        public static ColorRGB<byte> DefaultUpsideDiffuseLightColor { get; } = new ColorRGB<byte> { R = 0x49, G = 0x49, B = 0x49 };
        public static Vector3D<byte> DefaultUpsideDiffuseLightDirection { get; } = new Vector3D<byte> { X = 0xB4, Y = 0x9A, Z = 0x8A };
        public static ColorRGB<byte> DefaultDownsideDiffuseLightColor { get; } = new ColorRGB<byte> { R = 0xB7, G = 0xB7, B = 0xB7 };
        public static Vector3D<byte> DefaultDownsideDiffuseLightDirection { get; } = new Vector3D<byte> { X = 0x14, Y = 0x14, Z = 0x3C };

        public ColorRGB<byte> AmbientLightColor { get; set; }
        public ColorRGB<byte> UpsideDiffuseLightColor { get; set; }
        public Vector3D<byte> UpsideDiffuseLightDirection { get; set; }
        public ColorRGB<byte> DownsideDiffuseLightColor { get; set; }
        public Vector3D<byte> DownsideDiffuseLightDirection { get; set; }
        public ColorRGB<byte> FogColor { get; set; }
        public ushort FogStart { get; set; }
        public ushort DrawDistance { get; set; }
    }

    public class SkyboxSettingModel
    {
        public enum OvercastSettings
        {
            Sunny = 0x0,
            Cloudy = 0x1
        }

        public enum TimeLightingSettings
        {
            Flowing = 0x0,
            FixedIndoor = 0x1
        }

        public byte SkyboxNumber { get; set; }
        public OvercastSettings OvercastSetting { get; set; }
        public TimeLightingSettings TimeLightingSetting { get; set; }
    }

    public class SoundSettingModel
    {
        public byte Reverb { get; set; }
        public byte NightTimeSFX { get; set; }
        public byte DaytimeMusicTrack { get; set; }
    }

    public class CameraSettingModel
    {
        public enum CameraMovementRestrictions
        {
            None = 0x00,
            NoCUp = 0x10,
            BirdsEyeCUp = 0x20,
            FixedBackgroundNoCUp = 0x30,
            RotatingBackgroundNoCUp = 0x40,
            ShootingGallery = 0x50
        }

        public enum WorldMapLocations
        {
            HyruleField = 0x00,
            KakarikoVillage = 0x01,
            KakarikoGraveyard = 0x02,
            ZorasRiver = 0x03,
            KokiriForest = 0x04,
            SacredForestMeadow = 0x05,
            LakeHylia = 0x06,
            ZorasDomain = 0x07,
            ZorasFountain = 0x08,
            GerudoValley = 0x09,
            LostWoods = 0x0A,
            DesertColossus = 0x0B,
            GerudosFortress = 0x0C,
            HauntedWasteland = 0x0D,
            Market = 0x0E,
            HyruleCastle = 0x0F,
            DeathMountainTrail = 0x10,
            DeathMountainCrater = 0x11,
            GoronCity = 0x12,
            LonLonRanch = 0x13,
            DampesGraveWindmill = 0x14,
            GanonsCastle = 0x15,
            GrottoFairyFountain = 0x16
        }

        public CameraMovementRestrictions CameraMovementRestriction { get; set; }
        public WorldMapLocations WorldMapLocation { get; set; }
    }
}
