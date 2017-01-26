namespace P16_StepFunctions.ViewModels
{
    /// <summary>
    /// Class for data of stepfunctions.
    /// </summary>
    public class StepData
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public StepData()
        {
            // Do nothing.
            // A parameterless constructor is needed so that the DataGrid-property CanUserAddRows works.
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="lowerComparer">The arithmetic comparer for the lower bound.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="stepValue">The assigned value.</param>
        public StepData(string lowerComparer, double lowerBound, double stepValue)
        {
            LowerComparer = lowerComparer;
            LowerBound = lowerBound;
            StepValue = stepValue;
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="lowerComparer">The arithmetic comparer for the lower bound.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="stepValue">The assigned value.</param>
        /// <param name="selectedLowerComparer">The selected arithmetic comparer for the lower bound.</param>
        public StepData(string lowerComparer, double lowerBound, double stepValue, ArithmeticSignData selectedLowerComparer)
        {
            LowerComparer = lowerComparer;
            SelectedLowerComparer = selectedLowerComparer;
            LowerBound = lowerBound;
            StepValue = stepValue;
        }

        /// <summary>
        /// Gets or sets the lower comparer.
        /// </summary>
        public string LowerComparer { get; set; }

        /// <summary>
        /// Gets or sets the selected lower comparer.
        /// </summary>
        public ArithmeticSignData SelectedLowerComparer { get; set; }

        /// <summary>
        /// Gets or sets the lower bound.
        /// </summary>
        public double LowerBound { get; set; }

        /// <summary>
        /// Gets or sets the assigned value.
        /// </summary>
        public double StepValue { get; set; }

    }
}
