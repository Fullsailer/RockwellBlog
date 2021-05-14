using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockwellBlog.Data;
using RockwellBlog.Models;
using RockwellBlog.Enums;

namespace RockwellBlog.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;
        public SearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IOrderedQueryable<Post> SerachContent(string searchString)
        {
            //Step 1: Get an IQueryable that contains all the Posts in the event that
            //the user does not supply a search string
            var result = _context.Posts.Where(p => p.PublishState == Enums.PublishState.ProductionReady);


            
            //c.Moderated == null &&
            //c.Author.FullName.Contains(searchString) ||
                if (!string.IsNullOrEmpty(searchString))
            {

            result = result.Where(p => p.Title.Contains(searchString) ||
                                       p.Abstract.Contains(searchString) ||
                                       p.Content.Contains(searchString) ||
                                       p.Comments.Any(c => c.Body.Contains(searchString) ||
                                                           c.ModeratedBody.Contains(searchString) ||
                                                           c.Author.FirstName.Contains(searchString) ||
                                                           c.Author.LastName.Contains(searchString) ||
                                                           c.Author.Email.Contains(searchString)));
            }
            return result.OrderByDescending(p => p.Created); 
        }
    }
}
