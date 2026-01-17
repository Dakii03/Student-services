using System.Text.Json.Serialization;

public class StudentUpdateDto
{
    [JsonPropertyName("ime")]
    public string Ime { get; set; }

    [JsonPropertyName("prezime")]
    public string Prezime { get; set; }
}
