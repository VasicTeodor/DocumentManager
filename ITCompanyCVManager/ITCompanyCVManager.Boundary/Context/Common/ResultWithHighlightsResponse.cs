namespace ITCompanyCVManager.Boundary.Context.Common;

public class ResultWithHighlightsResponse
{
    public ApplicationResponse Application { get; set; }
    public List<string> Highlights { get; set; }
}