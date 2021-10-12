namespace Wayway.Engine.Audio
{
    [System.Flags]
    public enum AudioType
    {        
        None            = 0,
        Bgm             = 1 << 0,
        SfxGeneral      = 1 << 1,
        SfxProjectile   = 1 << 2,
        SfxUI           = 1 << 3,
        Bumper          = 1 << 4,
        All             = int.MaxValue
    }
}