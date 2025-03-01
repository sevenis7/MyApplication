using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService.Edit;
using MyApplicationServiceLayer.ProjectService.List;
using MyApplicationServiceLayer.ProjectService.Models;
using MyApplicationServiceLayer.ProjectService.Post;
using MyApplicationServiceLayer.ProjectService.Remove;

namespace MyApplicationServiceLayer.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IListProjectService _listProjectService;
        private readonly IEditProjectService _editProjectService;
        private readonly IPostProjectService _postProjectService;
        private readonly IRemoveProjectService _removeProjectService;

        public ProjectService
            (IListProjectService listProjectService,
            IEditProjectService editProjectService,
            IPostProjectService postProjectService,
            IRemoveProjectService removeProjectService)
        {
            _listProjectService = listProjectService;
            _editProjectService = editProjectService;
            _postProjectService = postProjectService;
            _removeProjectService = removeProjectService;
        }

        public async Task<Project?> Get(int id)
        {
            return await _listProjectService.Get(id);
        }

        public IQueryable<Project> GetAll()
        {
            return _listProjectService.GetAll();
        }

        public async Task<Project?> Edit(int id, Project project)
        {
            return await _editProjectService.Edit(id, project);
        }

        public async Task<Project?> Post(ProjectModel projectModel)
        {
            return await _postProjectService.Post(projectModel);
        }

        public async Task<Project?> Remove(int id)
        {
            return await _removeProjectService.Remove(id);
        }
    }
}
