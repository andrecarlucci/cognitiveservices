using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CognitiveServices.Vision
{
    public interface IVisionClient {
        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, VisualFeatures.Categories will be analyzed.</param>
        /// <param name="details">Optional domain-specific models to invoke when appropriate.  To obtain names of models supported, invoke the <see cref="ListModelsAsync">ListModelsAsync</see> method.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(string url, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null);

        /// <summary>
        /// Analyzes the image.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="visualFeatures">The visual features. If none are specified, VisualFeatures.Categories will be analyzed.</param>
        /// <param name="details">Optional domain-specific models to invoke when appropriate.  To obtain names of models supported, invoke the <see cref="ListModelsAsync">ListModelsAsync</see> method.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisResult> AnalyzeImageAsync(Stream imageStream, IEnumerable<VisualFeature> visualFeatures = null, IEnumerable<string> details = null);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="model">Model representing the domain.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, Model model);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <param name="visualFeatures">The visual features.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, Model model);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(string url, string modelName);

        /// <summary>
        /// Analyzes the image using a domain-specific image analysis model.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <returns>The AnalysisResult object.</returns>
        Task<AnalysisInDomainResult> AnalyzeImageInDomainAsync(Stream imageStream, string modelName);

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<ModelResult> ListModelsAsync();

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<AnalysisResult> DescribeAsync(string url, int maxCandidates = 1);

        /// <summary>
        /// List domain-specific models currently supproted.
        /// </summary>
        /// <returns></returns>
        Task<AnalysisResult> DescribeAsync(Stream imageStream, int maxCandidates = 1);

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="smartCropping">if set to <c>true</c> [smart cropping].</param>
        /// <returns>The byte array.</returns>
        Task<byte[]> GetThumbnailAsync(string url, int width, int height, bool smartCropping = true);

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="smartCropping">if set to <c>true</c> [smart cropping].</param>
        /// <returns>The byte array.</returns>
        Task<byte[]> GetThumbnailAsync(Stream stream, int width, int height, bool smartCropping = true);

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="detectOrientation">if set to <c>true</c> [detect orientation].</param>
        /// <returns>The OCR object.</returns>
        Task<OcrResults> RecognizeTextAsync(string imageUrl, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true);

        /// <summary>
        /// Recognizes the text.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="detectOrientation">if set to <c>true</c> [detect orientation].</param>
        /// <returns>The OCR object.</returns>
        Task<OcrResults> RecognizeTextAsync(Stream imageStream, string languageCode = LanguageCodes.AutoDetect, bool detectOrientation = true);

        /// <summary>
        /// HandwritingRecognitionOperation
        /// </summary>
        /// <param name="imageUrl">Image url</param>
        /// <returns>HandwritingRecognitionOperation created</returns>
        Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(string imageUrl);

        /// <summary>
        /// Create HandwritingRecognitionOperation
        /// </summary>
        /// <param name="imageStream">Image content is byte array.</param>
        /// <returns>HandwritingRecognitionOperation created</returns>
        Task<HandwritingRecognitionOperation> CreateHandwritingRecognitionOperationAsync(Stream imageStream);

        /// <summary>
        /// Get HandwritingRecognitionOperationResult
        /// </summary>
        /// <param name="opeartion">HandwritingRecognitionOperation object</param>
        /// <returns>HandwritingRecognitionOperationResult</returns>
        Task<HandwritingRecognitionOperationResult> GetHandwritingRecognitionOperationResultAsync(HandwritingRecognitionOperation opeartion);

        /// <summary>
        /// Gets the tags associated with an image.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns>Analysis result with tags.</returns>
        Task<AnalysisResult> GetTagsAsync(string imageUrl);

        /// <summary>
        /// Gets the tags associated with an image.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>Analysis result with tags.</returns>
        Task<AnalysisResult> GetTagsAsync(Stream imageStream);
    }
}
