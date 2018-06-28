using System.Threading.Tasks;
using Semp.Infrastructure.Data;
using Semp.Module.Cms.Models;
using Semp.Module.Core.Services;

namespace Semp.Module.Cms.Services
{
    public class PageService : IPageService
    {
        public const string PageEntityTypeId = "Page";

        private readonly IRepository<Page> _pageRepository;
        private readonly IEntityService _entityService;

        public PageService(IRepository<Page> pageRepository, IEntityService entityService)
        {
            _pageRepository = pageRepository;
            _entityService = entityService;
        }

        public async Task Create(Page page)
        {
            using (var transaction = _pageRepository.BeginTransaction())
            {
                page.Slug = _entityService.ToSafeSlug(page.Slug, page.Id, PageEntityTypeId);
                _pageRepository.Add(page);
                await _pageRepository.SaveChangesAsync();

                _entityService.Add(page.Name, page.Slug, page.Id, PageEntityTypeId);
                await _pageRepository.SaveChangesAsync();

                transaction.Commit();
            }
        }

        public async Task Update(Page page)
        {
            page.Slug = _entityService.ToSafeSlug(page.Slug, page.Id, PageEntityTypeId);
            _entityService.Update(page.Name, page.Slug, page.Id, PageEntityTypeId);
            await _pageRepository.SaveChangesAsync();
        }

        public async Task Delete(Page page)
        {
            _pageRepository.Remove(page);
            await _entityService.Remove(page.Id, PageEntityTypeId);
            _pageRepository.SaveChanges();
        }
    }
}
