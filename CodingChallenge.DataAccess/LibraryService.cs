using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.Utilities;
using System.Text.RegularExpressions;

namespace CodingChallenge.DataAccess
{
    public class LibraryService : ILibraryService
    {
        public LibraryService() { }

        private IEnumerable<Movie> GetMovies()
        {
            return _movies ?? (_movies = ConfigurationManager.AppSettings["LibraryPath"].FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = "ID", SortDirection sortDirection = SortDirection.Ascending)
        {
            var movies = GetMovies().Where(s => s.Title.Contains(title)).DistinctBy(x => x.Title);
            
            if (sortColumn == "Title")
                movies = (sortDirection == SortDirection.Descending) ? movies.OrderByDescending(x => ExcludeLeadingArticles(x.Title)) : movies.OrderBy(x => ExcludeLeadingArticles(x.Title));
            else
                movies = movies.OrderBy(sortColumn, sortDirection);

            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }
            return movies.ToList();
        }

        private string ExcludeLeadingArticles(string title)
        {
            //leading articles (a/an/the)
            if (String.IsNullOrEmpty(title))
                return title;

            if (title.StartsWith("a ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(2).TrimStart();

            if (title.StartsWith("an ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(3).TrimStart();

            if (title.StartsWith("the ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(4).TrimStart();

            return title;
        }
    }

    public static class EnumerablePropertyAccessorExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string property, SortDirection sortDirection)
        {
                return (sortDirection == SortDirection.Descending) ? enumerable.OrderByDescending(x => GetProperty(x, property)) :
                    enumerable.OrderBy(x => GetProperty(x, property));
        }

        private static object GetProperty(object o, string propertyName)
        {
            return o.GetType().GetProperty(propertyName).GetValue(o, null);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
    
}
