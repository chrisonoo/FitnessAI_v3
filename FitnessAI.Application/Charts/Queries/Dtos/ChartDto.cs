namespace FitnessAI.Application.Charts.Queries.Dtos;

public class ChartDto
{
    public string Label { get; set; }
    public List<ChartPositionDto> Positions { get; set; } = new();
}