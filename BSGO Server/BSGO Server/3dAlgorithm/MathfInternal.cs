namespace BSGO_Server._3dAlgorithm
{
    public struct MathfInternal
    {
        public static volatile float FloatMinNormal = 1.17549435E-38f;

        public static volatile float FloatMinDenormal = float.Epsilon;

        public static bool IsFlushToZeroEnabled = FloatMinDenormal == 0f;
    }
}
