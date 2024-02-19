using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace FitnessAI.Application.Tickets.Commands.AddTicket;

// String zwraca id karnetu, który zostanie dodany do bazy danych
public class AddTicketCommand : IRequest<string>
{
    [Required(ErrorMessage = "Pole 'Początek' jest wymagane")]
    [DisplayName("Początek")]
    public DateTime StartDate { get; set; }

    [DisplayName("Cena")]
    public decimal Price { get; set; }

    [DisplayName("Typ karnetu")]
    public int TicketTypeId { get; set; }

    public string UserId { get; set; }
}