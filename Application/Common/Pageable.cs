using System.Collections.Generic;

namespace Application.Common
{
    public class Pageable<T>
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IList<T> Items { get; set; }
    }
}