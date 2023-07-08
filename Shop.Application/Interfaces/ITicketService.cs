using Shop.Domain.ViewModels.Ticket;

namespace Shop.Application.Interfaces
{
    public interface ITicketService
    {
        #region ticket

        Task<AddTicketResult> AddUserTicket(AddTicketViewModel ticket, int userId);
        Task<FilterTicketViewModel> FilterTickets(FilterTicketViewModel filter);
        Task<TicketDetailViewModel?> GetTicketForShow(int ticketId, int userId);
        Task<TicketDetailViewModel?> GetTicketForShowAdmin(int ticketId);
        Task<AnswerTicketResult> AnswerTicket(AnswerTicketViewModel answer, int userId);
        Task<AnswerTicketResult> AnswerTicketFromAdmin(AnswerTicketViewModel answer, int userId);
        Task<bool> ClosedTicketByTicketId(int ticketId);

        #endregion
    }
}
