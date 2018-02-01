using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Helpers;
using Project.Models;
using Project.Models.GameTypeViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [Authorize(Roles = RoleHelper.Administrator)]
    public class GameTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GameTypeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: GameType
        public async Task<IActionResult> Index()
        {
            var gameTypes = await _context.GameTypes
                .Select(m => _mapper.Map<GameTypeViewModel>(m)).ToListAsync();
            return View(gameTypes);
        }

        // GET: GameType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameType = await _context.GameTypes
                .SingleOrDefaultAsync(m => m.GameTypeId == id);
            if (gameType == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<DetailsGameTypeViewModel>(gameType);
            return View(viewModel);
        }

        // GET: GameType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gameType = new GameType {Name = model.Name};
            _context.Add(gameType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: GameType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameType = await _context.GameTypes
                .SingleOrDefaultAsync(m => m.GameTypeId == id);
            if (gameType == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<EditGameTypeViewModel>(gameType);
            return View(viewModel);
        }

        // POST: GameType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var gameType = await _context.GameTypes
                    .SingleOrDefaultAsync(m => m.GameTypeId == model.GameTypeId);
                gameType.Name = model.Name;
                _context.Update(gameType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameTypeExists(model.GameTypeId))
                {
                    return View("NotFound");
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GameType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var gameType = await _context.GameTypes
                .SingleOrDefaultAsync(m => m.GameTypeId == id);
            if (gameType == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<DeleteGameTypeViewModel>(gameType);
            return View(viewModel);
        }

        // POST: GameType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteGameTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var gameType = await _context.GameTypes
                .SingleOrDefaultAsync(m => m.GameTypeId == model.GameTypeId);
            _context.GameTypes.Remove(gameType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameTypeExists(string id)
        {
            return await _context.GameTypes.AnyAsync(e => e.GameTypeId == id);
        }
    }
}
