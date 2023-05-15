using System.ComponentModel.DataAnnotations;

namespace BlazorServerMaui.WebComponents.Data.Entities;

public class CodicePersonale
{
    [Required(ErrorMessage = "Il campo codice personale non può essere vuoto")]
    public string CodPersonale { get; set; } = string.Empty;
}