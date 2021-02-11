namespace MetricDimensions
{
    public class DimensionAccumulatorManager
    {
        public DimensionAccumulator Get(DimensionNameTable names)
        {
            // TODO: should be using a pool
            return new DimensionAccumulator()
            {
                Names = names,
            };
        }
    }
}
