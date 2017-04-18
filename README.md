# Microsoft Cognitive Services Client

.NETStandard client version of Microsoft Cognitive Services

Implemented:
- Emotions

```
using (var stream = File.OpenRead("faceImage.bmp")) {
    var client = new EmotionClient("YOUR API KEY");
    var result = await client.RecognizeAsync(stream);

    foreach (var face in result) {
        Console.WriteLine("Face:");
        foreach (var kv in face.Scores.ToRankedList()) {
            //anger: x, happiness: y, etc...
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }
    }
}
