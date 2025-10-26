/// <summary>
/// Episode Search View Model
/// </summary>
/// <remarks>
/// View model for searching and filtering episodes
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class EpisodeSearchViewModel
    {
        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Podcast")]
        public int? PodcastID { get; set; }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Sort By")]
        public string SortBy { get; set; } = "ReleaseDate";

        public List<Episode> Results { get; set; } = new List<Episode>();
        public List<Podcast> AvailablePodcasts { get; set; } = new List<Podcast>();
    }
}
