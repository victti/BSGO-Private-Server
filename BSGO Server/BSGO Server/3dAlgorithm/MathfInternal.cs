namespace BSGO_Server._3dAlgorithm
{
    public struct MathfInternal
    {
        public static volatile float FloatMinNormal = 1.17549435E-38f;

        public static volatile float FloatMinDenormal = float.Epsilon;

        public static bool IsFlushToZeroEnabled = (int)FloatMinDenormal == (int)0f;
    }
}
