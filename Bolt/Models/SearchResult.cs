using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bolt.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        [Required]
        /// <summary>
        /// Bing/Google
        /// </summary>
        public string SearchEngine { get; set; }
        [Required]
        /// <summary>
        /// Result title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Date the value entered
        /// </summary>
        public DateTime EnteredDate { get; set; }
    }
}
