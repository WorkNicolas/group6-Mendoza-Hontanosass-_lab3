using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalPodcasters { get; set; }
        public int TotalListeners { get; set; }
        public int TotalPodcasts { get; set; }
        public int TotalEpisodes { get; set; }
        public int PendingPodcasts { get; set; }
        public int PendingEpisodes { get; set; }

    }
}
