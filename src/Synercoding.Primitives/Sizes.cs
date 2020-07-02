namespace Synercoding.Primitives
{
    /// <summary>
    /// A collection of default sizes, all in portrait orientation.
    /// </summary>
    public static class Sizes
    {
        /// <summary>
        /// A0, Based upon ISO 216
        /// </summary>
        public static Size A0 => new Size(841, 1189, Unit.Millimeters);
        /// <summary>
        /// A1, Based upon ISO 216
        /// </summary>
        public static Size A1 => new Size(594, 841, Unit.Millimeters);
        /// <summary>
        /// A2, Based upon ISO 216
        /// </summary>
        public static Size A2 => new Size(420, 594, Unit.Millimeters);
        /// <summary>
        /// A3, Based upon ISO 216
        /// </summary>
        public static Size A3 => new Size(297, 420, Unit.Millimeters);
        /// <summary>
        /// A4, Based upon ISO 216
        /// </summary>
        public static Size A4 => new Size(210, 297, Unit.Millimeters);
        /// <summary>
        /// A5, Based upon ISO 216
        /// </summary>
        public static Size A5 => new Size(148, 210, Unit.Millimeters);
        /// <summary>
        /// A6, Based upon ISO 216
        /// </summary>
        public static Size A6 => new Size(105, 148, Unit.Millimeters);

        /// <summary>
        /// Based upon cCommon American loose sizes
        /// </summary>
        public static Size HalfLetter => new Size(5.5, 8.5, Unit.Inches);
        /// <summary>
        /// Based upon cCommon American loose sizes
        /// </summary>
        public static Size Letter => new Size(8.5, 11, Unit.Inches);
        /// <summary>
        /// Based upon cCommon American loose sizes
        /// </summary>
        public static Size Legal => new Size(8.5, 14, Unit.Inches);
        /// <summary>
        /// Based upon cCommon American loose sizes
        /// </summary>
        public static Size JuniorLegal => new Size(5, 8, Unit.Inches);
        /// <summary>
        /// Based upon cCommon American loose sizes
        /// </summary>
        public static Size Ledger => new Size(11, 17, Unit.Inches);

        /// <summary>
        ///  ANSI A, based upon ANSI/ASME Y14.1
        /// </summary>
        public static Size AnsiA => new Size(8.5, 11, Unit.Inches);
        /// <summary>
        ///  ANSI B, based upon ANSI/ASME Y14.1
        /// </summary>
        public static Size AnsiB => new Size(11, 17, Unit.Inches);
        /// <summary>
        ///  ANSI C, based upon ANSI/ASME Y14.1
        /// </summary>
        public static Size AnsiC => new Size(17, 22, Unit.Inches);
        /// <summary>
        ///  ANSI D, based upon ANSI/ASME Y14.1
        /// </summary>
        public static Size AnsiD => new Size(22, 34, Unit.Inches);
        /// <summary>
        ///  ANSI E, based upon ANSI/ASME Y14.1
        /// </summary>
        public static Size AnsiE => new Size(34, 44, Unit.Inches);

        /// <summary>
        /// Super B, based upon ANSI B with 1 inch extra on all sides.
        /// </summary>
        public static Size SuperB => new Size(13, 19, Unit.Inches);
    }
}
