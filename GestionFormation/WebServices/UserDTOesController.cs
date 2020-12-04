using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GestionFormation.DAO;
using GestionFormation.DTO;

namespace GestionFormation.WebServices
{
    public class UserDTOesController : ApiController
    {
        private BDDContext db = new BDDContext();

        // GET: api/UserDTOes
        public IQueryable<UserDTO> GetUserDTOes()
        {
            return db.UserDTOes;
        }

        // GET: api/UserDTOes/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetUserDTO(int id)
        {
            UserDTO userDTO = await db.UserDTOes.FindAsync(id);
            if (userDTO == null)
            {
                return NotFound();
            }

            return Ok(userDTO);
        }

        // PUT: api/UserDTOes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserDTO(int id, UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            db.Entry(userDTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDTOExists(id))
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

        // POST: api/UserDTOes
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> PostUserDTO(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserDTOes.Add(userDTO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userDTO.Id }, userDTO);
        }

        // DELETE: api/UserDTOes/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> DeleteUserDTO(int id)
        {
            UserDTO userDTO = await db.UserDTOes.FindAsync(id);
            if (userDTO == null)
            {
                return NotFound();
            }

            db.UserDTOes.Remove(userDTO);
            await db.SaveChangesAsync();

            return Ok(userDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDTOExists(int id)
        {
            return db.UserDTOes.Count(e => e.Id == id) > 0;
        }
    }
}