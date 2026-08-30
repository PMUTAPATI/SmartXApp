using System.Collections.Generic;

namespace SmartXApp.Services
{
    public class BatchProcessorService
    {
        public List<double> ProcessHistoricalBatches(double[][] jaggedBatches)
        {
            List<double> resultList = new List<double>();
            for (int batch = 0; batch < jaggedBatches.Length; batch++)
            {
                for (int sample = 0; sample < jaggedBatches[batch].Length; sample++)
                {
                    resultList.Add(jaggedBatches[batch][sample]);
                }
            }
            return resultList;
        }
    }
}