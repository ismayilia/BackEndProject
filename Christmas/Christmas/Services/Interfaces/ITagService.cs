﻿using Christmas.Areas.Admin.ViewModels.Tag;

namespace Christmas.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagVM>> GetAllAsync();
        Task<TagVM> GetByIdWithoutTrackingAsync(int id);
        Task DeleteAsync(int id);
        Task CreateAsync(TagCreateVM tag);
        Task EditAsync(TagEditVM tag);
        Task<TagVM> GetByNameWithoutTrackingAsync(string name);
    }
}
