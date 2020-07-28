using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class ThesesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly MyContext _context;

        public ThesesController(IHostingEnvironment hostingEnv, MyContext context)
        {
            _hostingEnv = hostingEnv;
            _context = context;
        }





        //Dropdown für Type
        IQueryable<string> types = (new string[] { "Bachelor", "Master" }).AsQueryable();

        public JsonResult GetTypes(string text)
        {
            return Json(types.ToList());
        }





        //Enum für Sortierfunktion
        public enum SortCriteria

        {
            [Display(Name = "ID")]
            Id,
            [Display(Name = "Titel")]
            Title,
            [Display(Name = "Status")]
            Status,
            [Display(Name = "Name des Studenten")]
            StudentName,
            [Display(Name = "Anmeldedatum")]
            Registration,
            [Display(Name = "Abgabedatum")]
            Filing,
            [Display(Name = "Typ")]
            Type

        }






        // GET: Theses
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(string Search, string Filter, SortCriteria Sort = SortCriteria.Id, int Page = 1, int PageSize = 10)
        {
            IQueryable<Thesis> query = _context.Thesen;
            query = (Search != null) ? query.Where(m => (m.Title.Contains(Search))) : query;
            query = (Filter != null) ? query.Where(m => (Models.ErrorLevelExtensions.ToFriendlyString(m.Status) == Filter)) : query;

            switch (Sort)
            {
                case SortCriteria.Status:
                    query = query.OrderBy(m => m.Status);
                    break;
                case SortCriteria.Registration:
                    query = query.OrderBy(m => m.Registration);
                    break;
                case SortCriteria.Filing:
                    query = query.OrderBy(m => m.Filing);
                    break;
                case SortCriteria.Type:
                    query = query.OrderBy(m => m.Type);
                    break;
                case SortCriteria.Title:
                    query = query.OrderBy(m => m.Title);
                    break;
                case SortCriteria.StudentName:
                    query = query.OrderBy(m => m.StudentName);
                    break;
                default:
                    query = query.OrderBy(m => m.Id);
                    break;
            }

            int PageTotal = ((await query.CountAsync()) + PageSize - 1) / PageSize;
            Page = (Page > PageTotal) ? PageTotal : Page;
            Page = (Page < 1) ? 1 : Page;

            ViewBag.Search = Search;
            ViewBag.Filter = Filter;
            ViewBag.FilterValues = new SelectList(await _context.Thesen.Select(m => m.Status).Distinct().ToListAsync());
            ViewBag.Sort = Sort;
            ViewBag.Page = Page;
            ViewBag.PageTotal = PageTotal;
            ViewBag.PageSize = PageSize;

            var myContext = _context.Thesen.Include(t => t.Programme).Include(t => t.Supervisor);

            //return View(await myContext.ToListAsync());
            return View(await query.Skip(PageSize * (Page - 1)).Take(PageSize).ToListAsync());

        }





        // GET: Theses/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Thesen
                .Include(t => t.Programme)
                .Include(t => t.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thesis == null)
            {
                return NotFound();
            }

            return View(thesis);
        }





        // GET: Theses/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name");
            ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "FullName");
            return View();
        }





        // POST: Theses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,SupervisorId,Bachelor,Master,Status,StudentName,StudentId,ProgrammeId,Registration,Filing,Type,Summary,Strenghts,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteratureVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StrucutreWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,WeightSum,Grade,LastModified")] Thesis thesis)
        {
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", thesis.ProgrammeId);
            ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "FullName", thesis.SupervisorId);
           

            if (ModelState.IsValid)
            {
                thesis.SupervisorName = _context.Betreuer.Find(thesis.SupervisorId).FullName;
                thesis.SupervisorEMail = _context.Betreuer.Find(thesis.SupervisorId).EMail;

                thesis.LastModified = DateTime.Now;
                //thesis.Grade = thesis.Grade / 10;
                _context.Add(thesis);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(thesis);
        }




        
        // GET: Theses/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Thesen.FindAsync(id);
            thesis.LastModified = DateTime.Now;

            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", thesis.ProgrammeId);
            ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "FullName", thesis.SupervisorId);
            if (thesis == null)
            {
                return NotFound();
            }
            
            return View(thesis);
        }
        
        /*
        // GET: Theses/Edit/5
        [Authorize(Roles = "Administrator"), HttpPost("FileUpload")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var thesis = await _context.Thesen.FindAsync(id);
                ThesisVM thesisvm = new ThesisVM()
                {
                    Id = thesis.Id,
                    Title = thesis.Title,
                    Description = thesis.Description,
                    SupervisorId = thesis.SupervisorId,
                    Bachelor = thesis.Bachelor,
                    Master = thesis.Master,
                    Status = thesis.Status,
                    StudentName = thesis.StudentName,
                    StudentId = thesis.StudentId,
                    ProgrammeId = thesis.ProgrammeId,
                    Registration = thesis.Registration,
                    Filing = thesis.Filing,
                    Type = thesis.Type,
                    Summary = thesis.Summary,
                    Strenghts = thesis.Strenghts,
                    Weaknesses = thesis.Weaknesses,
                    Evaluation = thesis.Evaluation,
                    ContentVal = thesis.ContentVal,
                    LayoutVal = thesis.LayoutVal,
                    StructureVal = thesis.StructureVal,
                    StyleVal = thesis.StyleVal,
                    LiteratureVal = thesis.LiteratureVal,
                    DifficultyVal = thesis.DifficultyVal,
                    NoveltyVal = thesis.NoveltyVal,
                    RichnessVal = thesis.RichnessVal,
                    ContentWt = thesis.ContentWt,
                    LayoutWt = thesis.LayoutWt,
                    StrucutreWt = thesis.StrucutreWt,
                    LiteratureWt = thesis.LiteratureWt,
                    DifficultyWt = thesis.DifficultyWt,
                    NoveltyWt = thesis.NoveltyWt,
                    RichnessWt = thesis.RichnessWt,
                    WeightSum = thesis.WeightSum,
                    Grade = thesis.Grade,
                    LastModified = thesis.LastModified,

                };

                var fileName = Path.GetFileName(thesisvm.Pdf.FileName);
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "Datei");

                using(var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await thesisvm.Pdf.CopyToAsync(fileSteam);
                }
                
                ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", thesis.ProgrammeId);
                ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "EMail", thesis.SupervisorId);

                return View(thesisvm);
            }
            return View();
        }*/
       




        // POST: Theses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,SupervisorId,Bachelor,Master,Status,StudentName,StudentId,ProgrammeId,Registration,Filing,Type,Summary,Strenghts,Weaknesses,Evaluation,ContentVal,LayoutVal,StructureVal,StyleVal,LiteratureVal,DifficultyVal,NoveltyVal,RichnessVal,ContentWt,LayoutWt,StrucutreWt,StyleWt,LiteratureWt,DifficultyWt,NoveltyWt,RichnessWt,WeightSum,Grade,LastModified")] Thesis thesis)
        {
            if (id != thesis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    thesis.SupervisorName = _context.Betreuer.Find(thesis.SupervisorId).FullName;
                    thesis.SupervisorEMail = _context.Betreuer.Find(thesis.SupervisorId).EMail;
                    _context.Update(thesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThesisExists(thesis.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", thesis.ProgrammeId);
            ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "FullName", thesis.SupervisorId);
            return View(thesis);
        }





        // GET: Theses/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Thesen
                .Include(t => t.Programme)
                .Include(t => t.Supervisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thesis == null)
            {
                return NotFound();
            }

            return View(thesis);
        }

        // POST: Theses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thesis = await _context.Thesen.FindAsync(id);
            _context.Thesen.Remove(thesis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThesisExists(int id)
        {
            return _context.Thesen.Any(e => e.Id == id);
        }


        //Public View
        public async Task<IActionResult> Public(string Search, SortCriteria Sort = SortCriteria.Id, int Page = 1, int PageSize = 10)
        {
            IQueryable<Thesis> query = _context.Thesen.Include(t => t.Supervisor).Include(t => t.Programme);

            query = (Search != null) ? query.Where(m => (m.Title.Contains(Search))) : query;
            int PageTotal = ((await query.CountAsync()) + PageSize - 1) / PageSize;
            Page = (Page > PageTotal) ? PageTotal : Page;
            Page = (Page < 1) ? 1 : Page;

            switch (Sort)
            {
                default:
                    query = query.OrderBy(m => m.Title);
                    break;
            }

            //ViewData["SupervisorId"] = new SelectList(_context.Betreuer, "Id", "EMail", thesis.SupervisorId);
            ViewBag.Search = Search;
            ViewBag.Sort = Sort;
            ViewBag.Page = Page;
            ViewBag.PageTotal = PageTotal;
            ViewBag.PageSize = PageSize;

            return View(await query.Skip(PageSize * (Page - 1)).Take(PageSize).ToListAsync());

        }


        //Pdf View Details
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DetailsPdf(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thesis = await _context.Thesen
                .FirstOrDefaultAsync(m => m.Id == id);
            thesis.SupervisorName = _context.Betreuer.Find(thesis.SupervisorId).FullName;
            if (thesis == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(thesis);

        }
    }
}
