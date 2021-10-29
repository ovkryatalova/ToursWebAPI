using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ToursWebAPI.Entities;

namespace ToursWebAPI.Controllers
{
    public class HottelCommentsController : ApiController
    {
        private TourseBaseEntities db = new TourseBaseEntities();

        // GET: api/HottelComments
        public IQueryable<HottelComment> GetHottelComment()
        {
            return db.HottelComment;
        }

        [Route("api/getHotelComments")]
        public IHttpActionResult GetHottelComments(int hotelId)
        {
            var hotelComments = db.HottelComment.ToList().Where(p => p.HottelId == hotelId).ToList();
            return Ok(hotelComments);
        }
        // GET: api/HottelComments/5
        [ResponseType(typeof(HottelComment))]
        public IHttpActionResult GetHottelComment(int id)
        {
            HottelComment hottelComment = db.HottelComment.Find(id);
            if (hottelComment == null)
            {
                return NotFound();
            }

            return Ok(hottelComment);
        }

        // PUT: api/HottelComments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHottelComment(int id, HottelComment hottelComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hottelComment.Id)
            {
                return BadRequest();
            }

            db.Entry(hottelComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HottelCommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HottelComments
        [ResponseType(typeof(HottelComment))]
        public IHttpActionResult PostHottelComment(HottelComment hottelComment)
        {
            hottelComment.CreationDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(hottelComment.Author) || hottelComment.Author.Length > 100)
                ModelState.AddModelError("Author", "Author is required string up to 100 symbols.");
            if (string.IsNullOrWhiteSpace(hottelComment.Text))
                ModelState.AddModelError("Text", "Text is reqired string.");
            if (!(db.Hotel.ToList().FirstOrDefault(p => p.Id == hottelComment.HottelId) is Hotel))
                ModelState.AddModelError("HotelId", "HotelId is hotel`s Id from database.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HottelComment.Add(hottelComment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HottelCommentExists(hottelComment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hottelComment.Id }, hottelComment);
        }

        // DELETE: api/HottelComments/5
        [ResponseType(typeof(HottelComment))]
        public IHttpActionResult DeleteHottelComment(int id)
        {
            HottelComment hottelComment = db.HottelComment.Find(id);
            if (hottelComment == null)
            {
                return NotFound();
            }

            db.HottelComment.Remove(hottelComment);
            db.SaveChanges();

            return Ok(hottelComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HottelCommentExists(int id)
        {
            return db.HottelComment.Count(e => e.Id == id) > 0;
        }
    }
}