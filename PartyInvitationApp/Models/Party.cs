using PartyInvitationApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Party
{
    public int PartyId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    public string Location { get; set; }

    public List<Invitation> Invitations { get; set; } = new List<Invitation>();
}
