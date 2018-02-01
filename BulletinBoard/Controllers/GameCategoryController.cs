using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Helpers;
using Project.Models;
using Project.Models.GameCategoryViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [Authorize(Roles = RoleHelper.Administrator)]
    public class GameCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GameCategoryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: GameCategory
        public async Task<IActionResult> Index()
        {
            var gameCategories = await _context.GameCategories
                .Select(m => _mapper.Map<GameCategoryViewModel>(m)).ToListAsync();
            return View(gameCategories);
        }

        // GET: GameCategory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameCategory = await _context.GameCategories
                .SingleOrDefaultAsync(m => m.GameCategoryId == id);
            if (gameCategory == null)
            {
                return View("NotFound");
            }
            
            var viewModel = _mapper.Map<DetailsGameCategoryViewModel>(gameCategory);
            return View(viewModel);
        }

        // GET: GameCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gameCategory = new GameCategory {Name = model.Name};
            _context.Add(gameCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: GameCategory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameCategory = await _context.GameCategories
                .SingleOrDefaultAsync(m => m.GameCategoryId == id);
            if (gameCategory == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<EditGameCategoryViewModel>(gameCategory);
            return View(viewModel);
        }

        // POST: GameCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var gameCategory = await _context.GameCategories
                    .SingleOrDefaultAsync(m => m.GameCategoryId == model.GameCategoryId);
                gameCategory.Name = model.Name;
                _context.Update(gameCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameCategoryExists(model.GameCategoryId))
                {
                    return View("NotFound");
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GameCategory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameCategory = await _context.GameCategories
                .SingleOrDefaultAsync(m => m.GameCategoryId == id);
            if (gameCategory == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<DeleteGameCategoryViewModel>(gameCategory);
            return View(viewModel);
        }

        // POST: GameType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteGameCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gameCategory = await _context.GameCategories
                .SingleOrDefaultAsync(m => m.GameCategoryId == model.GameCategoryId);
            _context.GameCategories.Remove(gameCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameCategoryExists(string id)
        {
            return await _context.GameCategories.AnyAsync(e => e.GameCategoryId == id);
        }
    }
}
