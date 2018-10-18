﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Semp.Infrastructure.Data;
using Semp.Infrastructure.Web;
using Semp.Module.Cms.Models;
using Semp.Module.Cms.ViewModels;

namespace Semp.Module.Cms.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IRepository<Menu> _menuRepository;

        public MenuViewComponent(IRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /*
        public async Task<IViewComponentResult> InvokeAsync(long menuId)
        {
            var menu = await _menuRepository.Query().Include(x => x.MenuItems).ThenInclude(m => m.Entity).FirstOrDefaultAsync(x => x.Id == menuId);
            if (menu == null)
            {
                throw new ArgumentException($"Cannot found menu item id {menuId}");
            }

            var menuItemVms = new List<MenuItemVm>();
            var menuItems = menu.MenuItems.Where(x => !x.ParentId.HasValue).OrderBy(x => x.DisplayOrder);
            foreach (var item in menuItems)
            {
                var menuItemVm = Map(item);
                menuItemVms.Add(menuItemVm);
            }

            return View(this.GetViewPath(), menuItemVms);
        }
        */

        public async Task<IViewComponentResult> InvokeAsync(string menuName, string viewName = "Default")
        {
            var menu = await _menuRepository.Query()
                                            .Include(x => x.MenuItems)
                                            .ThenInclude(m => m.Entity)
                                            .FirstOrDefaultAsync(x => x.Name == menuName);
            if (menu == null)
            {
                throw new ArgumentException($"Cannot found menu item name {menuName}");
            }

            var menuItemVms = new List<MenuItemVm>();
            var menuItems = menu.MenuItems.Where(x => !x.ParentId.HasValue)
                                          .OrderBy(x => x.DisplayOrder);
            foreach (var item in menuItems)
            {
                var menuItemVm = Map(item);
                menuItemVms.Add(menuItemVm);
            }

            if (string.IsNullOrWhiteSpace(viewName))
                return View(this.GetViewPath("Default"), menuItemVms);
            return View(this.GetViewPath(viewName), menuItemVms);
        }

        private MenuItemVm Map(MenuItem menuItem)
        {
            var menuItemVm = new MenuItemVm
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Link = menuItem.Entity == null ? menuItem.CustomLink : $"/{menuItem.Entity.Slug}"
            };

            var childItems = menuItem.Children.OrderBy(x => x.DisplayOrder);
            foreach (var childItem in childItems)
            {
                var childMenuItemVm = Map(childItem);
                menuItemVm.AddChildItem(childMenuItemVm);
            }

            return menuItemVm;
        }
    }
}
