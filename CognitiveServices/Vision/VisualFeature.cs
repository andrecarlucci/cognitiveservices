namespace CognitiveServices.Vision {
    public enum VisualFeature {
        /// <summary>
        /// Clipart or a line drawing.
        /// </summary>
        ImageType,

        /// <summary>
        /// Accent color, dominant color, and whether an image is monochromatic.
        /// </summary>
        Color,

        /// <summary>
        /// Faces, if present, coordinates, gender and age.
        /// </summary>
        Faces,

        /// <summary>
        /// Raciness; pornographic in nature (nudity or sex act). Sexually suggestive content is also detected.
        /// </summary>
        Adult,

        /// <summary>
        /// Image categorizations; taxonomy defined in documentation.
        /// </summary>
        Categories,

        /// <summary>
        /// Image tags.
        /// </summary>
        Tags,

        /// <summary>
        /// Image description.
        /// </summary>
        Description
    }
}