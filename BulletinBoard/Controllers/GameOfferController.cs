using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Helpers;
using Project.Models;
using Project.Models.ErrorViewModels;
using Project.Models.GameOfferViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Project.Controllers
{
    [Authorize]
    public class GameOfferController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GameOfferController(ApplicationDbContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = RoleHelper.User)]
        public IActionResult Error(int? statusCode)
        {
            var vm = new ErrorViewModel
            {
                Response = statusCode?.ToString() ?? "-",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(vm);
        }

        // GET: GameOffer
        [Authorize(Roles = "Administrator, User")]
        [Route("Games/Index")]
        public async Task<IActionResult> Index()
        {
            var GameOffers = await GetGameOffersGreedy()
                .Select(m => _mapper.Map<GameOfferViewModel>(m))
                .ToListAsync();

            ViewData["GameOfferCount"] = GameOffers.Count;
            return View(GameOffers);
        }

        [Authorize(Roles = "Administrator, User")]
        [Route("Games/Search")]
        public async Task<IActionResult> Search(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return RedirectToAction(nameof(Index));
            }

            phrase = phrase.ToLower();

            // filter offers that contains search phrase, then map them to view models
            var GameOffers = await GetGameOffersGreedy()
                .Where(c => c.Title.Contains(phrase)
                            || c.GameType.Name.ToLower().Contains(phrase)
                            || c.GameCategory.Name.ToLower().Contains(phrase))
                .Select(m => _mapper.Map<GameOfferViewModel>(m))
                .ToListAsync();
            
            ViewData["GameOfferCount"] = GameOffers.Count;
            ViewData["phrase"] = phrase;
            return View("Index", GameOffers);
        }

        // GET: GameOffer/Popular
        [AllowAnonymous]
        [Route("Games/Popular")]
        public async Task<IActionResult> Popular()
        {
            var GameOffers = await GetGameOffersGreedy()
                .OrderByDescending(m => m.Visits)
                .Take(4)
                .Select(m => _mapper.Map<PopularGameOfferViewModel>(m))
                .ToListAsync();

            return View(GameOffers);
        }

        // GET: GameOffer/Details/5
        [Authorize(Roles = "Administrator, User")]
        [Route("Games/Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var GameOffer = await GetGameOffersGreedy()
                .SingleOrDefaultAsync(m => m.GameOfferId == id);

            if (GameOffer == null)
            {
                return View("NotFound");
            }

            GameOffer.Visits += 1;
            _context.Update(GameOffer);
            await _context.SaveChangesAsync();

            var viewModel = _mapper.Map<DetailsGameOfferViewModel>(GameOffer);

            return View(viewModel);
        }

        // GET: GameOffer/Create
        [Route("Games/Create")]
        public IActionResult Create()
        {
            var viewModel = new CreateGameOfferViewModel
            {
                GameCategories = _context.GameCategories,
                GameTypes = _context.GameTypes
            };

            return View(viewModel);
        }

        // POST: GameOffer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Games/Create")]
        public async Task<IActionResult> Create(CreateGameOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.GameCategories = _context.GameCategories;
                model.GameTypes = _context.GameTypes;
                return View(model);
            }

            var GameOffer = new GameOffer
            {

                GameCategory = await _context.GameCategories.SingleOrDefaultAsync(c => c.GameCategoryId == model.GameCategoryId),
                GameType = await _context.GameTypes.SingleOrDefaultAsync(c => c.GameTypeId == model.GameTypeId),

                Title = model.Title,
                Description = model.Description,

                Year = model.Year,
                Visits = 0
            };

            _context.Add(GameOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: GameOffer/Edit/5
        [Route("Games/Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var GameOffer = await GetGameOffersGreedy()
                .SingleOrDefaultAsync(m => m.GameOfferId == id);

            if (GameOffer == null)
            {
                return View("NotFound");
            }


            var viewModel = _mapper.Map<EditGameOfferViewModel>(GameOffer);
            viewModel.GameCategories = _context.GameCategories;
            viewModel.GameTypes = _context.GameTypes;
            return View(viewModel);
        }

        // POST: GameOffer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Games/Edit")]
        public async Task<IActionResult> Edit(EditGameOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.GameCategories = _context.GameCategories;
                model.GameTypes = _context.GameTypes;
                return View(model);
            }

            try
            {
                var GameOffer = await _context.GameOffers
                    .SingleOrDefaultAsync(m => m.GameOfferId == model.GameOfferId);

                GameOffer.GameCategory = await _context.GameCategories
                    .SingleOrDefaultAsync(c => c.GameCategoryId == model.GameCategoryId);
                GameOffer.GameType = await _context.GameTypes
                    .SingleOrDefaultAsync(c => c.GameTypeId == model.GameTypeId);
                GameOffer.Title = model.Title;
                GameOffer.Description = model.Description;
                GameOffer.Year = model.Year;


                _context.Update(GameOffer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameOfferExists(model.GameOfferId))
                {
                    return View("NotFound");
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GameOffer/Delete/5
        [Route("Games/Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var GameOffer = await _context.GameOffers
                .SingleOrDefaultAsync(m => m.GameOfferId == id);
            if (GameOffer == null)
            {
                return View("NotFound");
            }

            var viewModel = _mapper.Map<DeleteGameOfferViewModel>(GameOffer);
            return View(viewModel);
        }

        // POST: GameOffer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Games/Delete")]
        public async Task<IActionResult> Delete(DeleteGameOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var GameOffer = await _context.GameOffers
                .SingleOrDefaultAsync(m => m.GameOfferId == model.GameOfferId);
            _context.GameOffers.Remove(GameOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GameOfferExists(string id)
        {
            return await _context.GameOffers.AnyAsync(e => e.GameOfferId == id);
        }

        private IQueryable<GameOffer> GetGameOffersGreedy()
        {
            // greedy load as EF Core doesn't support lazy-loading yet
            return _context.GameOffers
                .Include(u => u.GameCategory)
                .Include(u => u.GameType);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }

        private async Task<bool> IsUserModerator()
        {
            var user = await GetCurrentUser();
            return await _userManager.IsInRoleAsync(user, RoleHelper.Moderator);
        }

        private async Task<bool> IsUserAdministrator()
        {
            var user = await GetCurrentUser();
            return await _userManager.IsInRoleAsync(user, RoleHelper.Administrator);
        }
    }
}
