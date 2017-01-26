namespace P16_StepFunctions.ViewModels
{
    /// <summary>
    /// Class for arithmetic sign data.
    /// </summary>
    public class ArithmeticSignData
    {
        ///// <summary>
        ///// The constructor.
        ///// </summary>
        //public ArithmeticSignData()
        //{
        //    // Do nothing.
        //    // A parameterless constructor is needed so that the DataGrid-property CanUserAddRows works.(Really needed here?)
        //}

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public ArithmeticSignData(string key, string value)
        {
            ArithmeticSignKey = key;
            ArithmeticSignValue = value;
        }

        /// <summary>
        /// The key of the arithmetic sign.
        /// </summary>
        public string ArithmeticSignKey { get; set; }

        /// <summary>
        /// The value of the arithmetic sign.
        /// </summary>
        public string ArithmeticSignValue { get; set; }

    }
}
