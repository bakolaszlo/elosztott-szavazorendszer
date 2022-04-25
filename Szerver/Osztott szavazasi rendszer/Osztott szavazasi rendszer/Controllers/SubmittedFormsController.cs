﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Osztott_szavazasi_rendszer.Data;
using Osztott_szavazasi_rendszer.Models;

namespace Osztott_szavazasi_rendszer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmittedFormsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<VoteHub> hubContext;

        public SubmittedFormsController(AppDbContext context, IHubContext<VoteHub> hubContext)
        {
            _context = context;
            this.hubContext = hubContext;
        }

        // GET: api/SubmittedForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmittedForm>>> GetSubmittedForms()
        {
            return await _context.SubmittedForms.ToListAsync();
        }

        // GET: api/SubmittedForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubmittedForm>> GetSubmittedForm(int id)
        {
            var submittedForm = await _context.SubmittedForms.FindAsync(id);

            if (submittedForm == null)
            {
                return NotFound();
            }

            return submittedForm;
        }

        // GET: api/SubmittedForms/5
        [HttpGet("formId/{id}")]
        public ActionResult<IEnumerable<SubmittedForm>> GetSubmittedFormsById(int id)
        {
            var submittedForm = _context.SubmittedForms.Where(obj => obj.FormId == id);

            if (submittedForm == null)
            {
                return NotFound();
            }

            return submittedForm.ToArray();
        }

        // PUT: api/SubmittedForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubmittedForm(int id, SubmittedForm submittedForm)
        {
            if (id != submittedForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(submittedForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmittedFormExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubmittedForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubmittedForm>> PostSubmittedForm(SubmittedForm submittedForm)
        {
            _context.SubmittedForms.Add(submittedForm);
            hubContext.Clients.All.SendAsync("UpdateVote", submittedForm, submittedForm.FormId);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubmittedForm", new { id = submittedForm.Id }, submittedForm);
        }

        // DELETE: api/SubmittedForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmittedForm(int id)
        {
            var submittedForm = await _context.SubmittedForms.FindAsync(id);
            if (submittedForm == null)
            {
                return NotFound();
            }

            _context.SubmittedForms.Remove(submittedForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubmittedFormExists(int id)
        {
            return _context.SubmittedForms.Any(e => e.Id == id);
        }
    }
}
