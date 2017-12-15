using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestAngularWithCore_Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] BookNames = new string[]
        {
            "The Chronicles of Narnia", "Romeo and Juliet", "Harry Potter and the Deathly Hallows (Harry Potter #7)", "The Hobbit", "The Help",
                "Life of Pi", "The Historian", "Eye of the Needle", "Mexico Set", "The Fault in Our Stars", "Speak", "The Outsiders",
                "Dreamland"
        };

        private static string[] Authors = new string[]
        {
            "C.S. Lewis, Pauline Baynes (Illustrator)", "William Shakespeare, Barbara A. Mowat (editor), Paul Werstine (Editor)",
                "J.K. Rowling, Mary GrandPré (Illustrator)", "J.R.R. Tolkien", "Kathryn Stockett", "Yann Martel", "Elizabeth Kostova",
                "Ken Follett", "Len Deighton", "John Green", "Laurie Halse Anderson", "S.E. Hinton", "Sarah Dessen"
        };

        private static int[] NumberOfPages = new int[]
        {
            350, 402, 500, 350, 650, 380, 299, 315, 512, 468, 375, 424, 304
        };

        public static string[] DateOfPublication = new string[]
        {
            "02/04/2008", "20/10/2010", "05/01/2014", "30/06/2015", "12/03/2006", "14/09/2013", "04/07/2009", "25/11/2012", "21/12/2016", "16/08/2011", "06/01/2004", "19/06/2007", "17/05/2017"
        };

        public class BookResult
        {
            public int total { get; set; }
            public Book[] books { get; set; }
        }

        public class Book
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Authors { get; set; }
            public int NumOfPages { get; set; }
            public string DateOfPublication { get; set; }
        }

        [HttpGet("[action]")]
        public BookResult BookDetails(int page, int limit)
        {
            var rng = new Random();
            var data = GetBooks();
            var skip = (limit * (page - 1));
            var total = data.Count();
            var dataarray = data.OrderBy(b => b.Name).Skip(skip).Take(limit).ToArray();
            return new BookResult { total=total,books= dataarray };
        }

        private IEnumerable<Book> GetBooks()
        {
            var booksList = new List<Book>();
            for(var i = 0; i < 13; i++)
            {
                var id = i + 1;
                var book = new Book()
                {
                    Id = id.ToString(),
                    Name = BookNames[i],
                    Authors = Authors[i],
                    NumOfPages = NumberOfPages[i],
                    DateOfPublication = DateOfPublication[i]
                };
                booksList.Add(book);
            }
            return booksList;
        }

        [HttpPost("[action]")]
        public IActionResult UpdateBookDetails([FromBody]Book book)
        {
            return Json(book);
        }
    }
}
