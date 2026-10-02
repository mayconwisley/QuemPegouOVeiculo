using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public static class ChecklistPhases
{
    public const string Departure = "departure";
    public const string Arrival = "arrival";
    public static bool IsValid(string? phase) => phase is Departure or Arrival;
}

public sealed class MovementChecklist : IEntity
{
    private MovementChecklist() { }

    public MovementChecklist(int movementId, string phase, bool tiresOk, bool lightsOk,
        bool fluidsOk, bool bodyOk, string? notes, DateTime checkedAtUtc)
    {
        MovementId = Guard.PositiveId(movementId, "Movimentação");
        if (!ChecklistPhases.IsValid(phase))
            throw new DomainException("Etapa do checklist inválida.");
        Phase = phase;
        Update(tiresOk, lightsOk, fluidsOk, bodyOk, notes, checkedAtUtc);
    }

    public int Id { get; private set; }
    public int MovementId { get; private set; }
    public string Phase { get; private set; } = "";
    public bool TiresOk { get; private set; }
    public bool LightsOk { get; private set; }
    public bool FluidsOk { get; private set; }
    public bool BodyOk { get; private set; }
    public string Notes { get; private set; } = "";
    public DateTime CheckedAtUtc { get; private set; }

    public void Update(bool tiresOk, bool lightsOk, bool fluidsOk, bool bodyOk,
        string? notes, DateTime checkedAtUtc)
    {
        var validatedNotes = Guard.Optional(notes, "Observações do checklist", 2000);
        var validatedCheckedAt = Guard.Utc(checkedAtUtc, "Data do checklist");
        TiresOk = tiresOk;
        LightsOk = lightsOk;
        FluidsOk = fluidsOk;
        BodyOk = bodyOk;
        Notes = validatedNotes;
        CheckedAtUtc = validatedCheckedAt;
    }
}
