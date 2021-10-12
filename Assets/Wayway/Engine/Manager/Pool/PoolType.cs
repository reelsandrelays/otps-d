namespace Wayway.Engine.Pool
{
    [System.Flags]
    public enum PoolType
    {
        None            = 0,

        ToyBox          = 1 << 0,
        Tower           = 1 << 2,
        Projectile      = 1 << 3,
        SubProjectile   = 1 << 4,
        Particle        = 1 << 5,
        Monster         = 1 << 6,
        UI              = 1 << 7,
        UIParticles     = 1 << 8,

        All = int.MaxValue
    }
}