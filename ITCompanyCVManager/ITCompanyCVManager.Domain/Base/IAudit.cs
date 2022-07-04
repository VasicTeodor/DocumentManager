namespace ITCompanyCVManager.Domain.Base;

public interface IAudit
{
    public DateTime Created { get; }
    public DateTime Updated { get; }
}